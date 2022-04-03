using Godot;
using System;

public class World : Camera2D
{
    PackedScene enemyScene;
    Timer enemySpawnTimer;
    Path2D currentPath;
    int enemyCount = 0;
    Path2D[] paths;
    Random random;
    [Export] float decay = 0.8f;
    [Export] Vector2 maxOffset = new Vector2(100, 75);
    [Export] float maxRoll = 0.1f;
    [Export] NodePath target;

    float trauma = 0.0f;
    float trauma_power = 2;
    Area2D shield;
    Global global;
    public int wave = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");

        random = new Random();
        enemyScene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");

        enemySpawnTimer = GetNode<Timer>("EnemySpawnTimer");
        enemySpawnTimer.Connect("timeout", this, "_on_EnemySpawnTimer_timeout");

        shield = GetNode<Area2D>("Shield");
        shield.Connect("body_entered", this, "_on_Shield_body_entered");

        paths = new Path2D[3];
        paths[0] = GetNode<Path2D>("Path1");
        paths[1] = GetNode<Path2D>("Path2");
        paths[2] = GetNode<Path2D>("Path3");

        global.startTime = DateTime.Now;

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (target != null){
            GlobalPosition = GetNode<Node2D>(target).GlobalPosition;
        }
        if (trauma > 0){
            trauma = Mathf.Max(trauma - decay * delta, 0);
            shake();
        }
    }

    public void startWave(){
        int numWaves = wave;
        int numEnemies = numWaves * 5;

        for (int i = 0; i < numWaves; i++){
            CreateWave(numEnemies, i);
        }
    }

    public void CreateWave(int count, int path){
        // random path from paths
        currentPath = paths[path % paths.Length];
        enemySpawnTimer.Start();
        enemyCount = count;
    }

    public void _on_EnemySpawnTimer_timeout(){
        enemyCount--;
        if (enemyCount <= 0){
            enemySpawnTimer.Stop();
            return;
        }
        var enemy = enemyScene.Instance() as Enemy;
        currentPath.AddChild(enemy);
    }

    public void shake(){
        var amount = Mathf.Pow(trauma, trauma_power);
        Offset = new Vector2(maxOffset.x * amount * (float)random.NextDouble(), maxOffset.y * amount * (float)random.NextDouble());
    }

    public void addTrauma(float amount){
        trauma = Mathf.Min(trauma + amount, 1.0f);
    }

    public void _on_Shield_body_entered(object body){
            GD.Print("hit");
            addTrauma(0.3f);
            ((body as KinematicBody2D).GetParent() as Enemy).hit(100);
            global.removeHealth(10);
    }
}
