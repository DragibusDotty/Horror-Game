using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float mouseSensitivityX = 3;

    [SerializeField]
    private float mouseSensitivityY = 3;

    private PlayerMotor motor;
    private PlayerCamera playerCamera;

    public Rigidbody rb;
    public Animator animator;
    public Camera camera;

    public int jumpForce;
    public bool isGrounded = true;
    public bool isMoving;
    public bool isShooting;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Calculer la vélocité (vitesse) du mouvement de notre joueur
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        motor.Move(velocity);
        
        animator.SetBool("isShooting", isShooting);

        //On utilise les variables xMov et zMov pour les transformer en un seul nombre
        float animationFastRun = (xMov + zMov);

        if(animationFastRun != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);

        //on calcule la rotation du joueur en un Vector3
        float yRot = Input.GetAxisRaw("Mouse X");
        
        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

        motor.Rotate(rotation);

        //on calcule la rotation de la caméra en un Vector3
        float xRot = Input.GetAxisRaw("Mouse Y");
        
        float cameraRotationX = xRot * mouseSensitivityY;

        motor.RotateCamera(cameraRotationX);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Floor")
        {
            isGrounded = true;
        }
    }
}