using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Scores : Node3D
{
	[Export]
	Label3D playerScoreLabel;

    [Export]
    Label3D dealerScoreLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}


    public void UpdatePlayerScore(int score)
    {
        playerScoreLabel.Text = $"{score}";
        if (score == 21)
        {
            playerScoreLabel.Modulate = new Color(0, 1, 0);
        }
        if (score > 21)
        {
            playerScoreLabel.Modulate = new Color(1, 0, 0);
        }
    }

    public void UpdateDealerScore(int score)
    {
        dealerScoreLabel.Text = $"{score}";
        if (score == 21)
        {
            dealerScoreLabel.Modulate = new Color(0, 1, 0);
        }
        if (score > 21)
        {
            dealerScoreLabel.Modulate = new Color(1, 0, 0);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
