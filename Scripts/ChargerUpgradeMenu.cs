using Godot;
using System;

public class ChargerUpgradeMenu : Node2D
{
    Charger charger;

    TextureButton speedUpgradeButton;
    public int currentSpeed = 1;
    public int costMult = 5;
    public Label CostLabel;
    Global global;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
        
        charger = GetParent().GetNode<Charger>("Charger");
        speedUpgradeButton = GetNode<TextureButton>("SpeedUpgradeButton");
        CostLabel = speedUpgradeButton.GetNode<Label>("CostLabel");

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
        global.upgrades++;
        GetNode<ColorRect>("Sprite/" + currentSpeed.ToString()).Visible = true;
        
        CostLabel.Text = (charger.speed * costMult).ToString();
        if (charger.speed >= charger.maxSpeed){
            CostLabel.Text = "/";
        }
    }
}
