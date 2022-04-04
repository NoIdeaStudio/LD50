using Godot;
using System;

public class FabricatorUpgradeMenu : Node2D
{
    Fabricator fabricator;

    TextureButton speedUpgradeButton;
    public int currentSpeed = 1;

    public int costMult = 5;
    public Label CostLabel;
    Global global;
    AudioStreamPlayer upgradeSound;
    AudioStreamPlayer cantAffordSound;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");

        fabricator = GetParent().GetNode<Fabricator>("Fabricator");
        speedUpgradeButton = GetNode<TextureButton>("SpeedUpgradeButton");
        CostLabel = speedUpgradeButton.GetNode<Label>("CostLabel");
        upgradeSound = GetNode<AudioStreamPlayer>("UpgradeSound");
        cantAffordSound = GetNode<AudioStreamPlayer>("CantAffordSound");

        speedUpgradeButton.Connect("pressed", this, "_on_SpeedUpgradeButton_pressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private void _on_SpeedUpgradeButton_pressed()
    {
        if (global.Iron >= currentSpeed * costMult && !global.tutorial){
            global.removeIron(currentSpeed*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        fabricator.upgradeSpeed();
        
        if (fabricator.speed >= fabricator.maxSpeed){
            speedUpgradeButton.Disabled = true;
        }
        currentSpeed++;
        global.upgrades++;
        GetNode<ColorRect>("Sprite2/" + currentSpeed.ToString()).Visible = true;
        upgradeSound.Play();

        CostLabel.Text = (fabricator.speed * costMult).ToString();
        
    }
}
