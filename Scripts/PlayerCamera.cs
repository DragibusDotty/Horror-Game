using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public PlayerController playerController;

    public Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        playerController.camera = camera;
    }

    void Update()
    {
        if(playerController.isShooting)
        {
            gameObject.GetComponent<Camera>().fieldOfView = 50;
        }
        else
        {
            gameObject.GetComponent<Camera>().fieldOfView = 70;
        }
    }
}
