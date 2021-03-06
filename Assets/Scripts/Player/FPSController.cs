using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	public SharedVector3 playerPosition;
	
	public float mouseSensitivityX = 1.0f;
	public float mouseSensitivityY = 1.0f;

	public float walkSpeed = 10.0f;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;

	Transform cameraT;
	float verticalLookRotation;

	Rigidbody rigidbodyR;

	float jumpForce = 250.0f;
	bool grounded;
	public LayerMask groundedMask;

	bool cursorVisible;
	bool cameraMovementActive = true;

	public float interactionDistance;

	private Ray interactionRay;

	private RaycastHit interactionRayHit;
	// Use this for initialization
	void Start () {
		cameraT = Camera.main.transform;
		rigidbodyR = GetComponent<Rigidbody> ();
		LockMouse ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(cameraMovementActive)
		{
			// rotation
			transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * mouseSensitivityX);
			verticalLookRotation += Input.GetAxis ("Mouse Y") * mouseSensitivityY;
			verticalLookRotation = Mathf.Clamp (verticalLookRotation, -60, 60);
			cameraT.localEulerAngles = Vector3.left * verticalLookRotation;
		}

		// movement
		Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
			grounded = true;
		}
		else {
			grounded = false;
		}
		
		playerPosition.Value = transform.position;
		/* Lock/unlock mouse on click */
		/*if (Input.GetMouseButtonUp (0)) {
			if (!cursorVisible) {
				UnlockMouse ();
			} else {
				LockMouse ();
			}
		}*/
	}

	private void LateUpdate()
	{
		interactionRay = new Ray (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), Camera.main.transform.forward);

		if(Physics.Raycast(interactionRay, out interactionRayHit, interactionDistance ))
		{
			if (interactionRayHit.collider.gameObject.GetComponent<IInteractable>() != null)
			{
				var interactable = interactionRayHit.collider.gameObject.GetComponent<IInteractable>();
				interactable.Hover();
			}
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			if(Physics.Raycast(interactionRay, out interactionRayHit, interactionDistance ))
			{
				if (interactionRayHit.collider.gameObject.GetComponent<IInteractable>() != null)
				{
					var interactable = interactionRayHit.collider.gameObject.GetComponent<IInteractable>();
					interactable.Interact(transform);
				} //Check other interactables here
			}
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z + 1f), interactionRay.direction * 1000f);
	}
	void FixedUpdate() {
		rigidbodyR.velocity = transform.rotation * moveAmount;
	}

	void UnlockMouse() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		cursorVisible = true;
	}

	void LockMouse() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cursorVisible = false;
	}

	public void SetCameraMovement(bool value){
		cameraMovementActive = value;
		if(value)
			LockMouse();
		else
			UnlockMouse();
	}
}
