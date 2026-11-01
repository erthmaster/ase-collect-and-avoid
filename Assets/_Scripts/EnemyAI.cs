using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
	public class EnemyAI : MonoBehaviour
	{
		[SerializeField] private Transform PatrolPointsParent;
		
		[SerializeField] protected NavMeshAgent NavMeshAgent;
		
		private List<Transform> PatrolPoints = new();
		
		private StateMachine _stateMachine;
		private PatrolState _patrolState;
		private ChasingState _chasingState;

		private void Awake()
		{
			foreach (Transform point in PatrolPointsParent)
			{
				PatrolPoints.Add(point);
			}
			
			_stateMachine = GetComponent<StateMachine>();

			_patrolState = new(_stateMachine, this);
			_chasingState = new(_stateMachine, this);
			
			print(_patrolState);
			_stateMachine.ChangeState(_patrolState);
		}


		private class EnemyState : State
		{
			protected EnemyAI EnemyAI;
			public EnemyState(StateMachine machine, EnemyAI enemyAI) : base(machine) => EnemyAI = enemyAI;

			public EnemyState Init(EnemyAI enemyAI)
			{
				EnemyAI = enemyAI;
				return this;
			}
		}


		private class PatrolState : EnemyState
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
				print($"Moving to: {_currentTarget.name}");
				print($"Last Patrol Points: {_lastPatrolPoints.Count}");
				if (Vector3.Distance(_currentTarget.position, EnemyAI.transform.position) < 1)
				{
					print("Retarget");
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


		private class ChasingState : EnemyState
		{
			public ChasingState(StateMachine machine, EnemyAI enemyAI) : base(machine, enemyAI) { }
			
			public override void OnEnter()
			{
				
			}
		}
	}
}
