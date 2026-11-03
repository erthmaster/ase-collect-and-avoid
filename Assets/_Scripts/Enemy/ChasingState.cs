using UnityEngine;

namespace Game
{
	public class ChasingState : EnemyState
	{
		public ChasingState(StateMachine machine, EnemyAI enemyAI) : base(machine, enemyAI) { }
		private Vector3 _lastPlayerPos;

		public override void OnEnter()
		{
			EnemyAI.NavMeshAgent.speed = EnemyAI.ChasingSpeed;
		}

		public override void OnUpdate()
		{
			if (EnemyAI.IsPlayerDetected)
				_lastPlayerPos = EnemyAI.PlayerTransform.position;
			
			EnemyAI.NavMeshAgent.SetDestination(_lastPlayerPos);
		}
		
		public bool IsOnTrack() => 
			EnemyAI.IsPlayerDetected || 
			(!EnemyAI.IsPlayerDetected && Vector3.Distance(_lastPlayerPos, EnemyAI.transform.position) > 1f);
	}
}
