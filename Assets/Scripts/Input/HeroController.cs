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
		private Canvas canvas;

		private readonly bool[] channeling = new bool[3];

		private void Start() {
			Mover = GetComponent<BodyMover>();
			canvas = FindObjectOfType<Canvas>();
			Direction = transform.forward;

			channeling[0] = false;
			channeling[1] = false;
			channeling[2] = false;
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
			if (channeling[1] || channeling[2]) {
				return;
			}

			channeling[0] = !channeling[0];
			Cast(0);
		}

		public void OnAbilityTwo(InputValue _) {
			if (channeling[0] || channeling[2]) {
				return;
			}

			channeling[1] = !channeling[1];
			Cast(1);
		}

		public void OnAbilityThree(InputValue _) {
			if (channeling[0] || channeling[1]) {
				return;
			}

			channeling[2] = !channeling[2];
			Cast(2);
		}

		private void Cast(int index) {
			if (channeling[index]) {
				if (hero.BeginCast(index)) {
					HUD.ShowCooldown(index);
				}
			} else {
				if (hero.EndCast(index)) {
					HUD.ShowCooldown(index);
				}
			}
		}

		private void Aim(Vector2 direction) {
			if (HUD != null) {
				if (usingMouse) {
					HUD.SetReticlePosition(direction / canvas.scaleFactor);
				} else {
					HUD.MoveReticle(direction);
				}
			}
		}
	}
}