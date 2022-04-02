using Godot;
using System;

public class ControlPanel : Area2D
{
    Global global;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, "_on_ControlPanel_body_entered");
        Connect("body_exited", this, "_on_ControlPanel_body_exited");

        global = GetNode<Global>("/root/Global");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_ControlPanel_body_entered(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            GetNode<InteractionMenu>("InteractionMenu").show();
        }
    }

    public void _on_ControlPanel_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            GetNode<InteractionMenu>("InteractionMenu").hide();
            global.shootMode = false;
        }
    }

    public void interact(){
        GetNode<InteractionMenu>("InteractionMenu").hide();
        global.shootMode = true;
    }

    public void upgrade(){
        GD.Print("upgrade pressed");
    }
}
