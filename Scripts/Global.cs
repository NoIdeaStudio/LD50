using Godot;
using System;

public class Global : Node
{
    public int Iron = 0;
    public int Energy = 0;
    public int maxIron = 10;
    public int maxEnergy = 10;
    public int health = 100;

    public int kills = 0;
    public int upgrades = 0;

    // start time
    public DateTime startTime;

    public Overlay overlay;
    public bool shootMode = false;
    public PackedScene deathScreen;
    public PackedScene newWaveScreen;
    public int enemiesLeft = 0;
    public int wave = 1;

    Timer waveTimer;
    public int countdown = 30;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        deathScreen = ResourceLoader.Load("res://Scenes/DeathScreen.tscn") as PackedScene;
        newWaveScreen = ResourceLoader.Load("res://Scenes/NewWaveScreen.tscn") as PackedScene;

        waveTimer = new Timer();
        waveTimer.WaitTime = 1;
        waveTimer.Connect("timeout", this, "waveCountdown");
        AddChild(waveTimer);
        waveTimer.Start();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void addIron(int amount){
        GD.Print("Adding Iron");
        Iron += amount;
        overlay.setIron(Iron);
    }

    public void addEnergy(int amount){
        GD.Print("Adding Energy");
        Energy += amount;
        overlay.setEnergy(Energy);
    }

    public void removeIron(int amount){
        GD.Print("Removing Iron");
        Iron -= amount;
        overlay.setIron(Iron);
    }

    public void removeEnergy(int amount){
        GD.Print("Removing Energy");
        Energy -= amount;
        overlay.setEnergy(Energy);
    }

    public void addHealth(int amount){
        GD.Print("Adding Health");
        health += amount;
        overlay.setHealth(health);
    }

    public void removeHealth(int amount){
        GD.Print("Removing Health");
        health -= amount;
        overlay.setHealth(health);

        if (health <= 0){
            GD.Print("Game Over");
            death();
        }
    }

    public void updateOverlay(){
        overlay.update();
    }

    public void death(){
        GetTree().Paused = true;
        GetTree().Root.AddChild(deathScreen.Instance());

        Energy = 0;
        Iron = 0;
        health = 100;
        overlay.setEnergy(Energy);
        overlay.setIron(Iron);
        overlay.setHealth(health);

        maxEnergy = 10;
        maxIron = 10;

        shootMode = false;

        updateOverlay();
    }

    public void waveCountdown(){
        countdown--;
        overlay.setWaveTime(countdown);
    }

    public void setWave(){
        
    }
}
