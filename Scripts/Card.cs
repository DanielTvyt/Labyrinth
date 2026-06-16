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

    public void Init(int value, int suit, bool isFaceUp)
	{
        //GD.Print("Card.cs is loading...");
        this.Value = value;
		this.Suit = suit;
		this.IsFaceUp = isFaceUp;
		texture.Texture = GetCardTexture();
    }

	public int GetValue()
	{
		if (Value > 10)
        {
            return 10;
        }
		if (Value == 1)
		{
            return 11;

        }
        return Value;
    }

	public void FlipCard()
    {
        IsFaceUp = true;
        texture.Texture = GetCardTexture();
    }

    private Texture2D GetCardTexture()
	{
        if (!IsFaceUp)
        {
            return GD.Load<Texture2D>("res://Assets/kerenel_Cards_seperated/CardFaceDown.png");
        }
		int id = Suit * 13 + Value + Suit;
		string path = $"res://Assets/kerenel_Cards_seperated/{id:00}_kerenel_Cards.png";
		//GD.Print($"Loading card texture from: {path}");
        return GD.Load<Texture2D>(path);
    }
    // Hart 1-13, Pik 15-27, diamonds 29-41, clubs 43-55


    public override void _Ready()
	{
        //GD.Print("Card.cs is ready! Value: " + Value + " Suit: " + Suit + " is face up? " + IsFaceUp);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

    }
}
