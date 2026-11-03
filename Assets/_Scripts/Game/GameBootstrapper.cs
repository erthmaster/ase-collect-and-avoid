using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
	[DefaultExecutionOrder(-10)]
	public class GameBootstrapper : MonoBehaviour
	{
		[SerializeField] private Transform _playerTransform;
		[SerializeField] private Transform _enemyAisTransform;
		[SerializeField] private Transform _patrolPointsParent;
		
		[SerializeField] private PlayerMovementController _playerMovementController;
		[SerializeField] private GameManager _gameManage;
		
		private PlayerInput _input;
		
		private void OnEnable()
		{
			_input.Enable();
		}

		private void OnDisable()
		{
			_input.Disable();
		}
		
		private void Awake()
		{
			_input = new();
			_playerMovementController.Init(_input.Player);
			_gameManage.Init(_input.UI);
			
			var points = _patrolPointsParent.Cast<Transform>().ToList();

			foreach (Transform enemy in _enemyAisTransform)
			{
				var ai = enemy.GetComponent<EnemyAI>();
				ai.Init(_playerTransform, points);
			}
		}
	}
}
