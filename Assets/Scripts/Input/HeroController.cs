using UnityEngine;
using UnityEngine.InputSystem;

using DungeonRaid.Characters.Heroes;

[RequireComponent(typeof(BodyMover))]
public class HeroController : MonoBehaviour {
	[SerializeField] private Hero hero = null;

	private Vector2 lookInput;
	private Vector3 movementInput;

	public BodyMover Mover { get; private set; }
	public Vector3 Direction { get; private set; }

	private void Start() {
		Mover = GetComponent<BodyMover>();
		Direction = transform.forward;
	}

	private void Update() {
		Aim(lookInput);
		Mover.MoveToward(movementInput, hero.Speed);
		if (movementInput.magnitude != 0) {
			Direction = movementInput.normalized;
		} else {
			Direction = transform.forward;
		}
	}

	public void OnMove(InputValue value) {
		Vector2 input = value.Get<Vector2>();
		movementInput = new Vector3(input.x, 0, input.y);
	}

	public void OnLook(InputValue value) {
		lookInput = value.Get<Vector2>();
	}

	public void OnFire(InputValue value) {
		hero.Cast(0);
	}

	private void Aim(Vector2 direction) {
		// TODO
	}
}
