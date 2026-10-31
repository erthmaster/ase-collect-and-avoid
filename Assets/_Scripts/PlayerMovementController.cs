using System;
using UnityEngine;

namespace Game
{
	public class PlayerMovementController : MonoBehaviour
	{
		[SerializeField] private float _moveVelocity;
		
		private Rigidbody _rigidbody;
		private PlayerInput _playerInput;

		private void Awake()
		{
			_playerInput = new();
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void OnEnable()
		{
			_playerInput.Enable();
		}

		private void OnDisable()
		{
			_playerInput.Disable();
		}

		private void FixedUpdate()
		{
			var moveDir = _playerInput.Player.Move.ReadValue<Vector2>().normalized;
			_rigidbody.linearVelocity = new Vector3(moveDir.x, 0, moveDir.y) * _moveVelocity;
		}
	}
}
