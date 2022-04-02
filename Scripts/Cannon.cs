using Godot;
using System;

public class Cannon : KinematicBody2D
{
    CollisionShape2D collisionShape;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
    }
}
