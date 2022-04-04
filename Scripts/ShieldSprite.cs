using Godot;
using System;

public class ShieldSprite : Sprite
{
    Timer timer;
    Color color;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, "_on_Timer_timeout");

        color = Modulate;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void hit(){
        Modulate = new Color(1, 0.5f, 0.5f);
        timer.Start();
    }

    public void _on_Timer_timeout(){
        Modulate = color;
    }
}
