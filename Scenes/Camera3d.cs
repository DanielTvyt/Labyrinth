using Godot;
using System;

public partial class Camera3d : Camera3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
    }
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            // Get the relative movement of the mouse
            Vector2 mouse = mouseMotion.Relative;
            // Print the mouse movement to the console
            //GD.Print("Mouse moved: ", mouse);
            //GD.Print("Rotation: ", Rotation.X);
            mouse.Normalized();
            //hoch runter
            if (Rotation.X < 1 && Rotation.X > -1.25)
            {
                RotateObjectLocal(new Vector3(-1, 0, 0), mouse.Y * 0.01f);
            }
            else if(Rotation.X < 1)
            {
                RotateObjectLocal(new Vector3(-1, 0, 0), -0.1f);
            }
            else
            {
                RotateObjectLocal(new Vector3(-1, 0, 0), 0.1f);
            }
            }
        }
}
