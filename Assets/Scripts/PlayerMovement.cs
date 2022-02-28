using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float playerAcceleration = 5f;
	[SerializeField] float playerMaxSpeed = 5f;
	Rigidbody rb;
	Vector3 movementDelta;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Move(InputAction.CallbackContext context)
	{
		Vector2 input = context.action.ReadValue<Vector2>();
		movementDelta = new Vector3(input.x, 0, input.y);
	}

	private void Update()
	{
		Vector3 moveVector;
		if (movementDelta.magnitude > 1)
			moveVector = movementDelta.normalized * Time.deltaTime * playerAcceleration;
		else
			moveVector = movementDelta * Time.deltaTime * playerAcceleration;
		rb.AddRelativeForce(moveVector);

		// Clamp velocity
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, playerMaxSpeed);
	}
}
