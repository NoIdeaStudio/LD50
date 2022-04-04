using Godot;
using System;

public class World : Camera2D
{
    PackedScene enemyScene;
    public Timer enemySpawnTimer;
    Path2D currentPath;
    int enemyCount = 0;
    Random random;
    [Export] float decay = 0.8f;
    [Export] Vector2 maxOffset = new Vector2(100, 75);
    [Export] float maxRoll = 0.1f;
    [Export] NodePath target;

    float trauma = 0.0f;
    float trauma_power = 2;
    Area2D shield;
    Global global;
    public int wave = 0;
    public int enemiesLeft = 0;
    public WaveSpawner[] paths = new WaveSpawner[3];
    public bool tutorial = true;
    public Sprite TutorialFabricator;
    public Sprite TutorialCharger;
    public Sprite TutorialCannon;
    public Sprite TutorialControlPanel;
    public ShieldSprite shieldSprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        global = GetNode<Global>("/root/Global");
        global.Setup();

        random = new Random();
        enemyScene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");

        enemySpawnTimer = GetNode<Timer>("EnemySpawnTimer");
        enemySpawnTimer.Connect("timeout", this, "_on_EnemySpawnTimer_timeout");

        shield = GetNode<Area2D>("Shield");
        shield.Connect("body_entered", this, "_on_Shield_body_entered");

        TutorialFabricator = GetNode<Sprite>("TutorialFabricator");
        TutorialCharger = GetNode<Sprite>("TutorialCharger");
        TutorialCannon = GetNode<Sprite>("TutorialCannon");
        TutorialControlPanel = GetNode<Sprite>("TutorialControlPanel");
        shieldSprite = GetNode<ShieldSprite>("ShieldSprite");


        
        paths[0] = GetNode<WaveSpawner>("Path1");
        paths[1] = GetNode<WaveSpawner>("Path2");
        paths[2] = GetNode<WaveSpawner>("Path3");

        global.startTime = DateTime.Now;

        if (global.tutorial){
            TutorialFabricator.Show();
        }else{
            enemySpawnTimer.Start();
        }

        


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
        global.countdown = (int)enemySpawnTimer.WaitTime;
        for (int i = 0; i < wave; i++){
            paths[i % paths.Length].numEnemies += wave;
        }

        enemySpawnTimer.WaitTime = 30 + wave*4;
        
    }

    public void _on_EnemySpawnTimer_timeout(){
        wave++;
        // every 5 waves increase global.enemyhealth
        if (wave % 5 == 0){
            global.enemyHealth++;
        }
        startWave();
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
            global.kills--;
            global.removeHealth(10);
            shieldSprite.hit();
    }
}
