using System.Linq;

using UnityEngine;

using DungeonRaid.Collections;
using DungeonRaid.Abilities;

namespace DungeonRaid.Characters {
	public abstract class Character : MonoBehaviour {
		[SerializeField] protected GameObject modelDataPrefab = null;
		[Space]
		[SerializeField] protected float hpMod = 1;
		[SerializeField] protected MeterComponent[] meters = null;
		[SerializeField] protected AmmoPool[] ammoPools = null;

		protected virtual void Start() {
			if (meters == null) {
				meters = new MeterComponent[0];
			}

			if (ammoPools == null) {
				ammoPools = new AmmoPool[0];
			}
			
			foreach (MeterComponent meter in meters) {
				if (meter.MeterName == "Health") {
					meter.MaxValue = CalculateHealth(GameObject.FindGameObjectsWithTag("Hero").Count());
					meter.Value = meter.MaxValue;
					break;
				}
			}
		}

		protected virtual void Update() {
			
		}

		public void UpdateMeter(string meterName, float amount) {
			MeterComponent meter = FindMeter(meterName);
			if (meter != null) {
				meter.Value += amount;
			}
		}

		public MeterComponent FindMeter(string meterName) {
			return meters.Where(m => m.MeterName == meterName).FirstOrDefault();
		}

		public bool HasMeter(string meterName) {
			return FindMeter(meterName) != null;
		}

		public void UpdateAmmoPool(string ammoName, float amount) {
			AmmoPool pool = FindAmmoPool(ammoName);
			if (pool != null) {
				pool.AmmoCount += amount;
			}
		}

		public AmmoPool FindAmmoPool(string ammoName) {
			return ammoPools.Where(p => p.name == ammoName).FirstOrDefault();
		}

		public bool HasAmmoPool(string ammoName) {
			return FindAmmoPool(ammoName) != null;
		}

		public bool CanAfford(Cost cost) {
			return cost.CheckCharacterCanAfford(this);
		}

		public bool PayCost(Cost cost) {
			return cost.PayFromCharacter(this);
		}

		protected abstract float CalculateHealth(int heroCount);
	}
}