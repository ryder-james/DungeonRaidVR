using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	public abstract class Effect : ScriptableObject {
		protected const string EffectMenuPrefix = "Dungeon Raid/Effects/";

		[SerializeField] private bool applyToCaster = false;

		public bool ApplyToCaster { get => applyToCaster; set => applyToCaster = value; }

		public abstract void Apply(Character target);
	}
}
