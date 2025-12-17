using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public GameObject playerGraphics;
    public GameObject playerCharacter;

    public PlayerController playerController;
    public Player player;

    public void Start()
    {
        playerController.animator = GetComponent<Animator>();
        player.animator = GetComponent<Animator>();
    }

    void Update()
    {
        float playerDifferenceY = 1.225f;
        Vector3 playerPosition = new Vector3(0, -playerDifferenceY, 0);
        playerCharacter.transform.position = playerGraphics.transform.position;
        playerCharacter.transform.rotation = playerGraphics.transform.rotation;
    }
}
