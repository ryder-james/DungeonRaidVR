using UnityEngine;

namespace DungeonRaid.Characters.Bosses {
    public class Boss : Character {
		[SerializeField] private float initialHealth = 100;

		public float InitialHealth { get => initialHealth; set => initialHealth = value; }

		public static float DamageMultiplier => 1 - ((heroCount - 1) * 0.25f);

		private static int heroCount;

		protected override float CalculateHealth(int heroCount) {
			Boss.heroCount = heroCount;
			float health = InitialHealth * Mathf.Pow(1 + hpMod, heroCount - 1);
			return Mathf.Ceil(health);
		}
    }
}


