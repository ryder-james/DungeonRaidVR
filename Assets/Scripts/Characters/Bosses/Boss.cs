using UnityEngine;

using DungeonRaid.Collections;

namespace DungeonRaid.Characters.Bosses {
    public class Boss : Character {
		[SerializeField] private float initialHealth = 100;

		public float InitialHealth { get => initialHealth; set => initialHealth = value; }

		protected override float CalculateHealth(int heroCount) {
			float health = InitialHealth * Mathf.Pow(1 + hpMod, heroCount - 1);
			return Mathf.Ceil(health);
		}
    }
}


