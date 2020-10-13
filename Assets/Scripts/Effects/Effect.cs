using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	public abstract class Effect : ScriptableObject {
		protected const string EffectMenuPrefix = "Dungeon Raid/Effects/";

		public abstract void Apply(Character target);
	}
}
