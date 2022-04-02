using Godot;
using System;

public class Cannon : StaticBody2D
{   
    private Sprite cannonTop;

    private InteractionMenu menu;

    Global global;

    [Export] public bool flipped = false;

    public PackedScene bullet;

    private Position2D bulletSpawner;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Area2D>("Area2D").Connect("body_entered", this, "_on_Cannon_body_entered");
        GetNode<Area2D>("Area2D").Connect("body_exited", this, "_on_Cannon_body_exited");

        cannonTop = GetNode<Sprite>("CannonTop");
        menu = GetNode<InteractionMenu>("InteractionMenu");
        global = GetNode<Global>("/root/Global");
        bulletSpawner = cannonTop.GetNode<Position2D>("BulletSpawner");

        // preload bullet
        bullet = (PackedScene)ResourceLoader.Load("res://Scenes/Bullet.tscn");

        if (flipped)
        {
            this.Scale *= new Vector2(-1, 1);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (global.shootMode){
            // get mouse position
            Vector2 mousePos = GetGlobalMousePosition();
            lookAt(mousePos);
        }
    }

    private void lookAt(Vector2 look){
        Vector2 direction;
        if (!flipped){
            direction = cannonTop.GlobalPosition - look;
        }else{
            direction = look - cannonTop.GlobalPosition;
        }
        float angle = direction.Angle();

        if (flipped){
            angle = -angle;
        }
        cannonTop.Rotation = angle;
    }
    public void _on_Cannon_body_entered(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.show();
        }
    }

    public void _on_Cannon_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.hide();
        }
    }

    public void interact(){
        GD.Print("yes pressed");
    }

    public void upgrade(){
        GD.Print("upgrade pressed");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("shoot"))
        {
            if (global.shootMode){
                GD.Print("shoot");
                Bullet bulletInstance = (Bullet)bullet.Instance();
                bulletInstance.Rotation = cannonTop.Rotation;
                bulletInstance.Position = bulletSpawner.GlobalPosition;

                bulletInstance.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation), -Mathf.Sin(cannonTop.Rotation));

                if (flipped){
                    bulletInstance.direction.x *= -1;
                }

                GetParent().AddChild(bulletInstance);
            }
        }
    }
}
