using UnityEngine;

using Sirenix.OdinInspector;

using DungeonRaid.Characters;
using DungeonRaid.Abilities.Effects;
using DungeonRaid.Abilities.Effects.Improveables;

namespace DungeonRaid.Abilities {
	public abstract class Ability : ScriptableObject {
		[System.Serializable]
		public struct EffectSet {
			public Cost[] costs;
			public Effect[] effects;
		}

		protected const string AbilityMenuPrefix = "Dungeon Raid/Abilities/";

		[SerializeField] private DurationType durationType = DurationType.Instant;

		[ShowIf("DurationType", DurationType.Timed)]
		[SerializeField] private float duration = 0;

		[ShowIf("DurationType", DurationType.Channeled)]
		[SerializeField] private float maxChannelTime = 0;

		[Space]
		[SerializeField] private EffectSet effectSet;

		[HideIf("DurationType", DurationType.Instant)]
		[SerializeField] private EffectSet updateEffectSet;

		[HideIf("DurationType", DurationType.Instant)]
		[SerializeField] private EffectSet endEffectSet;

		[HideIf("DurationType", DurationType.Instant)]
		[SerializeField] private ImproveableEffect[] improveableEffects = null;

		[Space]
		[SerializeField, Min(0)] private float cooldown = 1;
		[SerializeField] private Sprite icon = null;

		public Character Owner { get; set; }
		public Sprite Icon { get => icon; set => icon = value; }

		public DurationType DurationType { get => durationType; set => durationType = value; }

		public float Cooldown { get => cooldown; private set => cooldown = value; }
		public bool IsChanneling { get; set; }

		private bool isRunning = false;
		private float totalRunTime = 0, sinceLastUpdate = 0;

		public void AddTime(float dt) {
			if (isRunning) {
				totalRunTime += dt;
				sinceLastUpdate += dt;
				if (sinceLastUpdate >= 1) {
					sinceLastUpdate = 0;
					AbilityUpdate();
				}
			}
		}

		public bool CanCast() {
			return CanCast(ref effectSet.costs);
		}

		public bool Cast() {
			return Begin();
		}

		protected virtual bool Begin() {
			bool success = TryCast(ref effectSet);

			if (durationType == DurationType.Instant) {
				End();
				return success;
			} else {
				isRunning = true;
				totalRunTime = 0;
			}

			return success;
		}

		protected virtual bool AbilityUpdate() {
			if (durationType == DurationType.Channeled) {
				if (!IsChanneling || totalRunTime > maxChannelTime) {
					return End();
				}
			} else if (durationType == DurationType.Timed) {
				if (totalRunTime > duration) {
					return End();
				}
			} else {
				return false;
			}

			foreach (ImproveableEffect improveable in improveableEffects) {
				improveable.Improve();
			}

			return TryCast(ref updateEffectSet);
		}

		protected virtual bool End() {
			isRunning = false;
			totalRunTime = 0;
			sinceLastUpdate = 0;

			TargetCast(improveableEffects);

			return TryCast(ref endEffectSet);
		}

		protected abstract bool TargetCast(Effect[] effects);

		private bool TryCast(ref EffectSet set) {
			bool canCast = CanCast(ref set.costs);

			if (canCast) {
				if (TargetCast(set.effects)) {
					foreach (Cost cost in set.costs) {
						Owner.PayCost(cost);
					}
				} else {
					return false;
				}
			}

			return canCast;
		}

		private bool CanCast(ref Cost[] costs) {
			bool canCast = true;

			foreach (Cost cost in costs) {
				if (Owner == null || !Owner.CanAfford(cost)) {
					canCast = false;
					break;
				}
			}

			return canCast;
		}
	}
}