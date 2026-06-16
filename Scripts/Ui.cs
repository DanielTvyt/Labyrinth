using Godot;
using System;

public partial class Ui : Control
{
	[Export]
    Label playerScoreLabel;

    [Export]
    Label dealerScoreLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
    }

    public void UpdatePlayerScore(int score)
    {
        playerScoreLabel.Text = $"{score}";
        if (score == 21)
        {
            playerScoreLabel.AddThemeColorOverride("font_color", new Color(0, 1, 0));
        }
        if (score > 21)
        {
            playerScoreLabel.AddThemeColorOverride("font_color", new Color(1, 0, 0));
        }
    }

    public void UpdateDealerScore(int score)
    {
        dealerScoreLabel.Text = $"{score}";
        if (score == 21)
        {
            dealerScoreLabel.AddThemeColorOverride("font_color", new Color(0, 1, 0));
        }
        if (score > 21)
        {
            dealerScoreLabel.AddThemeColorOverride("font_color", new Color(1, 0, 0));
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
