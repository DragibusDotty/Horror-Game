using UnityEngine;

public class DetectionCollider : MonoBehaviour
{
    public Zombie zombie;

    private void OnTriggerEnter(Collider col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            zombie.playerToAttack = player;
            zombie.SetState(Zombie.ZombieState.Screaming);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Player player = col.GetComponent<Player>();
        if (player != null && player == zombie.playerToAttack)
        {
            zombie.playerToAttack = null;
            zombie.SetState(Zombie.ZombieState.Idle);
        }
    }
}
