using Godot;
using System;

public class Wave : Node2D
{
    public int number = 10;
    [Export] public int perRow = 5;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PackedScene enemyScene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");

        for (int i = 0; i < number; i++)
        {
            var enemy = (Enemy)enemyScene.Instance();
            enemy.Position = new Vector2(i % perRow * 100, i / perRow * 100);
            AddChild(enemy);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // move slowly down
        Position = new Vector2(Position.x, Position.y + .25f);
    }
}
