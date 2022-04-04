using Godot;
using System;

public class Enemy : PathFollow2D
{
    public int health = 1;
    public Path2D path;
    public Vector2[] points;
    public int pathIndex = 0;
    public int speed = 250;
    public Vector2 velocity;
    public AnimatedSprite sprite;
    Global global;
    PackedScene diePart;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
        diePart = GD.Load<PackedScene>("res://Scenes/ExplosionEnemy.tscn");

        velocity = Vector2.Zero;
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        
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
            ExplosionEnemy explosion = (ExplosionEnemy)diePart.Instance();
            explosion.GlobalPosition = GlobalPosition;
            GetParent().AddChild(explosion);
            audio.Play();
            global.kills++;
            global.enemiesLeft--;
            QueueFree();
        }
    }

    public void setAnimation(String animation){
        sprite.Play(animation);
    }
}
