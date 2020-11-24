using System;

using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "CustomCallback", menuName = StatusEffectMenuPrefix + "Custom Callback")]
	public class CustomCallbackStatusEffect : StatusEffect {
		public Action<Character> StartCallback { get; set; }
		public Action<Character> EndCallback { get; set; }

		protected override void StartEffect(Character target) {
			StartCallback(target);
		}

		protected override void StopEffect(Character target) {
			EndCallback(target);
		}
	}
}