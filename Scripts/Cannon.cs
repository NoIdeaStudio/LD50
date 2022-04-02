using Godot;
using System;

public class Cannon : StaticBody2D
{   
    private Sprite cannonTop;

    private InteractionMenu menu;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Area2D>("Area2D").Connect("body_entered", this, "_on_Cannon_body_entered");
        GetNode<Area2D>("Area2D").Connect("body_exited", this, "_on_Cannon_body_exited");

        cannonTop = GetNode<Sprite>("CannonTop");
        menu = GetNode<InteractionMenu>("InteractionMenu");

        lookAt(new Vector2(500, 150));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
    }

    private void lookAt(Vector2 look){
        Vector2 direction = look - cannonTop.GlobalPosition;
        float angle = direction.Angle();
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
}
