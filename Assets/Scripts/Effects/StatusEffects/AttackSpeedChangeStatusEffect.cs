using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Effects.StatusEffects {
	
	[CreateAssetMenu(fileName = "StatChangeEffect", menuName = StatusEffectMenuPrefix + "Stat Change")]
	public class AttackSpeedChangeStatusEffect : StatusEffect {
		[SerializeField] private float amount = 0;

		protected override void StartEffect(Character target) {
			(target as Hero).AttackSpeed += amount;
		}

		protected override void StopEffect(Character target) {
			(target as Hero).AttackSpeed -= amount;
		}
	}
}
