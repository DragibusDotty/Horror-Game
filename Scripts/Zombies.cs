using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Zombie : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Stats")]
    public float screamDuration = 2.7f;
    public float attackDamage = 5f;
    public float attackInterval = 1.25f;
    public float attackCooldown = 0.5f;

    [HideInInspector] public Player playerToAttack;
    [HideInInspector] public Coroutine attackCoroutine;

    private Coroutine screamCoroutine;

    public enum ZombieState { Idle, Screaming, Running, Attacking, Dead }
    public ZombieState currentState = ZombieState.Idle;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Si le joueur est mort, revenir à idle
        if (playerToAttack != null && playerToAttack.isDead)
        {
            playerToAttack = null;
            SetState(ZombieState.Idle);
        }

        // Suivi du joueur si en running
        if (currentState == ZombieState.Running && playerToAttack != null)
        {
            agent.SetDestination(playerToAttack.transform.position);
        }

        UpdateAnimations();
    }

    public void SetState(ZombieState newState)
    {
        if (currentState == ZombieState.Dead) return;

        currentState = newState;

        switch (currentState)
        {
            case ZombieState.Idle:
                agent.isStopped = true;
                agent.ResetPath();
                break;

            case ZombieState.Screaming:
                agent.isStopped = true;
                if (screamCoroutine == null && playerToAttack != null)
                {
                    screamCoroutine = StartCoroutine(ScreamThenRun());
                }
                break;

            case ZombieState.Running:
                agent.isStopped = false;
                break;

            case ZombieState.Attacking:
                agent.isStopped = true;
                break;

            case ZombieState.Dead:
                agent.isStopped = true;
                break;
        }
    }

    private IEnumerator ScreamThenRun()
    {
        yield return new WaitForSeconds(screamDuration);

        if (playerToAttack != null)
        {
            SetState(ZombieState.Running);
        }

        screamCoroutine = null;
    }

    public IEnumerator AttackPlayer()
    {
        while (playerToAttack != null)
        {
            yield return new WaitForSeconds(attackCooldown);
            playerToAttack.RpcTakeDamage(attackDamage);
            yield return new WaitForSeconds(attackInterval);
        }
        attackCoroutine = null;
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isIdling", currentState == ZombieState.Idle);
        animator.SetBool("isScreaming", currentState == ZombieState.Screaming);
        animator.SetBool("isRunning", currentState == ZombieState.Running);
        animator.SetBool("isAttacking", currentState == ZombieState.Attacking);
    }
}
