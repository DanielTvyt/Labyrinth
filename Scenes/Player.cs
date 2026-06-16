using Godot;

public partial class Player : CharacterBody3D
{
    public Vector2 mouse = new Vector2(0, 0);
    // How fast the player moves in meters per second.
    [Export]
    public int Speed { get; set; } = 14;
    // The downward acceleration when in the air, in meters per second squared.
    [Export]
    public int FallAcceleration { get; set; } = 75;

    private Vector3 _targetVelocity = Vector3.Zero;

    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector3.Zero;

        if (Input.IsActionPressed("keypress_d"))
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionPressed("keypress_a"))
        {
            direction.X -= 1.0f;
        }
        if (Input.IsActionPressed("keypress_s"))
        {
            direction.Z += 1.0f;
        }
        if (Input.IsActionPressed("keypress_w"))
        {
            direction.Z -= 1.0f;
        }
        if (Input.IsActionPressed("keypress_space") && IsOnFloor())
        {
            direction.Y += 1.0f;
        }

        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            // Setting the basis property will affect the rotation of the node.
            //GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
        }

        if (IsOnFloor()) _targetVelocity.Y = 0;
        // Ground velocity
        _targetVelocity.X = direction.X * Speed;
        _targetVelocity.Y += direction.Y * 2 * Speed;
        _targetVelocity.Z = direction.Z * Speed;

        // Vertical velocity
        if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
        {
            _targetVelocity.Y -= FallAcceleration * (float)delta;
        }
        
        // Moving the character
        Velocity = _targetVelocity.Rotated(Vector3.ModelTop, Rotation.Y);
        MoveAndSlide();
    }
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {       
            // Get the relative movement of the mouse
            mouse = mouseMotion.Relative;
            // Print the mouse movement to the console
            //GD.Print("Mouse moved: ", mouse);
            mouse.Normalized();
            //seitlich
            RotateObjectLocal(new Vector3(0, -1, 0), mouse.X * 0.01f);
        }
    }
    
}