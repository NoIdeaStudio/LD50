using Godot;
using System;

public class Fabricator : Area2D
{
    InteractionMenu menu;
    Timer timer;

    Global global;

    AnimatedSprite anim;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, "_on_Fabricator_body_entered");
        Connect("body_exited", this, "_on_Fabricator_body_exited");   
        

        menu = GetNode<InteractionMenu>("InteractionMenu");
        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, "_on_Timer_timeout");
        anim = GetNode<AnimatedSprite>("AnimatedSprite");

        global = GetNode<Global>("/root/Global");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_Fabricator_body_entered(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.show();
        }
    }

    public void _on_Fabricator_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.hide();
            timer.Stop();
            anim.Play("Idle");
        }
    }

    public void interact(){
        menu.hide();
        timer.Start();
        anim.Play("Fabricate");
    }

    public void upgrade(){
        timer.Stop();
    }

    public void _on_Timer_timeout(){
        global.addIron(1);
    }
}
