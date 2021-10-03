using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private bool _playerInSightRange, _playerInAttackRange;
    private PlayerMortal _playerMortal;
    private GameObject _playerGameObject;
    private Transform _playerTransform;
    private float _nextAttackTime = 0;
    [SerializeField]
    private EnemyMortal _enemyMortal;
    [SerializeField]
    private float _attackDelayInSeconds = 2f;


    public NavMeshAgent Agent;
    public LayerMask WhatIsPlayer;
    public float SightRange, AttackRange;

    public void Update()
    {
        if (!_enemyMortal.IsDead)
        {
            _playerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
            _playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

            if (_playerInSightRange)
            {
                if (_playerInAttackRange && Time.time > _nextAttackTime)
                {
                    AttackPlayer();
                }
                else
                {
                    ChasePlayer();
                }
            } 
        }
    }

    public void Stop()
    {
        Agent.SetDestination(transform.position);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, AttackRange);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, SightRange);
    }

    private void Awake()
    {
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = GameObject.Find("HorsePlayer").transform;
        _playerMortal = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMortal>();
    }
    private void ChasePlayer()
    {
        Agent.SetDestination(_playerTransform.position);
    }

    private void AttackPlayer()
    {
        transform.LookAt(_playerTransform);
        _playerMortal.TakeDamage(_enemyMortal.Weapon.GetWeaponDamage());
        _nextAttackTime = Time.time + _attackDelayInSeconds;
    }
}

