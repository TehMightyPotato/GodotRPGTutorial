using Godot;
using System;

public partial class Player : CharacterBody2D
{
	/// <summary>
	/// The players move speed
	/// </summary>
	[Export] private float _speed = 25.0f;

	/// <summary>
	/// The animation player handling the player animation
	/// </summary>
	[Export] private AnimationPlayer _animationPlayer;

	private void HandleInput()
	{
		var vec = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = vec * _speed;
	}

	private void UpdateAnimation()
	{
		if (Velocity.Length() == 0)
		{
			if (_animationPlayer.IsPlaying())
				_animationPlayer.Stop();
			return;
		}

		var direction = "down";
		if (Velocity.X < 0)
		{
			direction = "left";
		}
		else if (Velocity.X > 0)
		{
			direction = "right";
		}
		else if (Velocity.Y < 0)
		{
			direction = "up";
		}

		_animationPlayer.Play("player_move_" + direction);
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleInput();
		MoveAndSlide();
		UpdateAnimation();
	}
}
