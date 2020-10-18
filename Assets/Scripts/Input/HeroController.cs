using UnityEngine;
using UnityEngine.InputSystem;

using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Input {
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

			Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

			Mover.MoveToward(move, hero.Speed);
			if (move.magnitude != 0) {
				Direction = move.normalized;
			} else {
				Direction = transform.forward;
			}
		}

		public void OnMove(InputValue value) {
			movementInput = value.Get<Vector2>();
		}

		public void OnLook(InputValue value) {
			lookInput = value.Get<Vector2>();
		}

		public void OnAttack(InputValue _) {
			hero.IsAttacking = !hero.IsAttacking;
		}

		public void OnAbilityOne(InputValue _) {
			hero.Cast(0);
		}

		public void OnAbilityTwo(InputValue _) {
			hero.Cast(1);
		}

		public void OnAbilityThree(InputValue _) {
			hero.Cast(2);
		}

		private void Aim(Vector2 direction) {
			// TODO
		}
	}
}