using Godot;
using System;

public class Player : KinematicBody2D
{
    private Vector2 velocity = new Vector2();
    [Export] private int speed = 2000;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // get inputs
        if (Input.IsActionPressed("right"))
        {
            if (velocity.x > 10)
            {
                velocity.x = 10;
            }else{
                velocity.x += speed * delta;
            }
            
        }
        else if (Input.IsActionPressed("left"))
        {
            if (velocity.x < -10)
            {
                velocity.x = -10;
            }else{
                velocity.x -= speed * delta;
            }
        }else{
            velocity.x = 0;
        }

        // gravity
        // if not touching ground
        // if (!IsOnFloor())
        // {
        //     velocity.y += 1;
        // }else{
        //     velocity.y = 0;
        // }

        // apply velocity
        MoveAndCollide(velocity);
    }
}
