using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class StateMachine : MonoBehaviour
	{
		private struct StateTransition
		{
			public State From;
			public State To;
			public Func<bool> Condition;

			public StateTransition(State from, State to, Func<bool> condition)
			{
				From = from;
				To = to;
				Condition = condition;
			}
		}
		
		public State CurrentState { get; private set; }
		private List<StateTransition> _stateTransitions = new();

		public void AddTransition(State from, State to, Func<bool> condition)
		{
			_stateTransitions.Add(new StateTransition(from, to, condition));
		}

		public void ChangeState(State state)
		{
			CurrentState?.OnExit();
			CurrentState = state;
			CurrentState.OnEnter();
		}
		
		public void Update()
		{
			if(CurrentState != null)
				CurrentState.OnUpdate();

			foreach (var transition in _stateTransitions)
			{
				if ((transition.From == null || CurrentState == transition.From) && transition.Condition())
				{
					ChangeState(transition.To);
				}
			}
		}

		private void FixedUpdate()
		{
			if(CurrentState != null)
				CurrentState.OnFixedUpdate();
		}

		private void OnDrawGizmos()
		{
			if(CurrentState != null)
				CurrentState.OnGizmosDraw();
		}
	}
}
