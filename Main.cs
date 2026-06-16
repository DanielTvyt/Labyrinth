using Godot;
using System;

public partial class Main : Node3D
{
	[Export]
	public bool capturecam = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("keypress_escape"))
		{
			capturecam = false;
		}
		if (capturecam == false)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
			GetTree().ChangeSceneToFile("res://Scenes/Blackjack.tscn");
        }
	}
}
