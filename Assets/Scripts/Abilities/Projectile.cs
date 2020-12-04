using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;
using DungeonRaid.Characters.Bosses;

namespace DungeonRaid.Abilities {
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class Projectile : MonoBehaviour {
		[System.Flags]
		public enum ProjectileTargetType {
			Hero = 1 << 0,
			Boss = 1 << 1,
			Environment = 1 << 2
		}

		[SerializeField] private bool useTriggerCollision = true;
		[SerializeField] private ProjectileTargetType targetType = ProjectileTargetType.Boss;
		[SerializeField] private Effect[] onHitEffects = null;

		public Character Owner { get; set; }

		private void ApplyAll(Character target) {
			foreach (Effect e in onHitEffects) {
				if (Owner is Hero) {
					e.Apply(Owner, target, (Owner as Hero).TargetPoint);
				} else {
					e.Apply(Owner, target, Vector3.zero);
				}
			}

			Destroy(gameObject);
		}

		private void Hit(GameObject other) {
			Character hit = other.GetComponentInParent<Character>();
			
			if (targetType.HasFlag(ProjectileTargetType.Hero) && other.CompareTag("Hero") ||
				targetType.HasFlag(ProjectileTargetType.Boss) && other.CompareTag("Boss") ||
				targetType.HasFlag(ProjectileTargetType.Environment)) {

				ApplyAll(hit);
			} else {
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter(Collider other) {
			if (IsCasterOrChildOfCaster(other)) {
				return;
			}

			if (useTriggerCollision) {
				Hit(other.gameObject);
			}

		}

		private void OnCollisionEnter(Collision collision) {
			Collider other = collision.collider;
			if (IsCasterOrChildOfCaster(other)) {
				return;
			}

			if (!useTriggerCollision) {
				Hit(other.gameObject);
			}

		}

		private bool IsCasterOrChildOfCaster(Collider other) {
			if (Owner.gameObject == other.gameObject) {
				return true;
			} else if (other.transform.parent != null && Owner.gameObject == other.transform.parent.gameObject) {
				return true;
			} else {
				return false;
			}
		}
	}
}
