using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public float AttackRate = 2f;
    public LayerMask EnemyLayers;

    private float nextAttackTime = 0f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / AttackRate;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Defend();
        }
    }

    private void Attack()
    {

        animator.SetTrigger($"Attack{UnityEngine.Random.Range(1, 3)}");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(25);
        }
    }
    
    private void Defend()
    {

        animator.SetTrigger("Defend");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().DefendBlock();
        }

    }

    private void OnDrawGizmosSelected()
    {
        if(AttackPoint is null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
