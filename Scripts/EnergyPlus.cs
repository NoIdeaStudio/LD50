using Godot;
using System;

public class EnergyPlus : Sprite
{
    Timer timer;
    AudioStreamPlayer audio;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        audio = GetNode<AudioStreamPlayer>("Audio");
        audio.Play();

        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, "_on_Timer_timeout");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // move slowly up
        Position = new Vector2(Position.x, Position.y - 0.5f);

    }

    private void _on_Timer_timeout()
    {
        QueueFree();
    }
}
