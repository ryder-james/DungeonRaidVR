using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "LaunchArcProjectile", menuName = ObjectEffectMenuPrefix + "Launch Arc Projectile")]
	public class LaunchArcProjectileEffect : LaunchProjectileEffect {
		protected override Vector3 GetLaunchVelocity(Character caster, Character target, Vector3 point) {
			Vector3 start = caster.Nozzle;
			Vector3 dir = point - start;

			float height = dir.y;
			dir.y = 0;

			dir = Vector3.ClampMagnitude(dir, currentRange);

			float distance = dir.magnitude;
			dir.y = distance;
			distance += height;

			float velocity = Mathf.Sqrt(Mathf.Abs(distance) * Physics.gravity.magnitude);
			return velocity * dir.normalized;
		}
	}
}
