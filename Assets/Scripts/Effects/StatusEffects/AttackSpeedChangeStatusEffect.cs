using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;
using Assets.Scripts.Characters.Heroes;

namespace DungeonRaid.Effects.StatusEffects {
	
	[CreateAssetMenu(fileName = "AttackSpeedChange", menuName = StatusEffectMenuPrefix + "Attack Speed Change")]
	public class AttackSpeedChangeStatusEffect : StatusEffect {
		[SerializeField] private ChangeType changeType = ChangeType.Add;
		[SerializeField, Min(0.0001f)] private float amount = 1;

		protected override void StartEffect(Character target) {
			switch (changeType) {
			case ChangeType.Add:
				(target as Hero).AttackSpeed += amount;
				break;
			case ChangeType.Subtract:
				(target as Hero).AttackSpeed -= amount;
				break;
			case ChangeType.Multiply:
				(target as Hero).AttackSpeed *= amount;
				break;
			case ChangeType.Divide:
				(target as Hero).AttackSpeed /= amount;
				break;
			default:
				break;
			}
		}

		protected override void StopEffect(Character target) {
			switch (changeType) {
			case ChangeType.Add:
				(target as Hero).AttackSpeed -= amount;
				break;
			case ChangeType.Subtract:
				(target as Hero).AttackSpeed += amount;
				break;
			case ChangeType.Multiply:
				(target as Hero).AttackSpeed /= amount;
				break;
			case ChangeType.Divide:
				(target as Hero).AttackSpeed *= amount;
				break;
			default:
				break;
			}
		}
	}
}
