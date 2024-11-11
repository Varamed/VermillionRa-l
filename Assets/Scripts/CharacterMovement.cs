using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintMultiplier = 2f;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;

    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField] Transform cameraTransform;

    private CharacterController characterController;
    
   
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        float currentSpeed = speed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveX, moveZ) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (direction.magnitude < 0.1f)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
