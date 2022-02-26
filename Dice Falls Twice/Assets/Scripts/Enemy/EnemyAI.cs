using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
	private enum State
	{
		Patrolling,
		ChasingTarget,
		Attacking
	}
	
	[SerializeField] private Transform _target;
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private Wand _wand;
	[SerializeField] private int _maxHP;

	public UnityEvent _death;
	public HealthSystem HealthSystem { get { return _healthSystem; } }

	private HealthSystem _healthSystem;

	[SerializeField]private State _state, _previousState;
	private float _distance;

	private FieldOfView _fieldOfView;
	public bool isDead;

	private AudioManager _audioManagerHit, _audioManagerDeath;
	private GameObject _managerHit, _managerDeath;
	private int _indexOfSound;

	void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		_state = State.Patrolling;

		_healthSystem = new HealthSystem(_maxHP, gameObject);
		_fieldOfView = GetComponent<FieldOfView>();

		_managerHit = GameObject.FindGameObjectWithTag("AudioManagerHit");
		_audioManagerHit = _managerHit.GetComponent<AudioManager>();
		_managerDeath = GameObject.FindGameObjectWithTag("AudioManagerDeath");
		_audioManagerDeath = _managerDeath.GetComponent<AudioManager>();
	}

	void Update()
	{
		if (_target == null)
			_target = FindObjectOfType<PlayerWeaponManager>().transform;

		_distance = Vector3.Distance(_target.position, transform.position);
		switch (_state)
		{
			case State.Patrolling:
				FindTarget();
				break;

			case State.ChasingTarget:
				if ((_distance <= _fieldOfView.radius && (_previousState == State.Attacking || _previousState == State.ChasingTarget)) || _fieldOfView.canSeePlayer)
				{
					_agent.SetDestination(_target.position);
					_previousState = State.ChasingTarget;
					if (_distance <= _agent.stoppingDistance)
					{
						_state = State.Attacking;
						FaceTarget();
					}
						
				}
				break;

			case State.Attacking:
				FaceTarget();
				if (_distance > _agent.stoppingDistance)
				{
					_state = State.ChasingTarget;
					_previousState = State.Attacking;
				}
				
				if (_fieldOfView.canSeePlayer)
					_wand.Shoot();
				else
				{
					_state = State.ChasingTarget;
					_previousState = State.Attacking;
				}
				break;
		}
	}

	private void FaceTarget()
	{
		Vector3 direction = (_target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void FindTarget()
	{
		if (Vector3.Distance(transform.position, _target.position) < _fieldOfView.radius)
			_state = State.ChasingTarget;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _agent.stoppingDistance);
	}

	public void CheckHealth()
	{
		if (_healthSystem.GetHealth() > 0)
		{
			_indexOfSound = Random.Range(0, _audioManagerHit.GetSoundsCount());
			_audioManagerHit.Play("EnemyHit" + (_indexOfSound + 1));
		}

		if (_healthSystem.GetHealth() <= 0)
		{
			_indexOfSound = Random.Range(0, _audioManagerDeath.GetSoundsCount());
			_death.Invoke();
			_audioManagerDeath.Play("EnemyDeath" + (_indexOfSound + 1));
			gameObject.SetActive(false);
		}
	}
}
