using UnityEngine;
using UnityEngine.InputSystem;

using DungeonRaid.Characters.Heroes;
using DungeonRaid.UI;

namespace DungeonRaid.Input {
	[RequireComponent(typeof(BodyMover))]
	public class HeroController : MonoBehaviour {
		[SerializeField] private Hero hero = null;

		public BodyMover Mover { get; private set; }
		public HeroHUD HUD { get; set; }
		public Animator Animator { get; private set; }
		public Vector3 Direction { get; private set; }

		private Vector2 lookInput;
		private Vector3 movementInput;
		private Canvas canvas;

		private bool usingMouse = false;

		private readonly bool[] channeling = new bool[3];

		private void Start() {
			Mover = GetComponent<BodyMover>();
			canvas = FindObjectOfType<Canvas>();
			Animator = GetComponentInChildren<Animator>();
			Direction = transform.forward;

			channeling[0] = false;
			channeling[1] = false;
			channeling[2] = false;
		}

		private void Update() {
			if (hero.IsDead) {
				return;
			}

			Aim(lookInput);

			Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

			float speedSubtracter = hero.Speed * (Mover.NormalizedAngle * 0.5f);
			float speed = hero.Speed - speedSubtracter;
			if (hero.CanMove) {
				Mover.MoveToward(move, speed);
			} else {
				Mover.MoveToward(Vector2.zero, speed);
			}

			Animator.SetFloat("DirX", Mover.Direction.x);
			Animator.SetFloat("DirZ", Mover.Direction.z);
			Animator.SetFloat("Speed", Mover.CurrentSpeed / Mathf.Max(BodyMover.SpeedMultiplier * hero.Speed, 0.00001f));
			Animator.SetFloat("AnimSpeed", 1 + Animator.GetFloat("Speed"));

			if (move.magnitude != 0) {
				Direction = -Mover.Direction;
			} else {
				Direction = -transform.forward;
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
				hero.BeginCast(index);
			} else {
				hero.EndCast(index);
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