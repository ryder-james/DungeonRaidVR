using System;
using UnityEngine;

using Sirenix.OdinInspector;
using DungeonRaid.Collections;

namespace DungeonRaid.Characters.Abilities {
	[Serializable]
	public struct Cost {
		[SerializeField] private CostType type;

		[ShowIf("Type", CostType.Stat)]
		[SerializeField] private string meterName;

		[ShowIf("Type", CostType.Ammo)]
		[SerializeField] private string ammoName;

		[SerializeField] private float amount;

		public Cost(CostType type, string costName, float amount) {
			this.type = type;
			this.meterName = costName;
			this.ammoName = costName;
			this.amount = amount;
		}

		public bool CheckCharacterCanAfford(Character target) {
			bool canAfford;

			switch (type) {
			case CostType.Stat:
				MeterComponent meter = target.FindMeter(meterName);
				if (meter != null) {
					canAfford = meter.Value >= amount;
				} else {
					canAfford = false;
				}
				break;
			case CostType.Ammo:
				AmmoPool pool = target.FindAmmoPool(ammoName);
				if (pool != null) {
					canAfford = pool.AmmoCount >= amount;
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
				switch (type) {
				case CostType.Stat:
					target.UpdateMeter(meterName, -amount);
					break;
				case CostType.Ammo:
					target.UpdateAmmoPool(ammoName, -amount);
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