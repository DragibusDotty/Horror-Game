using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth;

    [Header("Components")]
    public Animator animator;
    public Zombie zombie; // référence au script Zombie
    public DetectionCollider detectionCollider;
    public AttackCollider attackCollider;

    [Header("Respawn")]
    [HideInInspector] public GameObject spawnPoint;
    public float respawnTime = 3f;

    private bool isDead = false;

    private void Start()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        if (zombie != null)
        {
            zombie.currentState = Zombie.ZombieState.Idle;
            zombie.playerToAttack = null;
            zombie.attackCoroutine = null;
            zombie.enabled = true;
        }

        if (detectionCollider != null)
            detectionCollider.enabled = true;

        if (attackCollider != null)
            attackCollider.enabled = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = true;

        if (animator != null)
            animator.SetBool("isDead", false);
    }

    public void ZombieTakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log(transform.name + " a maintenant " + currentHealth + " points de vie.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Désactiver les composants pour que le zombie ne bouge ni n'attaque
        if (zombie != null)
        {
            zombie.SetState(Zombie.ZombieState.Dead);
            zombie.playerToAttack = null;
            if (zombie.attackCoroutine != null)
            {
                zombie.StopCoroutine(zombie.attackCoroutine);
                zombie.attackCoroutine = null;
            }
            zombie.enabled = false;
        }

        if (detectionCollider != null) detectionCollider.enabled = false;
        if (attackCollider != null) attackCollider.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        if (animator != null) animator.SetBool("isDead", true);

        // Respawn après un délai
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = spawnPoint.transform.position;
        SetDefaults();
        Debug.Log(transform.name + " a respawné !");
    }

    // Debug : touche H pour tester les dégâts
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ZombieTakeDamage(100f);
        }
    }
}
