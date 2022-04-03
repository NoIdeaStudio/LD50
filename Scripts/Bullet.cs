using Godot;
using System;

public class Bullet : KinematicBody2D
{
    public int speed = 500;
    public Vector2 direction;
    public int damage = 1;
    public int piercing = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var coll = MoveAndCollide(direction * speed * delta);

        if (coll != null)
        {
            ((Enemy)((KinematicBody2D)coll.Collider).GetParent()).hit(damage);
            if (piercing > 0)
            {
                piercing--;
            }
            else
            {
                QueueFree();
            }
        }
    }
}
