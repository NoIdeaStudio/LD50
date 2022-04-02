using Godot;
using System;

public class Charger : Area2D
{
    InteractionMenu menu;

    Global global;
    Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, "_on_Charger_body_entered");
        Connect("body_exited", this, "_on_Charger_body_exited");

        menu = GetNode<InteractionMenu>("InteractionMenu");
        global = GetNode<Global>("/root/Global");
        timer = GetNode<Timer>("Timer");

        timer.Connect("timeout", this, "_on_Timer_timeout");
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
            menu.show();
        }
    }

    public void _on_Charger_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.hide();
            timer.Stop();
        }
    }

    public void interact(){
        menu.hide();
        timer.Start();
    }

    public void upgrade(){

    }

    public void _on_Timer_timeout(){
        global.addEnergy(1);
    }
}
