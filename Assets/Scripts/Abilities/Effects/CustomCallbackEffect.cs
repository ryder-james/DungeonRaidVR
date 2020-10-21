using System;

using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "CustomCallback", menuName = EffectMenuPrefix + "Custom Callback")]
	public class CustomCallbackEffect : Effect {
		public Action<Character> Callback { get; set; }

		public override void Apply(Character target) {
			Callback(target);
		}
	}
}
