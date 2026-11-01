namespace Game
{
	public class EnemyState : State
	{
		protected EnemyAI EnemyAI;
		public EnemyState(StateMachine machine, EnemyAI enemyAI) : base(machine) => EnemyAI = enemyAI;

		public EnemyState Init(EnemyAI enemyAI)
		{
			EnemyAI = enemyAI;
			return this;
		}
	}
}
