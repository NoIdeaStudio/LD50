using Godot;
using System;

public class InteractionMenu : Node2D
{
    [Export] public StreamTexture UpgradeTexture;
    [Export] public StreamTexture InteractTexture;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Sprite>("UpgradeButton/Sprite").Texture = UpgradeTexture;
        GetNode<Sprite>("InteractButton/Sprite").Texture = InteractTexture;
    }

    public void hide()
    {
        this.Hide();
    }

    public void show()
    {
        this.Show();
    }

    public void _on_InteractButton_pressed()
    {
        Node2D parent = GetParent<Node2D>();

        if (parent.GetType() == typeof(Fabricator))
        {
            Fabricator f = (Fabricator)parent;
            f.interact();
        }
        else if (parent.GetType() == typeof(Charger))
        {
            Charger c = (Charger)parent;
            c.interact();
        }
        else if (parent.GetType() == typeof(Cannon))
        {
            Cannon c = (Cannon)parent;
            c.interact();
        }
        else if (parent.GetType() == typeof(ControlPanel))
        {
            ControlPanel c = (ControlPanel)parent;
            c.interact();
        }
    }

    public void _on_UpgradeButton_pressed()
    {
        Node2D parent = GetParent<Node2D>();

        if (parent.GetType() == typeof(Fabricator))
        {
            Fabricator f = (Fabricator)parent;
            f.upgrade();
        }
        else if (parent.GetType() == typeof(Charger))
        {
            Charger c = (Charger)parent;
            c.upgrade();
        }
        else if (parent.GetType() == typeof(Cannon))
        {
            Cannon c = (Cannon)parent;
            c.upgrade();
        }
        else if (parent.GetType() == typeof(ControlPanel))
        {
            ControlPanel c = (ControlPanel)parent;
            c.upgrade();
        }
    }
}
