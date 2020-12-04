using UnityEngine;

using Sirenix.OdinInspector;

using DungeonRaid.Characters.Heroes;
using DungeonRaid.Abilities.Effects;
using DungeonRaid.Characters;

namespace DungeonRaid.Abilities {
	public abstract class Ability : ScriptableObject {
		public delegate void AbilityNotification();

		[System.Serializable]
		public struct EffectSet {
			public Cost[] costs;
			public Effect[] effects;
		}

		protected const string AbilityMenuPrefix = "Dungeon Raid/Abilities/";

		[SerializeField] private DurationType durationType = DurationType.Instant;

		[ShowIf("DurationType", DurationType.Timed)]
		[SerializeField, Min(1)] private float duration = 1;

		[ShowIf("DurationType", DurationType.Channeled)]
		[SerializeField, Min(1)] private int maxChannelTime = 1;

		[Space]
		[SerializeField] private EffectSet effectSet;

		[HideIf("DurationType", DurationType.Instant)]
		[SerializeField] private EffectSet updateEffectSet;

		[HideIf("DurationType", DurationType.Instant)]
		[SerializeField] private EffectSet endEffectSet;

		[HideIf("DurationType", DurationType.Instant)]
		[SerializeField] private ChannelableEffect[] channelableEffects = null;

		[Space]
		[SerializeField, Min(0)] private float cooldown = 1;
		[SerializeField] private Sprite icon = null;

		public Sprite Icon { get => icon; set => icon = value; }
		public Character Owner { get; set; }
		public AbilityNotification OnAbilityBeginCast { get; set; }
		public AbilityNotification OnAbilityUpdate { get; set; }
		public AbilityNotification OnAbilityEndCast { get; set; }

		protected Vector3 TargetPoint => Owner is Hero ? (Owner as Hero).TargetPoint : Vector3.zero;

		public DurationType DurationType { get => durationType; set => durationType = value; }

		public float Cooldown { get => cooldown; private set => cooldown = value; }
		public bool IsChanneling { get; set; }

		private bool isRunning = false;
		private float totalRunTime, sinceLastUpdate;
		private int channels = 0;

		public void AddTime(float dt) {
			if (isRunning) {
				sinceLastUpdate += dt;
				AbilityUpdate();
			}
		}

		public bool CanCast() {
			return CanCast(ref effectSet.costs);
		}

		public bool Cast() {
			return Begin();
		}

		protected virtual bool Begin() {
			OnAbilityBeginCast?.Invoke();
			bool success = TryCast(ref effectSet);

			foreach (ChannelableEffect effect in channelableEffects) {
				effect.BeginChannel();
			}

			if (durationType == DurationType.Instant) {
				End();
			} else if (success) {
				isRunning = true;
				if (durationType == DurationType.Channeled) {
					foreach (Cost cost in updateEffectSet.costs) {
						if (cost.Type == CostType.Stat) {
							Owner.FindMeter(cost.MeterName).IsRecharging = false;
						}
					}
				}
			}

			return success;
		}

		protected virtual bool AbilityUpdate() { 
			if (durationType == DurationType.Instant) {
				return End();
			}

			if (sinceLastUpdate >= 1) {
				OnAbilityUpdate?.Invoke();
				foreach (ChannelableEffect effect in channelableEffects) {
					effect.ChannelUpdate();
				}
				channels++;

				sinceLastUpdate = 0;

				return TryCast(ref updateEffectSet);
			} else {
				if (durationType == DurationType.Channeled) {
					if (!IsChanneling || channels >= maxChannelTime) {
						return End();
					}
				} else if (durationType == DurationType.Timed) {
					if (totalRunTime >= duration) {
						return End();
					}
				}

				return false;
			}
		}

		protected virtual bool End() {
			isRunning = false;
			totalRunTime = 0;
			sinceLastUpdate = 0;
			channels = 0;

			OnAbilityEndCast?.Invoke();

			foreach (ChannelableEffect effect in channelableEffects) {
				effect.EndChannel();
			}

			if (durationType == DurationType.Channeled) {
				foreach (Cost cost in updateEffectSet.costs) {
					if (cost.Type == CostType.Stat) {
						Owner.FindMeter(cost.MeterName).IsRecharging = true;
					}
				}
			}

			TargetCast(channelableEffects);

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