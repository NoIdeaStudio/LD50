using Godot;
using System;

public class DeathScreen : Node2D
{
    TextureButton RetryButton;
    TextureButton GiveUpButton;

    public Label SecondsLabel;
    public Label KillsLabel;
    public Label UpgradesLabel;

    public int Seconds;
    public int Kills;
    public int Upgrades;

    Global global;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");

        RetryButton = GetNode<TextureButton>("RetryButton");
        RetryButton.Connect("pressed", this, "_on_RetryButton_pressed");
        GiveUpButton = GetNode<TextureButton>("GiveUpButton");
        GiveUpButton.Connect("pressed", this, "_on_GiveUpButton_pressed");

        SecondsLabel = GetNode<Label>("SecondsLabel");
        KillsLabel = GetNode<Label>("KillsLabel");
        UpgradesLabel = GetNode<Label>("UpgradesLabel");

        // Seconds is difference between now and the start of the game
        Seconds = -(int)global.startTime.Subtract(DateTime.Now).TotalSeconds;
        Kills = global.kills;
        Upgrades = global.upgrades;

        SecondsLabel.Text = "You survived " + Seconds.ToString() + " seconds!";
        KillsLabel.Text = "You killed " + Kills.ToString() + " enemies!";
        UpgradesLabel.Text = "You upgraded " + Upgrades.ToString() + " times!";
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }


    private void _on_RetryButton_pressed()
    {
        GetTree().ChangeScene("res://Scenes/World.tscn");
        GetTree().Paused = false;
        global.startTime = DateTime.Now;
        global.kills = 0;
        global.upgrades = 0;
        QueueFree();
    }

    private void _on_GiveUpButton_pressed()
    {
        GetTree().Quit();
    }
}
