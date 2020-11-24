using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;
using Sirenix.OdinInspector;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "LaunchArcProjectile", menuName = ObjectEffectMenuPrefix + "Launch Arc Projectile")]
	public class LaunchArcProjectileEffect : LaunchProjectileEffect {
		//public override void Apply(Character caster, Character target, Vector3 point) {
		//	Vector3 start = caster.Nozzle;
		//	Vector3 dir = point - start;

		//	float height = dir.y;
		//	dir.y = 0;

		//	dir = Vector3.ClampMagnitude(dir, currentRange);

		//	float distance = dir.magnitude;
		//	dir.y = distance;
		//	distance += height;

		//	float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);

		//	GameObject projObj = Instantiate(projectilePrefab, caster.Nozzle, Quaternion.identity);
		//	Projectile proj = projObj.GetComponent<Projectile>();
		//	proj.GetComponent<Rigidbody>().AddForce(velocity * dir.normalized, ForceMode.VelocityChange);
		//	proj.Owner = caster;
		//}

		protected override Vector3 GetLaunchVelocity(Character caster, Character target, Vector3 point) {
			Vector3 start = caster.Nozzle;
			Vector3 dir = point - start;

			float height = dir.y;
			dir.y = 0;

			dir = Vector3.ClampMagnitude(dir, currentRange);

			float distance = dir.magnitude;
			dir.y = distance;
			distance += height;

			float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);

			return velocity * dir.normalized;
		}
	}
}
