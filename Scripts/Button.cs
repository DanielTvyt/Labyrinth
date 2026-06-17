using Godot;
using System;

public partial class Button : Godot.Button
{
    public override void _Ready()
    {
        this.Pressed += ButtonPressed;
    }

    private void ButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Blackjack.tscn");
    }
}

