using Godot;
using System;

public class Player : KinematicBody2D
{
    private Vector2 velocity = new Vector2();
    [Export] public int speed = 1000;
    public int maxSpeed = 4500;
    public AnimatedSprite anim;
    public bool right = false;
    public bool working = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        anim = GetNode<AnimatedSprite>("Body");
        anim.Play("Idle");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // get inputs
        if (Input.IsActionPressed("right"))
        {
            right = true;
            if (!anim.FlipH)
            {
                anim.FlipH = true;
            }
            if (velocity.x > 10)
            {
                velocity.x = 10;
            }else{
                velocity.x = speed * delta;
            }
            
        }
        else if (Input.IsActionPressed("left"))
        {
            right = false;
            if (anim.FlipH)
            {
                anim.FlipH = false;
            }
            if (velocity.x < -10)
            {
                velocity.x = -10;
            }else{
                velocity.x  = -(speed * delta);
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

        if ((Mathf.Abs(velocity.x) > 0 || Mathf.Abs(velocity.y) > 0) && anim.Animation != "Walk"){
            anim.Play("Walk");
        }else if (Mathf.Abs(velocity.x) == 0 && Mathf.Abs(velocity.y) == 0 && anim.Animation != "Idle" && !working){
            anim.Play("Idle");
        }else if (Mathf.Abs(velocity.x) == 0 && Mathf.Abs(velocity.y) == 0 && working && anim.Animation != "Work"){
            anim.Play("Work");
        }

        // apply velocity
        MoveAndCollide(velocity);
    }

    public void upgradeSpeed(){
        speed += 500;
    }
    
}
