using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;

public partial class Enemy : CharacterBody3D
{
	[Export]
	float speed = 2.0f;

	[Export]
	Node3D target;

	NavigationAgent3D _navigationAgent;

    [Export]
    float spawnCooldown = 2f;

    public override void _PhysicsProcess(double delta)
    {
        if (spawnCooldown > 0)
            spawnCooldown -= (float) delta;


        _navigationAgent.TargetPosition = target.GlobalPosition;

        Vector3 currentLocation = GlobalTransform.Origin;
		Vector3 nextLocation = _navigationAgent.GetNextPathPosition();
		Vector3 direction = (nextLocation - currentLocation).Normalized();

        Velocity = direction * speed * (float)delta * 100;

        

        MoveAndSlide();
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            GD.Print("I collided with ", ((Node)collision.GetCollider()).Name);
            if(((Node)collision.GetCollider()).Name == "CharacterBody3D")
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
                GetTree().ChangeSceneToFile("res://Scenes/Blackjack.tscn");
            }
        }

        base._PhysicsProcess(delta);
    }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
