using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;
using Sirenix.OdinInspector;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "LaunchProjectile", menuName = EffectMenuPrefix + "Launch Projectile")]
	public class LaunchProjectileEffect : ChannelableEffect {
		[SerializeField] private float range = 4;
		[SerializeField] private StackType stackType = StackType.None;
		[SerializeField, Min(0.01f), HideIf("stackType", StackType.None)] private float rangeImprovement = 1;
		[SerializeField] private GameObject projectilePrefab = null;

		protected float currentRange;

		public override void Apply(Character caster, Character target, Vector3 point) {
			GameObject projObj = Instantiate(projectilePrefab, caster.Nozzle, Quaternion.identity);
			Projectile proj = projObj.GetComponent<Projectile>();
			proj.GetComponent<Rigidbody>().AddForce(GetLaunchVelocity(caster, target, point), ForceMode.VelocityChange);
			proj.Owner = caster;
		}

		protected virtual Vector3 GetLaunchVelocity(Character caster, Character target, Vector3 point) {
			Vector3 start = caster.Nozzle;
			Vector3 dir = (point - start) * 2;

			return dir;
		}

		protected override void Begin() {
			currentRange = range;
		}

		protected override void Channel() {
			switch (stackType) {
			case StackType.Add:
				currentRange += rangeImprovement;
				break;
			case StackType.Subtract:
				currentRange -= rangeImprovement;
				break;
			case StackType.Multiply:
				currentRange *= rangeImprovement;
				break;
			case StackType.Divide:
				currentRange /= rangeImprovement;
				break;
			case StackType.Set:
				currentRange = rangeImprovement;
				break;
			default:
				break;
			}
		}

		protected override void End() {}
	}
}
