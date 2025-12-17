using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;
    public ZombieHealth zombieHealth;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    public PlayerController playerController;

    public GameObject lastGOHit;

    public GameObject crossair1;
    public GameObject crossair2;

    void Start()
    {
        crossair1 = GameObject.Find("Crossair");
        crossair2 = GameObject.Find("Crossair (1)");
        if(cam == null)
        {
            Debug.Log("Pas de caméra renseignée sur le système de tir");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetAxis("RT") > 0.5f)
        {
            Shoot();
        }

        if(Input.GetKey("mouse 1"))
        {
            playerController.isShooting = true;
            crossair1.SetActive(false);
            crossair2.SetActive(true);
        }
        else
        {
            playerController.isShooting = false;
            crossair1.SetActive(true);
            crossair2.SetActive(false);
        }
    }
    
    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask, QueryTriggerInteraction.Ignore))
        {
            if(hit.collider.tag == "Ennemy")
            {
                lastGOHit = hit.collider.gameObject;
                CmdplayerShot(hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    private void CmdplayerShot(string playerId, float damage)
    {
        Debug.Log(playerId + " à été touché.");

        ZombieHealth zombieHealth = lastGOHit.GetComponent<ZombieHealth>();
        zombieHealth.ZombieTakeDamage(damage);
    }
}
