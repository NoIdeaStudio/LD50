using Godot;
using System;

public class Overlay : Node2D
{
    Global global;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
        global.overlay = this;
    }

    public void setIron(int amount)
    {
        GetNode<Label>("IronLabel").Text = amount.ToString() + " /" + global.maxIron.ToString();
    }

    public void setEnergy(int amount)
    {
        GetNode<Label>("EnergyLabel").Text = amount.ToString() + " /" + global.maxEnergy.ToString();
    }

    public void setHealth(int amount)
    {
        GetNode<Label>("HealthLabel").Text = amount.ToString();
    }

    public void update(){
        setIron(global.Iron);
        setEnergy(global.Energy);
        setHealth(global.health);
    }

    public void setWaveTime(int time)
    {
        GetNode<Label>("WaveLabel").Text = time.ToString();
    }
}
