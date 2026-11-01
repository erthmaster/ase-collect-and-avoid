using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class StateMachine : MonoBehaviour
	{
		private State _currentState;

		public void ChangeState(State state)
		{
			_currentState?.OnExit();
			_currentState = state;
			_currentState.OnEnter();
		}
		
		public void Update()
		{
			_currentState.OnUpdate();
		}
	}
}
