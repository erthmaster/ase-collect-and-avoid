using System;
using UnityEngine;

namespace Game
{
	public class PlayerMovementController : MonoBehaviour
	{
		[SerializeField] private float _moveVelocity;
		
		[Header("Dash")]
		[SerializeField] private float _dashTime;
		[SerializeField] private float _dashDistance;
		[SerializeField] private float _dashCooldown;
		
		private Vector3 _moveDirection;
		private Rigidbody _rigidbody;
		private PlayerInput _playerInput;

		private float _currentDashCooldown;
		private float _currentDashTime;
		private bool _isDashing;
		private Vector3 _moveDirectionBeforeDash;

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

		private void Update()
		{
			if (_playerInput.Player.Dash.WasPressedThisFrame() && _currentDashCooldown !<= 0)
			{
				_isDashing = true;
				_currentDashTime = _dashTime;
			}

			_currentDashCooldown -= Time.deltaTime;
		}

		private void FixedUpdate()
		{
			_moveDirection = _playerInput.Player.Move.ReadValue<Vector2>().normalized;

			if (!_isDashing)
			{
				_moveDirectionBeforeDash = _moveDirection;
				_rigidbody.linearVelocity = new Vector3(_moveDirection.x, 0, _moveDirection.y) * _moveVelocity;
			}
			else
			{
				_rigidbody.linearVelocity = new Vector3(_moveDirectionBeforeDash.x, 0, _moveDirectionBeforeDash.y) * (_dashDistance/_dashTime);
				_currentDashTime -= Time.deltaTime;

				if (_currentDashTime <= 0)
				{
					_isDashing = false;
					_currentDashCooldown = _dashCooldown;
				}
			}
		}
	}
}
