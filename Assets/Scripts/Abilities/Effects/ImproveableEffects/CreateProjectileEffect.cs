using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.Improveables {
	[CreateAssetMenu(fileName = "CreateProjectile", menuName = ImproveableEffectMenuPrefix + "Create Projectile")]
	public class CreateProjectileEffect : ImproveableEffect {
		[SerializeField] private float range = 4;
		[SerializeField] private StackType stackType = StackType.Add;
		[SerializeField, Min(0.01f)] private float rangeImprovement = 1;
		[SerializeField] private GameObject projectilePrefab = null;

		public override void Apply(Hero caster, Character target, Vector3 point) {
			Vector3 start = caster.Nozzle;
			Vector3 dir = point - start;

			float height = dir.y;
			dir.y = 0;

			dir = Vector3.ClampMagnitude(dir, range);

			float distance = dir.magnitude;
			dir.y = distance;
			distance += height;

			float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);

			GameObject projObj = Instantiate(projectilePrefab, caster.Nozzle, Quaternion.identity);
			Projectile proj = projObj.GetComponent<Projectile>();
			proj.GetComponent<Rigidbody>().AddForce(velocity * dir.normalized, ForceMode.VelocityChange);
			proj.Owner = caster;
		}

		public override void Improve() {
			switch (stackType) {
			case StackType.Add:
				range += rangeImprovement;
				break;
			case StackType.Subtract:
				range -= rangeImprovement;
				break;
			case StackType.Multiply:
				range *= rangeImprovement;
				break;
			case StackType.Divide:
				range /= rangeImprovement;
				break;
			default:
				break;
			}
		}
	}
}
