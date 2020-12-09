using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "SpeedChange", menuName = StatusEffectMenuPrefix + "Speed Change")]
	public class SpeedChangeStatusEffect : StatusEffect {
		[SerializeField] private StackType speedSetting = StackType.Set;
		[SerializeField] private float amount = 0;

		private float originalSpeed;

		protected override void StartEffect(Character target) {
			Hero hero = target as Hero;
			originalSpeed = hero.Speed;
			switch (speedSetting) {
			case StackType.Add:
				hero.Speed += amount;
				break;
			case StackType.Subtract:
				hero.Speed -= amount;
				break;
			case StackType.Multiply:
				hero.Speed *= amount;
				break;
			case StackType.Divide:
				hero.Speed /= amount;
				break;
			case StackType.Set:
				hero.Speed = amount;
				break;
			default:
				break;
			}
		}

		protected override void StopEffect(Character target) {
			(target as Hero).Speed = originalSpeed;
		}
	}
}
