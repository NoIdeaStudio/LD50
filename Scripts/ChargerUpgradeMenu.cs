using Godot;
using System;

public class ChargerUpgradeMenu : Node2D
{
    Charger charger;

    TextureButton speedUpgradeButton;
    public int currentSpeed = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        charger = GetParent().GetNode<Charger>("Charger");
        speedUpgradeButton = GetNode<TextureButton>("SpeedUpgradeButton");

        speedUpgradeButton.Connect("pressed", this, "_on_SpeedUpgradeButton_pressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private void _on_SpeedUpgradeButton_pressed()
    {
        charger.upgradeSpeed();
        if (charger.speed >= charger.maxSpeed){
            speedUpgradeButton.Disabled = true;
        }
        currentSpeed++;
        GetNode<ColorRect>("Sprite/" + currentSpeed.ToString()).Visible = true;
        
    }
}
