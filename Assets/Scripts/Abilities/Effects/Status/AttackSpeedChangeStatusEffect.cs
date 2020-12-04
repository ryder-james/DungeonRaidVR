using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "AttackSpeedChange", menuName = StatusEffectMenuPrefix + "Attack Speed Change")]
	public class AttackSpeedChangeStatusEffect : StatusEffect {
		[SerializeField] private StackType changeType = StackType.Add;
		[SerializeField, Min(0.0001f)] private float amount = 1;

		protected override void StartEffect(Character target) {
			switch (changeType) {
			case StackType.Add:
				(target as Hero).AttackSpeed += amount;
				break;
			case StackType.Subtract:
				(target as Hero).AttackSpeed -= amount;
				break;
			case StackType.Multiply:
				(target as Hero).AttackSpeed *= amount;
				break;
			case StackType.Divide:
				(target as Hero).AttackSpeed /= amount;
				break;
			default:
				break;
			}
		}

		protected override void StopEffect(Character target) {
			switch (changeType) {
			case StackType.Add:
				(target as Hero).AttackSpeed -= amount;
				break;
			case StackType.Subtract:
				(target as Hero).AttackSpeed += amount;
				break;
			case StackType.Multiply:
				(target as Hero).AttackSpeed /= amount;
				break;
			case StackType.Divide:
				(target as Hero).AttackSpeed *= amount;
				break;
			default:
				break;
			}
		}
	}
}
