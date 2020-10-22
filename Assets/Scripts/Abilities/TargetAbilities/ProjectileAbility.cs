using UnityEngine;

using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities {
	[CreateAssetMenu(fileName = "Projectile", menuName = AbilityMenuPrefix + "Projectile")]
	public class ProjectileAbility : Ability {
		[SerializeField] private GameObject projectilePrefab = null;

		protected override bool TargetCast() {
			Vector3 start = Owner.Nozzle;
			Vector3 target = (Owner as Hero).TargetPoint;
			Vector3 dir = target - start;

			float height = dir.y;
			dir.y = 0;

			float distance = dir.magnitude;
			dir.y = distance;
			distance += height;

			float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);

			GameObject projObj = Instantiate(projectilePrefab, Owner.Nozzle, Quaternion.identity);
			Projectile proj = projObj.GetComponent<Projectile>();
			proj.GetComponent<Rigidbody>().AddForce(velocity * dir.normalized, ForceMode.VelocityChange);
			proj.Owner = Owner;
			proj.Effects = Effects;
			return true;
		}
	}
}
