﻿using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "Jump", menuName = EffectMenuPrefix + "Jump")]
	public class JumpEffect : Effect {
		[SerializeField] private float height = 1;

		public override void Apply(Character caster, Character target, Vector3 point) {
			Debug.Log($"jumped {height}");
		}
	}
}
