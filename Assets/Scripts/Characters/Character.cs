using System.Linq;
using UnityEngine;

using DungeonRaid.Collections;

namespace DungeonRaid.Characters {
	public abstract class Character : MonoBehaviour {
		[SerializeField] protected GameObject modelDataPrefab = null;
		[Space]
		[SerializeField] protected float hpMod = 1;
		[SerializeField] protected MeterComponent[] meters = null;
		[SerializeField] protected AmmoPool[] ammoPools = null;

		protected virtual void Start() {
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
			return meters.Where(m => m.MeterName == meterName).First();
		}

		protected abstract float CalculateHealth(int heroCount);
	}
}