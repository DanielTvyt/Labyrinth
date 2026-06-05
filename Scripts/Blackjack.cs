using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class Blackjack : Node3D
{
	private readonly Random rand = new();

    private Card[] Cards = new Card[52];
	[Export]
	private PackedScene CardObject;

	private int i = 0;

	public override void _Ready()
	{
		GD.Print("Blackjack.cs is loading...");
		GD.Load<PackedScene>("res://Scenes/card.tscn");

		var card = CardObject.Instantiate();

		

		this.AddChild(card);

        /*
        for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 13; j++)
			{
				Cards[i * 13 + j] = new Card(j + 1, i, false);
			}
		}
		*/

        //Card card = CardObject.Instantiate<Card>();
        //this.AddChild(card);


        GD.Print("Blackjack.cs is ready!");
		//DrawCard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private Card DrawCard()
	{
		while (true)
		{
			Card card = GetCard(rand.Next(0, 4), rand.Next(0, 13));
			if (card.IsInDeck)
			{
				card.IsInDeck = false;
				AddChild(card);
				card.Show(); //Might not be needed
			    GD.Print($"Drew card: {card.Value} of {card.Suit}");
                return card;
			}
		}
	}

	private Card GetCard(int suit, int value)
	{
		return Cards[suit * 13 + value];
    }
}
