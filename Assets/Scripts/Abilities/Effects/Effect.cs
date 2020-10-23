using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	public abstract class Effect : ScriptableObject {
		protected const string EffectMenuPrefix = "Dungeon Raid/Effects/";

		[SerializeField] private bool applyToCaster = false;

		public bool ApplyToCaster { get => applyToCaster; set => applyToCaster = value; }

		public abstract void Apply(Hero caster, Character target, Vector3 point);
	}
}
