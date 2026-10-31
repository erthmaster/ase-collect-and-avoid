using System;
using UnityEngine;

namespace Game
{
	public class Collectible : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed;

		private void Update()
		{
			transform.eulerAngles = new(0, transform.eulerAngles.y + _rotationSpeed * Time.deltaTime, 0);
		}
	}
}
