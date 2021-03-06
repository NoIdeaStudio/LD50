using Godot;
using System;

public class Credits : Node2D
{
    TextureButton BackButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BackButton = GetNode<TextureButton>("BackButton");

        BackButton.Connect("pressed", this, "_on_BackButton_pressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private void _on_BackButton_pressed()
    {
        QueueFree();
    }
}
