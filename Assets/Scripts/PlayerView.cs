using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
	[SerializeField] float xSensitivity = 8f;
	[SerializeField] float ySensitivity = 0.5f;

	[SerializeField] Transform playerCamera;
	[SerializeField] float xClamp = 85f;
	float xRotation = 0f;

	float xViewAxis, yViewAxis;

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
		Debug.Log(xViewAxis);
		transform.Rotate(Vector3.up, xViewAxis * xSensitivity * Time.deltaTime);

		xRotation -= yViewAxis * ySensitivity * Time.deltaTime;
		xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
		Vector3 targetRotation = transform.eulerAngles;
		targetRotation.x = xRotation;
		playerCamera.eulerAngles = targetRotation;
	}
}
