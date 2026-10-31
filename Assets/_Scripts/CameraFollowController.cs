using System;
using UnityEngine;

namespace Game
{
	public class CameraFollowController : MonoBehaviour
	{
		[SerializeField] private float _duration;
		[SerializeField] private Transform _playerTransform;

		private Vector3 _smoothingVelocity;
		private Transform _transform;

		private void Awake()
		{
			_transform = transform;
		}

		private void FixedUpdate()
		{
			if (Vector3.Distance(_playerTransform.position, _playerTransform.position) < 0.01)
			{
				_transform.position = Vector3.SmoothDamp(
					_transform.position,
					_playerTransform.position,
					ref _smoothingVelocity,
					_duration * Time.deltaTime);
			}
		}
	}
}
