using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CharacterController))]
public class playermovement : MonoBehaviour
{
	public PlayerController player { get; private set; }
	public CharacterController controller { get; private set; }

	public float moveSpeed = 8.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

	private void Awake()
	{
		player = GetComponent<PlayerController>();
		controller = GetComponent<CharacterController>();
	}

#if WORLDS
	void MoveBetweenWorlds(Transform from, Transform to)
	{
        controller.transform.position = to.TransformPoint(
            from.InverseTransformPoint(
                controller.transform.position));
    }
#endif // WORLDS

	// Update is called once per frame
	void Update()
	{
#if WORLDS
        if (Input.GetButtonDown("Fire2") && player.isGameControlEnabled)
		{
            controller.enabled = false;
			if (isInWorldB)
            {
                MoveBetweenWorlds(worldB, worldA);
                isInWorldB = false;
            }
			else
            {
                MoveBetweenWorlds(worldA, worldB);
                isInWorldB = true;
            }
            controller.enabled = true;
        }
#endif // WORLDS

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

		if (player.isGameControlEnabled)
		{
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			Vector3 move = transform.right * x + transform.forward * z;

			controller.Move(move * moveSpeed * Time.deltaTime);

			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
			}
		}

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
