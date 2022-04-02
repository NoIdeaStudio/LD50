using Godot;
using System;

public class Crosshair : Node2D
{
    Global global;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (global.shootMode){
            if (!this.Visible){
                this.Show();
            }

            this.Position = GetGlobalMousePosition();
        }else{
            if (this.Visible){
                this.Hide();
            }
        }
    }
}
