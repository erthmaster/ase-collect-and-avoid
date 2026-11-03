using System;
using PrimeTween;
using UnityEngine;

namespace Game
{
	public class Collectible : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private Collider _collider;

		private void Update()
		{
			transform.eulerAngles = new(0, transform.eulerAngles.y + _rotationSpeed * Time.deltaTime, 0);
		}

		public void OnPickup()
		{
			_collider.enabled = false;
			Sequence.Create()
				.Chain(Tween.Scale(transform, 2, 0.2f))
				.Chain(Tween.Scale(transform, 0, 0.2f))
				.OnComplete(() => Destroy(gameObject));
		}
	}
}
