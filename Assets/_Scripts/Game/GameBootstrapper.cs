using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
	[DefaultExecutionOrder(-100)]
	public class GameBootstrapper : MonoBehaviour
	{
		[SerializeField] private Transform _playerTransform;
		[SerializeField] private Transform _enemyAisTransform;
		[SerializeField] private Transform _patrolPointsParent;
		
		private void Awake()
		{
			var points = _patrolPointsParent.Cast<Transform>().ToList();

			foreach (Transform enemy in _enemyAisTransform)
			{
				var ai = enemy.GetComponent<EnemyAI>();
				ai.Init(_playerTransform, points);
			}
		}
	}
}
