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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        velocity = Vector2.Zero;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // // if points is null, then the path is not set
        // if (points == null)
        // {
        //     return;
        // }

        
        // var target = points[pathIndex];
        // if (Position.DistanceTo(target) < 1){
        //     pathIndex++;
        //     if (pathIndex >= points.Length){
        //         pathIndex = 0;
        //     }
        // }
        // velocity = (target-Position).Normalized() * speed * delta;
        // velocity = MoveAndSlide(velocity);

        // // get movement direction
        // var dir = velocity.Normalized();
        // // get angle of movement direction
        // var angle = Mathf.Atan2(dir.y, dir.x);
        // // set rotation to angle
        // Rotation = angle;

        this.Offset += speed * delta;
    }

    public void hit(int amount){
        health -= amount;
        GD.Print("took " + amount + " damage");
        if (health <= 0){
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
            QueueFree();
        }
    }
}
