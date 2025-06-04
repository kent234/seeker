using Godot;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		var startButton = GetNode<Button>("VBoxContainer/StartButton");
		var exitButton = GetNode<Button>("VBoxContainer/ExitButton");

		startButton.Pressed += OnStartButtonPressed;
		exitButton.Pressed += OnExitButtonPressed;
	}

	private void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://world.tscn"); // Переход на основную сцену
	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit(); // Выход из игры
	}
}
