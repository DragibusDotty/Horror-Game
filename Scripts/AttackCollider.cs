using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Zombie zombie;

    private void OnTriggerEnter(Collider col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            zombie.playerToAttack = player;
            zombie.SetState(Zombie.ZombieState.Attacking);

            if (zombie.attackCoroutine == null)
            {
                zombie.attackCoroutine = zombie.StartCoroutine(zombie.AttackPlayer());
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && player == zombie.playerToAttack)
        {
            if (zombie.attackCoroutine != null)
            {
                zombie.StopCoroutine(zombie.attackCoroutine);
                zombie.attackCoroutine = null;
            }

            zombie.SetState(Zombie.ZombieState.Running); // Retour au suivi si le joueur est toujours détecté
        }
    }
}
