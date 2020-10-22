using UnityEngine;

using DungeonRaid.Abilities.Effects;
using DungeonRaid.Characters;

namespace DungeonRaid.Abilities {
	[RequireComponent(typeof(Collider), typeof(Rigidbody))]
	public class Projectile : MonoBehaviour {
		[System.Flags]
		public enum ProjectileTargetType {
			Hero = 1 << 0,
			Boss = 1 << 1
		}

		[SerializeField] private bool useTriggerCollision = true;
		[SerializeField] private ProjectileTargetType targetType = ProjectileTargetType.Boss;

		public Effect[] Effects { get; set; }
		public Character Owner { get; set; }

		private void ApplyAll(Character target) {
			foreach (Effect e in Effects) {
				e.Apply(target);
			}
		}

		private void Hit(GameObject other) {
			if ((other.CompareTag("Hero") && targetType.HasFlag(ProjectileTargetType.Hero)) ||
				(other.CompareTag("Boss") && targetType.HasFlag(ProjectileTargetType.Boss))) {
				ApplyAll(other.GetComponent<Character>());
			}
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject == Owner.gameObject) {
				return;
			}

			if (useTriggerCollision) {
				Hit(other.gameObject);
				Destroy(gameObject);
			}

		}

		private void OnCollisionEnter(Collision collision) {
			Collider other = collision.collider;
			if (other.gameObject == Owner.gameObject) {
				return;
			}

			if (!useTriggerCollision) {
				Hit(other.gameObject);
				Destroy(gameObject);
			}

		}
	}
}
