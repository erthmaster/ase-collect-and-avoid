using System;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private int _currentScore;
		[SerializeField] private int _targetScore;
		[SerializeField] private PlayerCollisionDetector _collisionDetector;
		
		[SerializeField] private GameObject _gameMenuPanel;
		[SerializeField] private GameObject _pausedText;
		[SerializeField] private GameObject _victoryText;
		[SerializeField] private GameObject _loseText;
		[SerializeField] private TMP_Text _scoreText;
		
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private Camera _camera;
		[SerializeField] private ShakeSettings _loseCameraShake;

		private PlayerInput.UIActions _uiInput;
		private bool _isGamePaused;

		public void Init(PlayerInput.UIActions input)
		{
			_uiInput = input;
		}

		private void Awake()
		{
			_gameMenuPanel.SetActive(false);
			UpdateScore();
			_uiInput.Pause.performed += _ => SetGamePaused(!_isGamePaused);
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
			collectible.OnPickup();
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
			
			_gameMenuPanel.SetActive(true);
			_victoryText.SetActive(isVictory);
			_loseText.SetActive(!isVictory);

			if (!isVictory)
			{
				Tween.ShakeLocalPosition(_camera.transform, _loseCameraShake);
			}
		}

		public void SetGamePaused(bool isPause)
		{
			_isGamePaused = isPause;
			
			Time.timeScale = isPause ? 0 : 1;
			
			_gameMenuPanel.SetActive(isPause);
			_pausedText.SetActive(isPause);
		}

		public void RestartGame()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene(0);
		}
	}
}
