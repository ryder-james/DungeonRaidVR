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

		public Hero Owner { get; set; }

		private void ApplyAll(Character target) {
			foreach (Effect e in onHitEffects) {
				e.Apply(Owner, target, Owner.TargetPoint);
			}

			Destroy(gameObject);
		}

		private void Hit(GameObject other) {
			Character hit = other.GetComponent<Character>();
			
			if (targetType.HasFlag(ProjectileTargetType.Hero) && other.CompareTag("Hero") ||
				targetType.HasFlag(ProjectileTargetType.Boss) && other.CompareTag("Boss") ||
				targetType.HasFlag(ProjectileTargetType.Environment)) {

				ApplyAll(hit);
			} else {
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == Owner.gameObject) {
				return;
			}

			if (useTriggerCollision) {
				Hit(other.gameObject);
			}

		}

		private void OnCollisionEnter(Collision collision) {
			Collider other = collision.collider;
			if (other.gameObject == Owner.gameObject) {
				return;
			}

			if (!useTriggerCollision) {
				Hit(other.gameObject);
			}

		}
	}
}
