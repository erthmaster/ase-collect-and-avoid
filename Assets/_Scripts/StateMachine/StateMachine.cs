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
			if(_currentState != null)
				_currentState.OnUpdate();
		}

		private void FixedUpdate()
		{
			if(_currentState != null)
				_currentState.OnFixedUpdate();
		}

		private void OnDrawGizmos()
		{
			if(_currentState != null)
				_currentState.OnGizmosDraw();
		}
	}
}
