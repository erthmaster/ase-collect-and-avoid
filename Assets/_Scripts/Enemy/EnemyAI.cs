using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
	public class EnemyAI : MonoBehaviour
	{
		[field: SerializeField] public float PlayerDetectionRadius { get; private set; } = 10;
		[field:SerializeField] public float PatrollingSpeed { get; private set; }
		[field:SerializeField] public float ChasingSpeed { get; private set; }
		[SerializeField] private MeshRenderer _redEyesRenderer;
		
		private PatrolState _patrolState;
		private ChasingState _chasingState;
		
		private readonly RaycastHit[] _raycastHits = new RaycastHit[10];
		private StateMachine _stateMachine;
		private List<Transform> _patrolPoints = new();
		
		public NavMeshAgent NavMeshAgent { get; private set; }
		public Transform PlayerTransform { get; private set; }
		public bool IsPlayerDetected { get; private set; }
		public IReadOnlyList<Transform> PatrolPoints => _patrolPoints;

		public void Init(Transform playerTransform, List<Transform> patrolPoints)
		{
			PlayerTransform = playerTransform;
			_patrolPoints = patrolPoints;
		}
		
		private void Awake()
		{
			_stateMachine = GetComponent<StateMachine>();
			NavMeshAgent = GetComponent<NavMeshAgent>();
			
			_patrolState = new(_stateMachine, this);
			_chasingState = new(_stateMachine, this);
			
			_stateMachine.AddTransition(_patrolState, _chasingState, () => IsPlayerDetected);
			_stateMachine.AddTransition(_chasingState, _patrolState, () => !_chasingState.IsOnTrack());
			
			_stateMachine.ChangeState(_patrolState);
		}

		private void Update()
		{
			_redEyesRenderer.enabled = _stateMachine.CurrentState == _chasingState;
		}

		public void FixedUpdate()
		{
			IsPlayerDetected = false;
			var ray = new Ray(transform.position, PlayerTransform.position - transform.position);
			var size = Physics.RaycastNonAlloc(ray, _raycastHits, PlayerDetectionRadius, LayerMask.GetMask("Objects"));

			if(size > 0)
			{
				for (var i = 0; i < size; i++)
				{
					var hit = _raycastHits[i];

					if (!hit.collider)
						continue;
					
					if (hit.collider.gameObject.CompareTag("Player") && i <= 0)
					{
						IsPlayerDetected = true;
						_stateMachine.ChangeState(_chasingState);
					}
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = IsPlayerDetected ? Color.green : Color.red;
			Gizmos.DrawWireSphere(transform.position, PlayerDetectionRadius);
		}
	}
}
