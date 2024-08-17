using Godot;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void BitChangedEventHandler(int value);

	[Signal]
	public delegate void ResetPuzzleEventHandler();

	[Signal]
	public delegate void SolvedPuzzleEventHandler();
}
