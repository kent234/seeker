using Godot;
public partial class Item : Area3D
{
	private bool _playerInside = false;
	public override void _Ready()
	{
		this.Connect(SignalName.BodyEntered, new Callable(this, nameof(OnBodyEntered)));
		this.Connect(SignalName.BodyExited, new Callable(this, nameof(OnBodyExited)));
	}
	private void OnBodyEntered(Node3D body)
	{
		if (body.Name == "Player")
		{
			_playerInside = true;
		}
	}
	private void OnBodyExited(Node3D body)
	{
		if (body.Name == "Player")
		{
			_playerInside = false;
		}
	}
	public override void _Process(double delta)
	{
		if (_playerInside && Input.IsKeyPressed(Key.E))
		{
			GD.Print("Item collected: " + Name);
			QueueFree();
		}
	}
}
