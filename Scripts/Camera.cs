using Godot;
using System;
using System.Formats.Tar;

public partial class Camera : Camera3D
{
	private float targeAngle = 0;
	private float stepSize = 0.01f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (targeAngle > 0)
		{
            if (Rotation.X < targeAngle)
				RotateX(stepSize);
		}
	}

	public void PivotUp()
	{
		targeAngle = 1.8f;
	}
}
