using Godot;
using System;

public class Enemy : PathFollow2D
{
    public int health = 1;
    public Path2D path;
    public Vector2[] points;
    public int pathIndex = 0;
    public int speed = 100;
    public Vector2 velocity;
    public AnimatedSprite sprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        velocity = Vector2.Zero;
        sprite = GetNode<AnimatedSprite>("Sprite");
        
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        this.Offset += speed * delta;
    }

    public void hit(int amount){
        health -= amount;
        GD.Print("took " + amount + " damage");
        if (health <= 0){
            AudioStreamPlayer2D audio = new AudioStreamPlayer2D();
            audio.Stream = ResourceLoader.Load("res://Assets/enemy_death.sfxr") as AudioStream;
            GetParent().AddChild(audio);
            audio.Play();
            QueueFree();
        }
    }
}
