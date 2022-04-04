using Godot;
using System;

public class WaveSpawner : Path2D
{
    [Export] public int numEnemies = 0;
    [Export] public int speed = 250;
    [Export] public int health = 1;
    [Export] public float interval = 1;
    [Export] public String animation = "var1";
    
    Global global;

    Timer timer;
    PackedScene enemyScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");

        enemyScene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");

        timer = GetNode<Timer>("Timer");
        timer.WaitTime = interval;
        timer.Connect("timeout", this, nameof(SpawnEnemy));
        timer.Start();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void SpawnEnemy(){
        if (numEnemies <= 0){
            return;
        }
        var enemy = (Enemy)enemyScene.Instance();
        enemy.GlobalPosition = GlobalPosition;
        enemy.speed = speed;
        enemy.health = global.enemyHealth;
        AddChild(enemy);
        enemy.setAnimation(animation);
        numEnemies--;
        global.enemiesLeft++;
    }
}
