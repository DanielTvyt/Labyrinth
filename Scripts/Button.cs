using Godot;
using System;

public partial class Button : Godot.Button
{
    public override void _Ready()
    {
        GD.Print(".");
        
        this.Pressed += ButtonPressed;
        GD.Print(".");

    }

    private void ButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Blackjack.tscn");
    }
}

