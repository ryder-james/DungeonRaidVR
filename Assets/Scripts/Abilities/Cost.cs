using System;

using UnityEngine;

using Sirenix.OdinInspector;

using DungeonRaid.Collections;
using DungeonRaid.Characters;

namespace DungeonRaid.Abilities {
	[Serializable]
	public struct Cost {
		[SerializeField] private CostType type;

		[ShowIf("Type", CostType.Stat)]
		[SerializeField] private string meterName;

		[ShowIf("Type", CostType.Ammo)]
		[SerializeField] private string ammoName;

		[SerializeField] private float amount;

		public CostType Type { get => type; set => type = value; }
		public string AmmoName { get => ammoName; set => ammoName = value; }
		public string MeterName { get => meterName; set => meterName = value; }
		public float Amount { get => amount; set => amount = value; }

		public bool CheckCharacterCanAfford(Character target) {
			bool canAfford;

			switch (Type) {
			case CostType.Stat:
				MeterComponent meter = target.FindMeter(MeterName);
				if (meter != null) {
					canAfford = meter.Value >= Amount;
				} else {
					canAfford = false;
				}
				break;
			case CostType.Ammo:
				AmmoPool pool = target.FindAmmoPool(AmmoName);
				if (pool != null) {
					canAfford = pool.AmmoCount >= Amount;
				} else {
					canAfford = false;
				}
				break;
			default:
				canAfford = false;
				break;
			}

			return canAfford;
		}

		public bool PayFromCharacter(Character target) {
			bool canPay = CheckCharacterCanAfford(target);

			if (canPay) {
				switch (Type) {
				case CostType.Stat:
					target.UpdateMeter(MeterName, -Amount);
					break;
				case CostType.Ammo:
					target.UpdateAmmoPool(AmmoName, -Amount);
					break;
				default:
					canPay = false;
					break;
				}
			}

			return canPay;
		}
	}
}