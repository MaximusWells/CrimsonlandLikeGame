using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Animator animator;
    private NavMeshAgent navigator;
    private GameObject playerToFollow;
    PlayerController playerScript;
    public float nextAttack = 0.5f;
    public int damage;
    private float attackTimer;

    void Awake () {
        animator = GetComponent<Animator>();
        navigator = GetComponent<NavMeshAgent>();
        playerToFollow = GameObject.FindWithTag("Player");
        playerScript = playerToFollow.GetComponent<PlayerController>();
    }
	
	void Update ()
    {
        Follow();
    }

    public void Follow()
    {
        navigator.SetDestination(playerToFollow.transform.position);
    }

    public void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= nextAttack)
        {
            animator.SetBool("isAttacking", true);
            attackTimer = 0f;
            playerScript.GetDamaged(damage);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
