using Godot;
using System;

public class Global : Node
{
    public int Iron = 0;
    public int Energy = 0;

    public Overlay overlay;
    public bool shootMode = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void addIron(int amount){
        GD.Print("Adding Iron");
        Iron += amount;
        overlay.setIron(Iron);
    }

    public void addEnergy(int amount){
        GD.Print("Adding Energy");
        Energy += amount;
        overlay.setEnergy(Energy);
    }

    public void removeIron(int amount){
        GD.Print("Removing Iron");
        Iron -= amount;
        overlay.setIron(Iron);
    }

    public void removeEnergy(int amount){
        GD.Print("Removing Energy");
        Energy -= amount;
        overlay.setEnergy(Energy);
    }
}
