using UnityEngine;

using Sirenix.OdinInspector;

using DungeonRaid.Characters;
using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Abilities {
	public abstract class Ability : ScriptableObject {
		protected const string AbilityMenuPrefix = "Dungeon Raid/Abilities/";

		[SerializeField] private Cost[] costs = null;
		[SerializeField] private Effect[] effects = null;

		[SerializeField] private DurationType durationType = DurationType.Instant;

		[ShowIf("DurationType", DurationType.Timed)]
		[SerializeField] private float duration = 0;

		[ShowIf("DurationType", DurationType.Channeled)]
		[SerializeField] private float maxChannelTime = 0;

		[SerializeField, Min(0)] private float cooldown = 1;

		[SerializeField] private Sprite icon = null;

		public Character Owner { get; set; }
		public DurationType DurationType { get => durationType; set => durationType = value; }
		public float Cooldown { get => cooldown; private set => cooldown = value; }

		public Sprite Icon { get => icon; set => icon = value; }
		protected Effect[] Effects { get => effects; set => effects = value; }
		protected float Duration { get => duration; set => duration = value; }
		protected float MaxChannelTime { get => maxChannelTime; set => maxChannelTime = value; }

		public bool CanCast() {
			bool canCast = true;

			foreach (Cost cost in costs) {
				if (Owner == null || !Owner.CanAfford(cost)) {
					canCast = false;
					break;
				}
			}

			return canCast;
		}

		public bool Cast() {
			bool canCast = CanCast();

			if (canCast) {
				if (TargetCast()) {
					foreach (Cost cost in costs) {
						Owner.PayCost(cost);
					}
				} else {
					return false;
				}
			}

			return canCast;
		}

		protected abstract bool TargetCast();
	}
}