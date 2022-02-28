using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	// Player Acceleration, or Force applied per tick
	[SerializeField] float playerAcceleration = 5f;
	// Players Max Speed in Velocity Magnitude
	[SerializeField] float playerMaxSpeed = 5f;
	Rigidbody rb;
	// Movement delta, or change in movement from Input
	Vector3 movementDelta;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		Vector2 input = context.action.ReadValue<Vector2>();
		movementDelta = new Vector3(input.x, 0, input.y);
	}

	private void FixedUpdate()
	{
		ApplyMoveForce();

		// Clamp velocity
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, playerMaxSpeed);
	}

	private void ApplyMoveForce()
	{
		Vector3 moveVector;
		if (movementDelta.magnitude > 1)
			moveVector = movementDelta.normalized * Time.deltaTime * playerAcceleration;
		else
			moveVector = movementDelta * Time.deltaTime * playerAcceleration;
		rb.AddRelativeForce(moveVector);
	}
}
