using Godot;
using System;

public class MainMenu : Node2D
{
    TextureButton StartGameButton;
    TextureButton CreditsButton;
    TextureButton QuitGameButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StartGameButton = GetNode<TextureButton>("StartGameButton");
        CreditsButton = GetNode<TextureButton>("CreditsButton");
        QuitGameButton = GetNode<TextureButton>("QuitGameButton");

        StartGameButton.Connect("pressed", this, "_on_StartGameButton_pressed");
        CreditsButton.Connect("pressed", this, "_on_CreditsButton_pressed");
        QuitGameButton.Connect("pressed", this, "_on_QuitGameButton_pressed");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private void _on_StartGameButton_pressed()
    {
        GetTree().ChangeScene("res://Scenes/World.tscn");
    }

    private void _on_CreditsButton_pressed()
    {
        GetTree().ChangeScene("res://Scenes/Credits.tscn");
    }

    private void _on_QuitGameButton_pressed()
    {
        GetTree().Quit();
    }
}
