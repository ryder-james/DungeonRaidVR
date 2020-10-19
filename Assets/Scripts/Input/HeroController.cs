using UnityEngine;
using UnityEngine.InputSystem;

using DungeonRaid.Characters.Heroes;
using DungeonRaid.UI;

namespace DungeonRaid.Input {
	[RequireComponent(typeof(BodyMover))]
	public class HeroController : MonoBehaviour {
		[SerializeField] private Hero hero = null;

		private Vector2 lookInput;
		private Vector3 movementInput;

		public BodyMover Mover { get; private set; }
		public Vector3 Direction { get; private set; }
		public HeroHUD HUD { get; set; }

		private bool usingMouse = false;

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

		public void OnAim(InputValue value) {
			lookInput = value.Get<Vector2>();
		}

		public void OnAimExact(InputValue value) {
			usingMouse = true;
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
			if (HUD != null) {
				if (usingMouse) {
					Vector3 viewPoint = Camera.main.ScreenToViewportPoint(direction);
					Vector3 worldPoint = Camera.main.ViewportToWorldPoint(viewPoint);
					Debug.Log(viewPoint);
					HUD.SetReticlePosition(worldPoint);
				} else {
					HUD.MoveReticle(direction);
				}
			}
		}
	}
}