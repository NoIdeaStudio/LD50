using Godot;
using System;

public class Fabricator : Area2D
{
    InteractionMenu menu;
    Timer timer;

    Global global;

    AnimatedSprite anim;

    PackedScene IronPlus;
    public int speed = 1;
    public int maxSpeed = 5;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, "_on_Fabricator_body_entered");
        Connect("body_exited", this, "_on_Fabricator_body_exited");   
        

        menu = GetNode<InteractionMenu>("InteractionMenu");
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = 2;
        timer.Connect("timeout", this, "_on_Timer_timeout");
        anim = GetNode<AnimatedSprite>("AnimatedSprite");

        IronPlus = GD.Load<PackedScene>("res://Scenes/IronPlus.tscn");

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
            if (global.tutorial && ((World)GetParent()).TutorialFabricator.Visible == true){
                ((World)GetParent()).TutorialFabricator.Hide();
                ((World)GetParent()).TutorialCharger.Show();
            }
        }
    }

    public void _on_Fabricator_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.hide();
            timer.Stop();
            anim.Play("Idle");
            GetParent().GetNode<FabricatorUpgradeMenu>("FabricatorUpgradeMenu").Hide();
            GetParent().GetNode<Player>("Player").working = false;
        }
    }

    public void interact(){
        menu.hide();
        timer.Start();
        anim.Play("Fabricate");
        GetParent().GetNode<Player>("Player").working = true;
    }

    public void upgrade(){
        GetParent().GetNode<FabricatorUpgradeMenu>("FabricatorUpgradeMenu").Show();
    }

    public void _on_Timer_timeout(){
        if (global.Iron >= global.maxIron){
            timer.Stop();
            anim.Play("Idle");
            return;
        }
        global.addIron(1);
        
        var iron = IronPlus.Instance() as EnergyPlus;
        iron.Position = Position + new Vector2(0, -50);
        GetParent().AddChild(iron);
    }

    public void upgradeSpeed(){
        speed ++;
        if(speed > maxSpeed){
            speed = maxSpeed;
        }
        timer.WaitTime = 2f / speed;
    }
}
