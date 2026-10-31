using System;
using UnityEngine;

namespace Game
{
	public class PlayerCollectibleDetector : MonoBehaviour
	{
		public event Action<Collectible> CollectibleCollided;
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Collectible>(out var collectible))
			{
				CollectibleCollided?.Invoke(collectible);
			}
		}
	}
}
