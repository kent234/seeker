using Godot;

public partial class Player : CharacterBody3D
{
	public float Speed = 2.0f; // Скорость передвижения
	public float JumpStrength = 4.5f; // Сила прыжка (невысокий прыжок)
	public float Gravity = 9.8f; // Гравитация
	public float MouseSensitivity = 0.005f; // Чувствительность мыши
	private Camera3D _camera;

	public override void _Ready()
	{
		_camera = GetNode<Camera3D>("Camera3D");
		Input.MouseMode = Input.MouseModeEnum.Captured; // Захват мыши для вида от первого лица
		GD.Print("Player is ready!");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Применение гравитации
		if (!IsOnFloor())
			velocity.Y -= Gravity * (float)delta;

		// Прыжок
		if (Input.IsKeyPressed(Key.Space) && IsOnFloor())
			velocity.Y = JumpStrength;

		// Движение WASD
		Vector3 direction = Vector3.Zero;
		if (Input.IsKeyPressed(Key.W))
			direction -= Transform.Basis.Z; // Вперед
		if (Input.IsKeyPressed(Key.S))
			direction += Transform.Basis.Z; // Назад
		if (Input.IsKeyPressed(Key.A))
			direction -= Transform.Basis.X; // Влево
		if (Input.IsKeyPressed(Key.D))
			direction += Transform.Basis.X; // Вправо

		// Нормализация направления, чтобы избежать ускорения по диагонали
		direction = direction.Normalized();

		// Применение скорости
		velocity.X = direction.X * Speed;
		velocity.Z = direction.Z * Speed;
		

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			// Вращение игрока (по оси Y) и камеры (по оси X)
			RotateY(-mouseMotion.Relative.X * MouseSensitivity);
			_camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);
			_camera.RotationDegrees = new Vector3(
				Mathf.Clamp(_camera.RotationDegrees.X, -70, 70),
				_camera.RotationDegrees.Y,
				_camera.RotationDegrees.Z
			);
		}
	}
}
