using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private int _currentScore;
		[SerializeField] private int _targetScore;
		[SerializeField] private PlayerCollisionDetector _collisionDetector;
		
		[SerializeField] private GameObject _gameOverPanel;
		[SerializeField] private GameObject _victoryText;
		[SerializeField] private GameObject _loseText;
		[SerializeField] private TMP_Text _scoreText;
		
		[SerializeField] private AudioSource _audioSource;

		private void Awake()
		{
			_gameOverPanel.SetActive(false);
			UpdateScore();
		}

		private void OnEnable()
		{
			_collisionDetector.CollectibleCollided += OnCollisionCollision;
			_collisionDetector.EnemyCollided += OnEnemyCollided;
		}

		private void OnDisable()
		{
			_collisionDetector.CollectibleCollided -= OnCollisionCollision;
			_collisionDetector.EnemyCollided -= OnEnemyCollided;
		}

		private void OnCollisionCollision(Collectible collectible)
		{
			_currentScore++;
			Destroy(collectible.gameObject);
			UpdateScore();
			
			_audioSource.Play();
		}
		
		private void OnEnemyCollided(EnemyAI enemyAI)
		{
			FinishGame(false);
		}

		private void UpdateScore()
		{
			_scoreText.text = $"Score: {_currentScore}/{_targetScore}";
			
			if (_currentScore >= _targetScore)
			{
				FinishGame(true);
			}
		}

		private void FinishGame(bool isVictory)
		{
			Time.timeScale = 0;
			
			_gameOverPanel.SetActive(true);
			_victoryText.SetActive(isVictory);
			_loseText.SetActive(!isVictory);
		}

		public void RestartGame()
		{
			SceneManager.LoadScene(0);
		}
	}
}
