using System;
using UnityEngine;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private int _currentScore;
		[SerializeField] private int _targetScore;
		[SerializeField] private PlayerCollectibleDetector _collectibleDetector;

		private void OnEnable()
		{
			_collectibleDetector.CollectibleCollided += OnCollectibleCollision;
		}

		private void OnDisable()
		{
			_collectibleDetector.CollectibleCollided -= OnCollectibleCollision;
		}

		private void OnCollectibleCollision(Collectible collectible)
		{
			_currentScore++;
			Destroy(collectible.gameObject);
			UpdateScore();
		}

		private void UpdateScore()
		{
			if (_currentScore >= _targetScore)
			{
				print("Перемога!");
			}
		}
	}
}
