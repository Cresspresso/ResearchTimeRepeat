using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
	public bool isHumanControlEnabled = true;

    public float moveSpeed = 8.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    public Transform worldA;
    public Transform worldB;
    public bool isInWorldB = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void MoveBetweenWorlds(Transform from, Transform to)
	{
        controller.transform.position = to.TransformPoint(
            from.InverseTransformPoint(
                controller.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && isHumanControlEnabled)
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

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

		if (isHumanControlEnabled)
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
