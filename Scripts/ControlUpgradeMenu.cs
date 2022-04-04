using Godot;
using System;

public class ControlUpgradeMenu : Node2D
{
    public TextureButton speedUpgradeButton;
    public TextureButton InventoryUpgradeButton;
    public int currentSpeed = 1;
    public int currentInventory = 1;
    public int upgrades = 1;
    public int costMult = 5;
    Global global;
    AudioStreamPlayer upgradeSound;
    AudioStreamPlayer cantAffordSound;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");

        speedUpgradeButton = GetNode<TextureButton>("WalkingSpeedUpgradeButton");
        InventoryUpgradeButton = GetNode<TextureButton>("InventoryUpgradeButton");
        upgradeSound = GetNode<AudioStreamPlayer>("UpgradeSound");
        cantAffordSound = GetNode<AudioStreamPlayer>("CantAffordSound");

        speedUpgradeButton.Connect("pressed", this, "_on_SpeedUpgradeButton_pressed");
        InventoryUpgradeButton.Connect("pressed", this, "_on_InventoryUpgradeButton_pressed");
    }

    public void _on_SpeedUpgradeButton_pressed(){
        if (global.Iron >= upgrades * costMult){
            global.removeIron(upgrades*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        GetParent().GetNode<Player>("Player").upgradeSpeed();
        currentSpeed++;
        upgrades++;
        global.upgrades++;
        upgradeSound.Play();
        updatePrices();
        speedUpgradeButton.GetNode<ColorRect>("Sprite/" + currentSpeed.ToString()).Visible = true;
        if (currentSpeed >= 5){
            speedUpgradeButton.Disabled = true;
        }
    }

    public void _on_InventoryUpgradeButton_pressed(){
        if (global.Iron >= upgrades * costMult){
            global.removeIron(upgrades*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        global.maxIron += 10;
        global.maxEnergy += 10;
        global.updateOverlay();

        currentInventory++;
        upgrades++;
        global.upgrades++;
        upgradeSound.Play();
        updatePrices();
        InventoryUpgradeButton.GetNode<ColorRect>("Sprite/" + currentInventory.ToString()).Visible = true;

        if (currentInventory >= 5){
            InventoryUpgradeButton.Disabled = true;
        }
    }

    private void updatePrices(){
        speedUpgradeButton.GetNode<Label>("CostLabel").Text = (upgrades * costMult).ToString();
        InventoryUpgradeButton.GetNode<Label>("CostLabel").Text = (upgrades * costMult).ToString();
    }
}
