using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "CreateProjectile", menuName = EffectMenuPrefix + "Create Projectile")]
	public class CreateProjectileEffect : Effect {
		[SerializeField] private GameObject projectilePrefab = null;

		public override void Apply(Character caster) {
			Vector3 start = caster.Nozzle;
			Vector3 target = (caster as Hero).TargetPoint;
			Vector3 dir = target - start;

			float height = dir.y;
			dir.y = 0;

			float distance = dir.magnitude;
			dir.y = distance;
			distance += height;

			float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);

			GameObject projObj = Instantiate(projectilePrefab, caster.Nozzle, Quaternion.identity);
			Projectile proj = projObj.GetComponent<Projectile>();
			proj.GetComponent<Rigidbody>().AddForce(velocity * dir.normalized, ForceMode.VelocityChange);
			proj.Owner = caster;
		}
	}
}
