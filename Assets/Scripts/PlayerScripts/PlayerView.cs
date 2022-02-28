using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
	// X Axis (Input) Sensitivity
	[SerializeField] float xSensitivity = 8f;
	// Y Axis (Input) Sensitivity
	[SerializeField] float ySensitivity = 0.5f;
	[SerializeField] Transform playerCamera;
	// Up/Down view clamping along X Axis (Local Rotation)
	[SerializeField] float xClamp = 85f;

	// Left/Right rotation along the X Axis (Local Rotation)
	float xRotation = 0f;
	// Left/Right View Axis (Input)
	float xViewAxis;
	// Up/Down View Axis (Input)
	float yViewAxis;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void OnLook(InputAction.CallbackContext context)
	{
		Vector2 input = context.ReadValue<Vector2>();
		xViewAxis = input.x;
		yViewAxis = input.y;
	}

	// Update is called once per frame
	void Update()
	{
		UpdateLeftRightViewAxis();
		UpdateUpDownViewAxis();
	}

	private void UpdateLeftRightViewAxis()
	{
		// Left/Right View Axis
		transform.Rotate(Vector3.up, xViewAxis * xSensitivity * Time.deltaTime);
	}

	private void UpdateUpDownViewAxis()
	{
		// Sets Rotation value for up/down, frame rate independent
		xRotation -= yViewAxis * ySensitivity * Time.deltaTime;
		// Clamp value for up/down view
		xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
		// Get Euler Angles from current local rotation
		Vector3 targetRotation = transform.eulerAngles;
		// Apply new X axis rotation value to local rotation
		targetRotation.x = xRotation;
		// Apply adjusted rotation to camera
		playerCamera.eulerAngles = targetRotation;
	}
}
