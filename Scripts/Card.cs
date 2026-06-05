using Godot;
using System;

public partial class Card : Node3D
{
	public int Value;
	public int Suit;
	public bool IsFaceUp = false;
	public bool IsInDeck = true;

	[Export]
    private Sprite3D texture;

    public Card(int value, int suit, bool isFaceUp)
	{
		this.Value = value;
		this.Suit = suit;
		this.IsFaceUp = isFaceUp;
		texture.Texture = GetCardTexture();
    }

	private Texture2D GetCardTexture()
	{
		int id = Suit * 13 + Value;
		string path = $"res://assets/cards/{id}_kerenel_Cards.png";

        return GD.Load<Texture2D>(path);
    }

    public override void _Ready()
	{
		GD.Print("Card.cs is loading...");
        //GD.Print("Card.cs is ready! Value: " + Value + " Suit: " + Suit + " is face up? " + IsFaceUp);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

    }
}
