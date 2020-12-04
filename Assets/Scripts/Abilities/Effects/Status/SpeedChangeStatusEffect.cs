using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "SpeedChange", menuName = StatusEffectMenuPrefix + "Speed Change")]
	public class SpeedChangeStatusEffect : StatusEffect {
		[SerializeField] private float amount = 0;

		protected override void StartEffect(Character target) {
			(target as Hero).Speed += amount;
		}

		protected override void StopEffect(Character target) {
			(target as Hero).Speed -= amount;
		}
	}
}
