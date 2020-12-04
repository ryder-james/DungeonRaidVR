using DungeonRaid.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "EffectSet", menuName = SuperEffectMenuPrefix + "Effect Set")]
	public class EffectSet : Effect {
		[SerializeField] private Effect[] effects = null;

		public override void Apply(Character caster, Character target, Vector3 point) {
			foreach (Effect effect in effects) {
				effect.Apply(caster, target, point);
			}
		}
	}
}