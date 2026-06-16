using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;

public partial class Enemy : CharacterBody3D
{
	[Export]
	private NavigationAgent3D _navigationAgent;

	[Export]
	float speed = 2.0f;


    public override void _PhysicsProcess(double delta)
    {
		Vector3 currentLocation = GlobalTransform.Origin;
		Vector3 nextLocation = _navigationAgent.GetNextPathPosition();
		Vector3 direction = (nextLocation - currentLocation).Normalized();
        base._PhysicsProcess(delta);
    }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
