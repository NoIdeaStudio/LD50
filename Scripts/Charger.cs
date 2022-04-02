using Godot;
using System;

public class Charger : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, "_on_Charger_body_entered");
        Connect("body_exited", this, "_on_Charger_body_exited");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_Charger_body_entered(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            GD.Print("yes");
        }
    }

    public void _on_Charger_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            GD.Print("no");
        }
    }
}
