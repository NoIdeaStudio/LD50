using Godot;
using System;

public class Charger : Area2D
{
    InteractionMenu menu;

    Global global;
    Timer timer;
    AnimatedSprite anim;

    public int speed = 1;
    public int maxSpeed = 5;

    PackedScene EnergyPlus;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, "_on_Charger_body_entered");
        Connect("body_exited", this, "_on_Charger_body_exited");

        menu = GetNode<InteractionMenu>("InteractionMenu");
        global = GetNode<Global>("/root/Global");
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = 2;
        anim = GetNode<AnimatedSprite>("AnimatedSprite");

        timer.Connect("timeout", this, "_on_Timer_timeout");

        EnergyPlus = GD.Load<PackedScene>("res://Scenes/EnergyPlus.tscn");
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
            if (global.tutorial && ((World)GetParent()).TutorialCharger.Visible == true){
                ((World)GetParent()).TutorialCharger.Hide();
                ((World)GetParent()).TutorialCannon.Show();
            }
        }
    }

    public void _on_Charger_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.hide();
            timer.Stop();
            GetParent().GetNode<ChargerUpgradeMenu>("ChargerUpgradeMenu").Hide();
            anim.Play("Idle");
            GetParent().GetNode<Player>("Player").working = false;
        }
    }

    public void interact(){
        menu.hide();
        timer.Start();
        anim.Play("Charging");
        GetParent().GetNode<Player>("Player").working = true;
    }

    public void upgrade(){
        GetParent().GetNode<ChargerUpgradeMenu>("ChargerUpgradeMenu").Show();
    }

    public void _on_Timer_timeout(){
        if (global.Energy >= global.maxEnergy){
            timer.Stop();
            anim.Play("Idle");
            return;
        }
        global.addEnergy(1);
        var energy = EnergyPlus.Instance() as EnergyPlus;
        energy.Position = Position + new Vector2(0, -50);
        GetParent().AddChild(energy);
    }

    public void upgradeSpeed(){
        speed ++;
        timer.WaitTime = 2f / speed;
    }
}
