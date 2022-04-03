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
    public int maxAmmo = 10;
    public int currentAmmo = 10;
    Label ammoLabel;

    Timer timer;
    Timer chargeTimer;
    private bool canShoot = true;
    [Export] public string shootAction = "shoot";

    PackedScene EnergyPlus;
    PackedScene EnergyMinus;
    public bool tripple = false;
    public bool five = false;
    public int damage = 1;
    public int piercing = 1;
    public int chargeTime = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Area2D>("Area2D").Connect("body_entered", this, "_on_Cannon_body_entered");
        GetNode<Area2D>("Area2D").Connect("body_exited", this, "_on_Cannon_body_exited");

        cannonTop = GetNode<Sprite>("CannonTop");
        menu = GetNode<InteractionMenu>("InteractionMenu");
        global = GetNode<Global>("/root/Global");
        bulletSpawner = cannonTop.GetNode<Position2D>("BulletSpawner");
        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, "_on_Timer_timeout");
        ammoLabel = GetNode<Label>("AmmoLabel");
        ammoLabel.Text = currentAmmo.ToString();
        chargeTimer = GetNode<Timer>("ChargeTimer");
        chargeTimer.Connect("timeout", this, "_on_ChargeTimer_timeout");
        EnergyPlus = GD.Load<PackedScene>("res://Scenes/EnergyPlus.tscn");
        EnergyMinus = GD.Load<PackedScene>("res://Scenes/EnergyMinus.tscn");

        // preload bullet
        bullet = (PackedScene)ResourceLoader.Load("res://Scenes/Bullet.tscn");

        if (flipped)
        {
            this.Scale *= new Vector2(-1, 1);
            ammoLabel.RectScale *= new Vector2(-1, 1);
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
            chargeTimer.Stop();
        }
    }

    public void interact(){
        menu.hide();
        chargeTimer.Start();
    }

    public void _on_ChargeTimer_timeout(){
        if (global.Energy <= 0){
            return;
        }
        if (currentAmmo < maxAmmo){
            currentAmmo++;
            ammoLabel.Text = currentAmmo.ToString();
            global.removeEnergy(1);
            var energyPlus = (EnergyPlus)EnergyPlus.Instance();
            energyPlus.Position = Position;
            AddChild(energyPlus);
            var energyMinus = (EnergyPlus)EnergyMinus.Instance();
            energyMinus.Position = Position + new Vector2(10, 0);
            AddChild(energyMinus);
        }
    }

    public void upgrade(){
        GD.Print("upgrade pressed");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(shootAction))
        {
            if (global.shootMode){
                if (canShoot && currentAmmo > 0){
                    shoot();
                }
                timer.Start();
            }
        }else if (@event.IsActionReleased(shootAction)){
            if (global.shootMode){
                timer.Stop();
                canShoot = true;
            }
        }
    }

    public void _on_Timer_timeout()
    {
        canShoot = true;
        if (currentAmmo > 0){
            shoot();
        }
    }

    private void shoot(){
        canShoot = false;
        currentAmmo--;
        ammoLabel.Text = currentAmmo.ToString();
        ((World)GetParent()).addTrauma(0.2f);
        GetNode<AnimationPlayer>("AnimationPlayer").Stop();
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        GD.Print("shoot");
        Bullet bulletInstance = (Bullet)bullet.Instance();
        bulletInstance.Rotation = cannonTop.Rotation;
        bulletInstance.Position = bulletSpawner.GlobalPosition;

        bulletInstance.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation), -Mathf.Sin(cannonTop.Rotation));

        if (flipped){
            bulletInstance.direction.x *= -1;
        }

        bulletInstance.damage = damage;
        bulletInstance.piercing = piercing;
        GetParent().AddChild(bulletInstance);

        if (tripple){
            Bullet bulletInstance2 = (Bullet)bullet.Instance();
            bulletInstance2.Rotation = cannonTop.Rotation;
            bulletInstance2.Position = bulletSpawner.GlobalPosition;

            bulletInstance2.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation - 25), -Mathf.Sin(cannonTop.Rotation - 25));

            if (flipped){
                bulletInstance2.direction.x *= -1;
            }

            Bullet bulletInstance3 = (Bullet)bullet.Instance();
            bulletInstance3.Rotation = cannonTop.Rotation;
            bulletInstance3.Position = bulletSpawner.GlobalPosition;

            bulletInstance3.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation + 25), -Mathf.Sin(cannonTop.Rotation + 25));

            if (flipped){
                bulletInstance3.direction.x *= -1;
            }

            bulletInstance2.damage = damage;
            bulletInstance2.piercing = piercing;

            bulletInstance3.damage = damage;
            bulletInstance3.piercing = piercing;

            GetParent().AddChild(bulletInstance2);
            GetParent().AddChild(bulletInstance3);
        }

        

        GetNode<AnimationPlayer>("AnimationPlayer").Play("Recoil");
    }
}
