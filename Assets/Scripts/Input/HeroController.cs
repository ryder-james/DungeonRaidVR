using UnityEngine;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour {
	[SerializeField] private float speed = 1;

	private Rigidbody body;
	private Vector2 movementInput, lookInput;

	private void Start() {
		body = GetComponent<Rigidbody>();
	}

	private void Update() {
		Aim(lookInput);
		Move(new Vector3(movementInput.x, 0, movementInput.y));
	}

	public void OnMove(InputValue value) {
		movementInput = value.Get<Vector2>();
	}

	public void OnLook(InputValue value) {
		lookInput = value.Get<Vector2>();
	}

	private void Move(Vector3 direction) {
		if (direction.magnitude > 0) {
			body.velocity = transform.rotation * (Vector3.ClampMagnitude(direction, 1) * speed * 5);
			body.velocity = Vector3.ClampMagnitude(body.velocity, speed * 5);
		} else {
			body.velocity = Vector3.zero;
		}
	}

	private void Aim(Vector2 direction) {
		// TODO
	}
}
