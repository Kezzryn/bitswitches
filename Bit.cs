using Godot;

public partial class Bit : ColorRect
{

	[Export]
	public Color onColor = Colors.Yellow;

	[Export]
	public Color offColor = Colors.Red;

	[Export]
	public int value = 0;

	private Label bitValue;
	private SignalBus bus;

	private bool isSolved = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		bus = GetNode<SignalBus>("/root/SignalBus");
		bus.ResetPuzzle += ResetPuzzle_Reciever;
		bus.SolvedPuzzle += SolvedPuzzle_Reciever;

		bitValue = GetNode<Label>("bitLabel");

		Color = offColor;
	}


	private void SolvedPuzzle_Reciever()
	{
		isSolved = true;
	}

	private void ResetPuzzle_Reciever()
	{
		Color = offColor;
		bitValue.Text = "0";
		isSolved = false;
	}

	public void OnGUIInput(InputEvent e)
	{
		if (isSolved) return;
		if (e is InputEventMouseButton && e.IsPressed())
		{
			if(this.Color == offColor)
			{
				bus.EmitSignal(SignalBus.SignalName.BitChanged, value);
				this.Color = onColor;
				bitValue.Text = value.ToString();
			}
			else
			{
				bus.EmitSignal(SignalBus.SignalName.BitChanged, -value);
				this.Color = offColor;
				bitValue.Text = "0";
			}
		}
	}
}
