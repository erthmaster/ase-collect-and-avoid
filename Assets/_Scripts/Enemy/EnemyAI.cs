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
		
		[HideInInspector] public NavMeshAgent NavMeshAgent;
		[HideInInspector] public List<Transform> PatrolPoints = new();
		
		private StateMachine _stateMachine;
		private PatrolState _patrolState;
		private ChasingState _chasingState;

		private void Awake()
		{
			_stateMachine = GetComponent<StateMachine>();
			NavMeshAgent = GetComponent<NavMeshAgent>();
			
			foreach (Transform point in PatrolPointsParent)
			{
				PatrolPoints.Add(point);
			}
			
			_patrolState = new(_stateMachine, this);
			_chasingState = new(_stateMachine, this);
			
			print(_patrolState);
			_stateMachine.ChangeState(_patrolState);
		}




	}
}
