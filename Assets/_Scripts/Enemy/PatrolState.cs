using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class PatrolState : EnemyState
	{
		public PatrolState(StateMachine machine, EnemyAI enemyAI) : base(machine, enemyAI) { }

		private Transform _currentTarget;
		private readonly List<Transform> _lastPatrolPoints = new();

		
		public override void OnEnter()
		{
			FindTargetAndMove();
		}

		public override void OnUpdate()
		{
			// Debug.Log($"Moving to: {_currentTarget.name}");
			// Debug.Log($"Last Patrol Points: {_lastPatrolPoints.Count}");
			if (Vector3.Distance(_currentTarget.position, EnemyAI.transform.position) < 1)
			{
				// Debug.Log("Retarget");
				FindTargetAndMove();
			}
		}

		private void FindTargetAndMove()
		{
			StripList(_lastPatrolPoints, 10);
				
			_currentTarget = GetNearestPatrolPoint();
			EnemyAI.NavMeshAgent.SetDestination(_currentTarget.position);
				
			_lastPatrolPoints.Add(_currentTarget);
		}

		private void StripList(List<Transform> list, int leftCount)
		{
			if(list.Count > leftCount - 1)
				list.RemoveRange(0, leftCount);
		}

		private Transform GetNearestPatrolPoint()
		{
			Transform closest = null;
			var currentPos = EnemyAI.transform.position;

			foreach (var point in EnemyAI.PatrolPoints)
			{
				if (closest == null || 
				    Vector3.Distance(point.position, currentPos) <
				    Vector3.Distance(closest.position, currentPos))
				{
					if(_lastPatrolPoints.Contains(point))
						continue;
						
					closest = point;
				}
			}

			return closest;
		}
	}
}
