using Godot;
using System;

public class Cannon : StaticBody2D
{   
    private Sprite cannonTop;

    private InteractionMenu menu;

    Global global;

    [Export] public bool flipped = false;

    public PackedScene bullet;

    private Position2D bulletSpawner;
    public int maxAmmo = 10;
    public int currentAmmo = 10;
    Label ammoLabel;

    Timer timer;
    Timer chargeTimer;
    private bool canShoot = true;
    [Export] public string shootAction = "shoot";

    PackedScene EnergyPlus;
    PackedScene EnergyMinus;
    public bool tripple = false;
    public bool five = false;
    public int damage = 1;
    public int bulletSpeed = 500;
    public int piercing = 0;
    public int chargeTime = 1;
    public PackedScene ShootParticlesScene;
    [Export] public String UpgradeMenu = "CannonUpgradeMenu";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Area2D>("Area2D").Connect("body_entered", this, "_on_Cannon_body_entered");
        GetNode<Area2D>("Area2D").Connect("body_exited", this, "_on_Cannon_body_exited");

        cannonTop = GetNode<Sprite>("CannonTop");
        menu = GetNode<InteractionMenu>("InteractionMenu");
        global = GetNode<Global>("/root/Global");
        bulletSpawner = cannonTop.GetNode<Position2D>("BulletSpawner");
        timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, "_on_Timer_timeout");
        ammoLabel = GetNode<Label>("AmmoLabel");
        ammoLabel.Text = currentAmmo.ToString() + " /" + maxAmmo.ToString();
        chargeTimer = GetNode<Timer>("ChargeTimer");
        chargeTimer.Connect("timeout", this, "_on_ChargeTimer_timeout");
        EnergyPlus = GD.Load<PackedScene>("res://Scenes/EnergyPlus.tscn");
        EnergyMinus = GD.Load<PackedScene>("res://Scenes/EnergyMinus.tscn");
        ShootParticlesScene = GD.Load<PackedScene>("res://Scenes/ShootParticles.tscn");

        // preload bullet
        bullet = (PackedScene)ResourceLoader.Load("res://Scenes/Bullet.tscn");

        if (flipped)
        {
            this.Scale *= new Vector2(-1, 1);
            ammoLabel.RectScale *= new Vector2(-1, 1);
            ammoLabel.RectPosition = new Vector2(ammoLabel.RectPosition.x + 28, ammoLabel.RectPosition.y);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (global.shootMode){
            // get mouse position
            Vector2 mousePos = GetGlobalMousePosition();
            lookAt(mousePos);
        }
    }

    private void lookAt(Vector2 look){
        Vector2 direction;
        if (!flipped){
            direction = cannonTop.GlobalPosition - look;
        }else{
            direction = look - cannonTop.GlobalPosition;
        }
        float angle = direction.Angle();

        if (flipped){
            angle = -angle;
        }
        cannonTop.Rotation = angle;
    }
    public void _on_Cannon_body_entered(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.show();
            if (global.tutorial && ((World)GetParent()).TutorialCannon.Visible == true){
                ((World)GetParent()).TutorialCannon.Hide();
                ((World)GetParent()).TutorialControlPanel.Show();
                ((World)GetParent()).wave = 1;
                ((World)GetParent()).startWave();
                ((World)GetParent()).enemySpawnTimer.Start();
            }
        }
    }

    public void _on_Cannon_body_exited(object body)
    {
        if (body.GetType() == typeof(Player))
        {
            menu.hide();
            chargeTimer.Stop();
            GetParent().GetNode<CannonUpgradeMenu>(UpgradeMenu).Hide();
            GetParent().GetNode<Player>("Player").working = false;
        }
    }

    public void interact(){
        menu.hide();
        chargeTimer.Start();
        GetParent().GetNode<Player>("Player").working = true;
    }

    public void _on_ChargeTimer_timeout(){
        if (global.Energy <= 0){
            return;
        }
        if (currentAmmo < maxAmmo){
            currentAmmo++;
            ammoLabel.Text = currentAmmo.ToString() + " /" + maxAmmo.ToString();
            global.removeEnergy(1);
            var energy = (EnergyPlus)EnergyPlus.Instance();
            energy.Position = bulletSpawner.GlobalPosition;
            GetParent().AddChild(energy);
        }
    }

    public void upgrade(){
        GetParent().GetNode<CannonUpgradeMenu>(UpgradeMenu).CurrentCannon = this;
        GetParent().GetNode<CannonUpgradeMenu>(UpgradeMenu).Show();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(shootAction))
        {
            if (global.shootMode){
                if (canShoot && currentAmmo > 0){
                    shoot();
                }
                timer.Start();
            }
        }else if (@event.IsActionReleased(shootAction)){
            if (global.shootMode){
                timer.Stop();
                canShoot = true;
            }
        }
    }

    public void _on_Timer_timeout()
    {
        canShoot = true;
        if (currentAmmo > 0){
            shoot();
        }
    }

    private void shoot(){
        canShoot = false;
        currentAmmo--;
        ammoLabel.Text = currentAmmo.ToString() + " /" + maxAmmo.ToString();
        ((World)GetParent()).addTrauma(0.2f);
        GetNode<AnimationPlayer>("AnimationPlayer").Stop();
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        var energyminus = (EnergyPlus)EnergyMinus.Instance();
        energyminus.Position = bulletSpawner.GlobalPosition;
        GetParent().AddChild(energyminus);
        var shootParticlesInstance = (ShootParticles)ShootParticlesScene.Instance();
        shootParticlesInstance.Position = bulletSpawner.GlobalPosition;
        shootParticlesInstance.Rotation = cannonTop.GlobalRotation;
        shootParticlesInstance.Scale *= new Vector2(-1, 1);
        GetParent().AddChild(shootParticlesInstance);

        GD.Print("shoot");
        Bullet bulletInstance = (Bullet)bullet.Instance();
        bulletInstance.Rotation = cannonTop.Rotation;
        bulletInstance.Position = bulletSpawner.GlobalPosition;

        bulletInstance.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation), -Mathf.Sin(cannonTop.Rotation));

        if (flipped){
            bulletInstance.direction.x *= -1;
        }

        bulletInstance.damage = damage;
        bulletInstance.piercing = piercing;
        bulletInstance.speed = bulletSpeed;
        GetParent().AddChild(bulletInstance);

        if (tripple){
            Bullet bulletInstance2 = (Bullet)bullet.Instance();
            bulletInstance2.Rotation = cannonTop.Rotation;
            bulletInstance2.Position = bulletSpawner.GlobalPosition;

            bulletInstance2.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation - 25), -Mathf.Sin(cannonTop.Rotation - 25));

            if (flipped){
                bulletInstance2.direction.x *= -1;
            }

            Bullet bulletInstance3 = (Bullet)bullet.Instance();
            bulletInstance3.Rotation = cannonTop.Rotation;
            bulletInstance3.Position = bulletSpawner.GlobalPosition;

            bulletInstance3.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation + 25), -Mathf.Sin(cannonTop.Rotation + 25));

            if (flipped){
                bulletInstance3.direction.x *= -1;
            }

            bulletInstance2.damage = damage;
            bulletInstance2.piercing = piercing;
            bulletInstance2.speed = bulletSpeed;

            bulletInstance3.damage = damage;
            bulletInstance3.piercing = piercing;
            bulletInstance3.speed = bulletSpeed;

            GetParent().AddChild(bulletInstance2);
            GetParent().AddChild(bulletInstance3);
        }

        if (five){
            Bullet bulletInstance4 = (Bullet)bullet.Instance();
            bulletInstance4.Rotation = cannonTop.Rotation;
            bulletInstance4.Position = bulletSpawner.GlobalPosition;

            bulletInstance4.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation - 50), -Mathf.Sin(cannonTop.Rotation - 50));

            if (flipped){
                bulletInstance4.direction.x *= -1;
            }

            Bullet bulletInstance5 = (Bullet)bullet.Instance();
            bulletInstance5.Rotation = cannonTop.Rotation;
            bulletInstance5.Position = bulletSpawner.GlobalPosition;

            bulletInstance5.direction = new Vector2(-Mathf.Cos(cannonTop.Rotation + 50), -Mathf.Sin(cannonTop.Rotation + 50));

            if (flipped){
                bulletInstance5.direction.x *= -1;
            }

            bulletInstance4.damage = damage;
            bulletInstance4.piercing = piercing;
            bulletInstance4.speed = bulletSpeed;

            bulletInstance5.damage = damage;
            bulletInstance5.piercing = piercing;
            bulletInstance5.speed = bulletSpeed;

            GetParent().AddChild(bulletInstance4);
            GetParent().AddChild(bulletInstance5);
        }

        

        GetNode<AnimationPlayer>("AnimationPlayer").Play("Recoil");
    }

    public void UpgradeDamage(){
        damage += 1;
    }

    public void UpgradePiercing(){
        piercing += 1;
    }

    public void UpgradeVelocity(){
        bulletSpeed += 250;
    }

    public void UpgradeMaxCharge(){
        maxAmmo += 5;
    }

    public void UpgradeCharging(){
        chargeTime += 1;
        chargeTimer.WaitTime = 1f/chargeTime;
    }

    public void UpgradeTriple(){
        tripple = true;
    }

    public void UpgradeFive(){
        five = true;
    }


}
