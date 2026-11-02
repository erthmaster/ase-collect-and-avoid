namespace Game
{
	public class State
	{
		private StateMachine _stateMachine;

		public State(StateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		protected void TransitionTo(State state)
		{
			_stateMachine.ChangeState(state);
		}

		public virtual void OnEnter() {}
		public virtual void OnExit() {}
		public virtual void OnUpdate() {}
		public virtual void OnFixedUpdate() {}
		public virtual void OnGizmosDraw() {}
	}
}
