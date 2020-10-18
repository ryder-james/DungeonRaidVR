using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "AttackSpeedChange", menuName = StatusEffectMenuPrefix + "Attack Speed Change")]
	public class AttackSpeedChangeStatusEffect : StatusEffect {
		[SerializeField] private StatChangeType changeType = StatChangeType.Add;
		[SerializeField, Min(0.0001f)] private float amount = 1;

		protected override void StartEffect(Character target) {
			switch (changeType) {
			case StatChangeType.Add:
				(target as Hero).AttackSpeed += amount;
				break;
			case StatChangeType.Subtract:
				(target as Hero).AttackSpeed -= amount;
				break;
			case StatChangeType.Multiply:
				(target as Hero).AttackSpeed *= amount;
				break;
			case StatChangeType.Divide:
				(target as Hero).AttackSpeed /= amount;
				break;
			default:
				break;
			}
		}

		protected override void StopEffect(Character target) {
			switch (changeType) {
			case StatChangeType.Add:
				(target as Hero).AttackSpeed -= amount;
				break;
			case StatChangeType.Subtract:
				(target as Hero).AttackSpeed += amount;
				break;
			case StatChangeType.Multiply:
				(target as Hero).AttackSpeed /= amount;
				break;
			case StatChangeType.Divide:
				(target as Hero).AttackSpeed *= amount;
				break;
			default:
				break;
			}
		}
	}
}
