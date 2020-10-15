using UnityEngine;
using UnityEngine.InputSystem;

using DungeonRaid.Characters.Heroes;

[RequireComponent(typeof(BodyMover))]
public class HeroController : MonoBehaviour {
	[SerializeField] private Hero hero = null;

	private Vector2 movementInput, lookInput;

	public BodyMover Mover { get; private set; }

	private void Start() {
		Mover = GetComponent<BodyMover>();
	}

	private void Update() {
		Aim(lookInput);
		Mover.MoveToward(new Vector3(movementInput.x, 0, movementInput.y), hero.Speed);
	}

	public void OnMove(InputValue value) {
		movementInput = value.Get<Vector2>();
	}

	public void OnLook(InputValue value) {
		lookInput = value.Get<Vector2>();
	}

	public void OnFire(InputValue value) {
		hero.UseAbility(0);
	}

	private void Aim(Vector2 direction) {
		// TODO
	}
}
