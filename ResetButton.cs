using Godot;

public partial class ResetButton : Button
{
	private SignalBus bus;
	public override void _Ready()
	{
		bus = GetNode<SignalBus>("/root/SignalBus");
		bus.SolvedPuzzle += SolvedPuzzle_Reciever;
		this.Visible = false;
	}

	public void SolvedPuzzle_Reciever()
	{
		this.Visible = true;
	}

	public void OnPressed()
	{
		bus.EmitSignal(SignalBus.SignalName.ResetPuzzle);
		this.Visible = false;
	}
}
