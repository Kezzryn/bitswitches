using Godot;

public partial class BitFlipper : Node2D
{
	private int total;
	private int secretNumber;
	private readonly RandomNumberGenerator rng = new();

	private SignalBus bus;

	private Label lblTotal;
	private Label lblGrats;
	private Label lblHint;

	public override void _Ready()
	{
		bus = GetNode<SignalBus>("/root/SignalBus");
		
		lblTotal = GetNode<Label>("lblTotal");
		lblHint = GetNode<Label>("lblHint");
		lblGrats = GetNode<Label>("lblGrats");

		bus.BitChanged += BitChanged_Reciever;
		bus.ResetPuzzle += ResetPuzzle_Reciever;

		PickNumber();
	}

	private void BitChanged_Reciever(int amount)
	{
		total += amount;
		UpdateTotalDisplay();
		if (total == secretNumber)
		{
			SolvedPuzzle();
		}
	}

	private void ResetPuzzle_Reciever()
	{
		total = 0;
		lblGrats.Visible = false;
		UpdateTotalDisplay();
		PickNumber();
	}

	private void PickNumber()
	{
		secretNumber = rng.RandiRange(1, 255);
		lblHint.Text = secretNumber.ToString();
	}

	private void SolvedPuzzle()
	{
		lblGrats.Visible = true;
		bus.EmitSignal(SignalBus.SignalName.SolvedPuzzle);
	}

	private void UpdateTotalDisplay()
	{
		lblTotal.Text = total.ToString();
	}
}
