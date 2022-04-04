using Godot;
using System;

public class NewWaveScreen : Node2D
{
    Label WaveLabel;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        WaveLabel = GetNode<Label>("WaveLabel");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void setWave(int wave){
        WaveLabel.Text = wave.ToString();
    }
}
