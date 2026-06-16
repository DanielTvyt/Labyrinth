using Godot;
using System;

public partial class Flicker : OmniLight3D
{
    [Export]
    private float flickerIntensity = 0.25f;

    Random rand = new Random();

    bool isFlickering = true;

    float lightDelta = 0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		this.LightEnergy = 10f;
		
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (isFlickering)
        {
            lightDelta += ((float)rand.NextDouble() - 0.5f) * flickerIntensity;
            if (lightDelta > 15f)
            {
                lightDelta = 15f;
            }
            if (lightDelta < -9f)
            {
                lightDelta = -9f;
            }
            LightEnergy = 10f + lightDelta;
            return;
        }
    }

    public void PlayerWon()
    {
        LightEnergy = 10f;
        LightColor = new Color(0.1f, 1f, 0.1f);
        isFlickering = false;
    }
    public void PlayerLost()
    {
        LightEnergy = 10f;
        LightColor = new Color(1f, 0.1f, 0.1f);
        flickerIntensity = 1f;
        isFlickering = true;
    }
}
