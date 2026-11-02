using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
	public class EnemyAI : MonoBehaviour
	{
		[HideInInspector] public NavMeshAgent NavMeshAgent;
		[field: SerializeField] public float PlayerDetectionRadius { get; private set; } = 10;
		
		public IReadOnlyList<Transform> PatrolPoints => _patrolPoints;
		private Transform PlayerTransform { get; set; }

		private PatrolState PatrolState { get; set; }
		private ChasingState ChasingState { get; set; }
		
		private readonly RaycastHit[] _raycastHits = new RaycastHit[10];
		private StateMachine _stateMachine;
		private List<Transform> _patrolPoints = new();
		
		private bool _isPlayerDetected;
		

		public void Init(Transform playerTransform, List<Transform> patrolPoints)
		{
			PlayerTransform = playerTransform;
			_patrolPoints = patrolPoints;
		}
		
		private void Awake()
		{
			_stateMachine = GetComponent<StateMachine>();
			NavMeshAgent = GetComponent<NavMeshAgent>();
			
			PatrolState = new(_stateMachine, this);
			ChasingState = new(_stateMachine, this);
			
			print(PatrolState);
			// _stateMachine.ChangeState(PatrolState);
		}
		
		public void FixedUpdate()
		{
			_isPlayerDetected = false;
			var ray = new Ray(transform.position, PlayerTransform.position - transform.position);
			var size = Physics.RaycastNonAlloc(ray, _raycastHits, PlayerDetectionRadius, LayerMask.GetMask("Objects"));

			Debug.DrawLine(transform.position, PlayerTransform.position - transform.position);
			if(size > 0)
			{
				for (var i = 0; i < size; i++)
				{
					var hit = _raycastHits[i];

					if (!hit.collider)
						continue;
					
					Debug.Log("");
					
					if (hit.collider.gameObject.CompareTag("Player"))
					{
						_isPlayerDetected = true;
					}
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = _isPlayerDetected ? Color.green : Color.darkGreen;
			Gizmos.DrawWireSphere(transform.position, PlayerDetectionRadius);
		}
	}
}
