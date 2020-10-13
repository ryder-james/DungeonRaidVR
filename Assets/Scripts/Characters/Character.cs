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
				}
			}
		}

		protected virtual void Update() {

		}

		protected abstract float CalculateHealth(int heroCount);
	}
}