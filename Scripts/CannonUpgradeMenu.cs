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
    public TextureButton TripleUpgradeButton;
    public TextureButton FiveUpgradeButton;

    public int currentDamage = 1;
    public int currentVelocity = 1;
    public int currentPiercing = 1;
    public int currentCharging = 1;
    public int currentMaxCharge = 1;

    public int upgrades = 1;
    public int costMult = 5;
    Global global;
    AudioStreamPlayer upgradeSound;
    AudioStreamPlayer cantAffordSound;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");

        DamageUpgradeButton = GetNode<TextureButton>("DamageUpgradeButton");
        VelocityUpgradeButton = GetNode<TextureButton>("VelocityUpgradeButton");
        PiercingUpgradeButton = GetNode<TextureButton>("PiercingUpgradeButton");
        ChargingUpgradeButton = GetNode<TextureButton>("ChargingUpgradeButton");
        MaxChargeUpgradeButton = GetNode<TextureButton>("MaxChargeUpgradeButton");
        TripleUpgradeButton = GetNode<TextureButton>("TripleUpgradeButton");
        FiveUpgradeButton = GetNode<TextureButton>("FiveUpgradeButton");
        upgradeSound = GetNode<AudioStreamPlayer>("UpgradeSound");
        cantAffordSound = GetNode<AudioStreamPlayer>("CantAffordSound");

        DamageUpgradeButton.Connect("pressed", this, "_on_DamageUpgradeButton_pressed");
        VelocityUpgradeButton.Connect("pressed", this, "_on_VelocityUpgradeButton_pressed");
        PiercingUpgradeButton.Connect("pressed", this, "_on_PiercingUpgradeButton_pressed");
        ChargingUpgradeButton.Connect("pressed", this, "_on_ChargingUpgradeButton_pressed");
        MaxChargeUpgradeButton.Connect("pressed", this, "_on_MaxChargeUpgradeButton_pressed");
        TripleUpgradeButton.Connect("pressed", this, "_on_TripleUpgradeButton_pressed");
        FiveUpgradeButton.Connect("pressed", this, "_on_FiveUpgradeButton_pressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private void _on_DamageUpgradeButton_pressed()
    {
        if (global.Iron >= upgrades * costMult){
            global.removeIron(upgrades*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        CurrentCannon.UpgradeDamage();
        
        currentDamage++;
        upgrades ++;
        global.upgrades++;
        upgradeSound.Play();
        updatePrices();
        GetNode<ColorRect>("DamageUpgradeButton/Sprite/" + currentDamage.ToString()).Visible = true;
        if (currentDamage >= 5){
            DamageUpgradeButton.Disabled = true;
            return;
        }
    }

    private void _on_VelocityUpgradeButton_pressed()
    {
        if (global.Iron >= upgrades * costMult){
            global.removeIron(upgrades*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        CurrentCannon.UpgradeVelocity();
        currentVelocity++;
        upgrades ++;
        global.upgrades++;
        upgradeSound.Play();
        updatePrices();
        GetNode<ColorRect>("VelocityUpgradeButton/Sprite/" + currentVelocity.ToString()).Visible = true;

        if (currentVelocity >= 5)
        {
            VelocityUpgradeButton.Disabled = true;
            return;
        }
    }

    private void _on_PiercingUpgradeButton_pressed()
    {
        if (global.Iron >= upgrades * costMult){
            global.removeIron(upgrades*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        CurrentCannon.UpgradePiercing();
        currentPiercing++;
        upgrades ++;
        global.upgrades++;
        upgradeSound.Play();
        updatePrices();
        GetNode<ColorRect>("PiercingUpgradeButton/Sprite/" + currentPiercing.ToString()).Visible = true;

        if (currentPiercing >= 5)
        {
            PiercingUpgradeButton.Disabled = true;
            return;
        }
    }

    private void _on_ChargingUpgradeButton_pressed()
    {
        if (global.Iron >= upgrades * costMult){
            global.removeIron(upgrades*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        CurrentCannon.UpgradeCharging();
        currentCharging++;
        upgrades ++;
        global.upgrades++;
        upgradeSound.Play();
        updatePrices();
        GetNode<ColorRect>("ChargingUpgradeButton/Sprite/" + currentCharging.ToString()).Visible = true;

        if (currentCharging >= 5)
        {
            ChargingUpgradeButton.Disabled = true;
            return;
        }
    }

    private void _on_MaxChargeUpgradeButton_pressed()
    {
        if (global.Iron >= upgrades * costMult){
            global.removeIron(upgrades*costMult);
        }else{
            cantAffordSound.Play();
            return;
        }
        CurrentCannon.UpgradeMaxCharge();
        
        currentMaxCharge++;
        upgrades ++;
        global.upgrades++;
        upgradeSound.Play();
        updatePrices();
        GetNode<ColorRect>("MaxChargeUpgradeButton/Sprite/" + currentMaxCharge.ToString()).Visible = true;
        if(currentMaxCharge >= 5)
        {
            MaxChargeUpgradeButton.Disabled = true;
        }
    }

    private void updatePrices(){
        DamageUpgradeButton.GetNode<Label>("CostLabel").Text = (upgrades * costMult).ToString();
        VelocityUpgradeButton.GetNode<Label>("CostLabel").Text = (upgrades * costMult).ToString();
        PiercingUpgradeButton.GetNode<Label>("CostLabel").Text = (upgrades * costMult).ToString();
        ChargingUpgradeButton.GetNode<Label>("CostLabel").Text = (upgrades * costMult).ToString();
        MaxChargeUpgradeButton.GetNode<Label>("CostLabel").Text = (upgrades * costMult).ToString();
    }

    private void _on_TripleUpgradeButton_pressed()
    {
        if (global.Iron >= 25){
            global.removeIron(25);
        }else{
            cantAffordSound.Play();
            return;
        }
        CurrentCannon.UpgradeTriple();
        TripleUpgradeButton.Disabled = true;
        FiveUpgradeButton.Disabled = false;
        global.upgrades++;
        upgradeSound.Play();
    }

    private void _on_FiveUpgradeButton_pressed()
    {
        if (global.Iron >= 50){
            global.removeIron(50);
        }else{
            cantAffordSound.Play();
            return;
        }
        CurrentCannon.UpgradeFive();
        FiveUpgradeButton.Disabled = true;
        global.upgrades++;
        upgradeSound.Play();
    }

}
