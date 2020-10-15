using System.Collections;
using UnityEngine;

using DungeonRaid.Effects.StatusEffects;
using DungeonRaid.Characters;
using DungeonRaid.Effects;

[CreateAssetMenu(fileName = "RepeatedEffect", menuName = StatusEffectMenuPrefix + "Repeated Effect")]
public class RepeatedEffectStatusEffect : StatusEffect {
	[SerializeField] private Effect effect = null;

	protected override void StartEffect(Character target) {
		InvokeRepeating(target, t => effect.Apply(t));
	}

	protected override void StopEffect(Character target) {}

}
