using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

public partial class Blackjack : Node3D
{
    public const int PLAYER_PREP = 0;
    public const int DEALER_PREP = 1;
    public const int PLAYER_TURN = 2;
    public const int DEALER_TURN = 3;
    public const int GAME_OVER   = 4;

    public int playingMode = PLAYER_PREP;

    [Export]
    Control GameOverScreen;

    [Export]
	private Scores ui;

    [Export]
    private Flicker flicker;

    [Export]
    Camera camera;

    private PackedScene CardObject;

    private readonly Random rand = new();

    private bool[] Deck = new bool[52];

	private List<Card> playerCards = new();
	private List<Card> dealerCards = new();

	private float cardDelay = 0.5f;
    private float countdown = 1f;

    private float cardSpacing = 0.5f;

    private bool playerBusted = false;
    private bool dealerBusted = false;

    public override void _Ready()
	{
		GD.Print("Blackjack.cs is loading...");
        CardObject = GD.Load<PackedScene>("res://Scenes/card.tscn");

        for (int i = 0; i < Deck.Length; i++)
        {
            Deck[i] = true;
        }
        GD.Print("Blackjack.cs is ready!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (countdown > 0)
        {
            countdown -= (float)delta;
            return;
        }

        if (playingMode == PLAYER_PREP)
        {
            if (playerCards.Count > 0)
            {
                playingMode = DEALER_PREP;
            }
            PlayerDrawCard();
            countdown = cardDelay;
            return;
        }

        if (playingMode == DEALER_PREP)
        {
            if (dealerCards.Count > 0)
            {
                playingMode = PLAYER_TURN;
                DealerDrawCard(false);
                countdown = cardDelay;
                return;
            }
            DealerDrawCard();
            countdown = cardDelay;
            return;
        }

        if (playingMode == GAME_OVER)
        {
            GD.Print(IsPlayerWin() ? "Player wins!" : "Dealer wins!");
            playingMode = int.MaxValue;
            return;
        }

        if (playingMode == PLAYER_TURN)
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                PlayerDrawCard();
                countdown = cardDelay;
                return;
            }
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                playingMode = DEALER_TURN;
                return;
            }
        }

        if (playingMode == DEALER_TURN)
        {
            DealerTurn();
            countdown = cardDelay;
            return;
        }
    }

    private void DealerTurn()
    {
        foreach (Card card in dealerCards)
        {
            if (!card.IsFaceUp)
            {
                card.FlipCard();
                GetDealerScore();
                return;
            }
        }
        int score = GetDealerScore();

        if (score < 17)
        {
            DealerDrawCard();
            return;
        }
        playingMode = GAME_OVER;
    }

    private int GetPlayerScore()
    {
        int score = 0;
		int numAces = 0;
        foreach (Card card in playerCards)
        {
            if (card.GetValue() < 11)
			{
				score += card.GetValue();
				continue;
			}
			score++;
			numAces++;
        }
		if (numAces > 0 && score < 12)
		{
			score += 10;
        }

        GD.Print($"Player score: {score}");
		ui.UpdatePlayerScore(score);
        if (score > 21)
        {
            playerBusted = true;
            playingMode = GAME_OVER;
            return 0;
        }
        return score;
    }

    private int GetDealerScore()
    {
        int score = 0;
        int numAces = 0;
        foreach (Card card in dealerCards)
        {
            if (!card.IsFaceUp)
            {
                if (card.GetValue() + score == 21)
                {
                    card.FlipCard();
                    GetDealerScore();
                }
                continue;
            }
            if (card.GetValue() < 11)
            {
                score += card.GetValue();
                continue;
            }
            score++;
            numAces++;
        }
        if (numAces > 0 && score < 12)
        {
            score += 10;
        }

        ui.UpdateDealerScore(score);
        if (score > 21)
        {
            dealerBusted = true;
            playingMode = GAME_OVER;
        }
        return score;
    }

    private bool IsPlayerWin()
    {
        if (GetDealerScore() > GetPlayerScore() && !dealerBusted || playerBusted)
        {
            flicker.PlayerLost();
            camera.PivotUp();
            GameOverScreen.Visible = true;
            return false;
        }
        flicker.PlayerWon();

        GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");

        return true;
    }

    private void PlayerDrawCard()
    {
		Vector3 position = new((playerCards.Count * 0.3f) - 0.7f, -0.8f, playerCards.Count * 0.01f);
        playerCards.Add(DrawCard(position));
        int score = GetPlayerScore();
        if (score == 21)
        {
            playingMode = GAME_OVER;
        }
    }

    private void DealerDrawCard(bool isFaceUp = true)
    {
        Vector3 position = new((dealerCards.Count * 0.3f) - 0.7f, 0.8f, dealerCards.Count * 0.01f);
        dealerCards.Add(DrawCard(position, isFaceUp));
        GD.Print($"Dealer score: {GetDealerScore()}");
    }

    private Card DrawCard(Vector3 position, bool isFaceUp = true)
	{
        while (true)
		{
			Card card = CardObject.Instantiate<Card>();

            int suit = rand.Next(0, 4);
			int value = rand.Next(1, 13);

			int cardId = GetCardId(suit, value);

            if (Deck[cardId])
			{
				Deck[cardId] = false;

				card.Init(value, suit, isFaceUp);

				card.Position = position;

				card.RotateZ((float)((rand.NextDouble() - 0.5) * 0.1));

				this.AddChild(card);
				//GD.Print($"Drew card: {card.Value} of {card.Suit}");
				return card;
			}
		}
	}

	private int GetCardId(int suit, int value)
	{
		return suit * 13 + value;
    }
	
}
