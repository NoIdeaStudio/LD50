using Godot;
using System;

public class Bullet : KinematicBody2D
{
    public int speed = 500;
    public Vector2 direction;
    public int damage = 1;

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
            if (coll.Collider.GetType() == typeof(Enemy))
            {
                GD.Print("Hit enemy");
                Enemy enemy = (Enemy)coll.Collider;
                enemy.hit(damage);
                QueueFree();
            }
        }
    }
}
