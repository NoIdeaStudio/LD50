using Godot;
using System;

public class CannonUpgradeMenu : Node2D
{
    public Cannon CurrentCannon;

    public TextureButton DamageUpgradeButton;
    public TextureButton VelocityUpgradeButton;
    public TextureButton PiercingUpgradeButton;
    public TextureButton ChargingUpgradeButton;
    public TextureButton MaxChargeUpgradeButton;

    public int currentDamage = 1;
    public int currentVelocity = 1;
    public int currentPiercing = 1;
    public int currentCharging = 1;
    public int currentMaxCharge = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        DamageUpgradeButton = GetNode<TextureButton>("DamageUpgradeButton");
        VelocityUpgradeButton = GetNode<TextureButton>("VelocityUpgradeButton");
        PiercingUpgradeButton = GetNode<TextureButton>("PiercingUpgradeButton");
        ChargingUpgradeButton = GetNode<TextureButton>("ChargingUpgradeButton");
        MaxChargeUpgradeButton = GetNode<TextureButton>("MaxChargeUpgradeButton");

        DamageUpgradeButton.Connect("pressed", this, "_on_DamageUpgradeButton_pressed");
        VelocityUpgradeButton.Connect("pressed", this, "_on_VelocityUpgradeButton_pressed");
        PiercingUpgradeButton.Connect("pressed", this, "_on_PiercingUpgradeButton_pressed");
        ChargingUpgradeButton.Connect("pressed", this, "_on_ChargingUpgradeButton_pressed");
        MaxChargeUpgradeButton.Connect("pressed", this, "_on_MaxChargeUpgradeButton_pressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private void _on_DamageUpgradeButton_pressed()
    {
        CurrentCannon.UpgradeDamage();
        if (CurrentCannon.damage >= 5)
        {
            DamageUpgradeButton.Disabled = true;
        }
        currentDamage++;
        GetNode<ColorRect>("DamageUpgradeButton/Sprite/" + currentDamage.ToString()).Visible = true;
    }

    private void _on_VelocityUpgradeButton_pressed()
    {
        CurrentCannon.UpgradeVelocity();
        if (CurrentCannon.bulletSpeed >= 1500)
        {
            VelocityUpgradeButton.Disabled = true;
        }
        currentVelocity++;
        GetNode<ColorRect>("VelocityUpgradeButton/Sprite/" + currentVelocity.ToString()).Visible = true;
    }

    private void _on_PiercingUpgradeButton_pressed()
    {
        CurrentCannon.UpgradePiercing();
        if (CurrentCannon.piercing >= 5)
        {
            PiercingUpgradeButton.Disabled = true;
        }
        currentPiercing++;
        GetNode<ColorRect>("PiercingUpgradeButton/Sprite/" + currentPiercing.ToString()).Visible = true;
    }

    private void _on_ChargingUpgradeButton_pressed()
    {
        CurrentCannon.UpgradeCharging();
        if (CurrentCannon.chargeTime >= 5)
        {
            ChargingUpgradeButton.Disabled = true;
        }
        currentCharging++;
        GetNode<ColorRect>("ChargingUpgradeButton/Sprite/" + currentCharging.ToString()).Visible = true;
    }

    private void _on_MaxChargeUpgradeButton_pressed()
    {
        CurrentCannon.UpgradeMaxCharge();
        if (CurrentCannon.maxAmmo >= 30)
        {
            MaxChargeUpgradeButton.Disabled = true;
        }
        currentMaxCharge++;
        GetNode<ColorRect>("MaxChargeUpgradeButton/Sprite/" + currentMaxCharge.ToString()).Visible = true;
    }
}
