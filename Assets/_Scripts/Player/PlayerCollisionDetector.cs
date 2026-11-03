using System;
using UnityEngine;

namespace Game
{
	public class PlayerCollisionDetector : MonoBehaviour
	{
		public event Action<Collectible> CollectibleCollided;
		public event Action<EnemyAI> EnemyCollided;
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Collectible>(out var collectible))
			{
				CollectibleCollided?.Invoke(collectible);
			}
			
			if (other.TryGetComponent<EnemyAI>(out var enemyAI))
			{
				EnemyCollided?.Invoke(enemyAI);
			}
		}
	}
}
