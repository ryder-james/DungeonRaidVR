using System.Linq;

using UnityEngine;

using DungeonRaid.Collections;
using DungeonRaid.Abilities;
using System.Collections.Generic;
using DungeonRaid.Abilities.Effects.StatusEffects;
using DungeonRaid.Abilities.Effects;
using System;

namespace DungeonRaid.Characters {
	public abstract class Character : MonoBehaviour {
		[SerializeField] private Transform nozzle = null;
		[SerializeField] private Transform center = null;
		[SerializeField] protected GameObject modelDataPrefab = null;
		[Space]
		[SerializeField] protected float hpMod = 1;
		[SerializeField] private MeterComponent[] meters = null;
		[SerializeField] protected AmmoPool[] ammoPools = null;

		public MeterComponent[] Meters { get => meters; private set => meters = value; }
		private Dictionary<Type, List<MonoBehaviour>> ComponentPools { get; set; } = new Dictionary<Type, List<MonoBehaviour>>();

		public Animator Animator {
			get { 
				if (animator == null) {
					animator = GetComponentInChildren<Animator>();
				}

				return animator;
			}
			private set => animator = value;
		}

		public Vector3 Nozzle { get => nozzle.position; set => nozzle.position = value; }
		public Vector3 Center => center != null ? center.position : transform.position;
		public bool Initialized { get; protected set; }

		private Animator animator;

		protected virtual void Start() {
			if (Meters == null) {
				Meters = new MeterComponent[0];
			}

			if (ammoPools == null) {
				ammoPools = new AmmoPool[0];
			}
		}

		protected virtual void Update() {
			
		}

		public void UpdateHealth(int heroCount) {
			foreach (MeterComponent meter in Meters) {
				if (meter.MeterName == "Health") {
					meter.MaxValue = CalculateHealth(Mathf.Max(1, heroCount));
					meter.Value = meter.MaxValue;
					break;
				}
			}
		}

		public void UpdateMeter(string meterName, float amount) {
			MeterComponent meter = FindMeter(meterName);
			if (meter != null) {
				meter.Value += amount;
			}
		}

		public MeterComponent FindMeter(string meterName) {
			return Meters.Where(m => m.MeterName == meterName).FirstOrDefault();
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

		public T GetFreeBehaviour<T>(bool add = true) where T : MonoBehaviour {
			if (!ComponentPools.ContainsKey(typeof(T))) {
				if (add) {
					return AddBehaviour<T>();
				} else {
					return null;
				}
			}

			T result = null;
			foreach (T element in ComponentPools[typeof(T)]) {
				if (!element.enabled) {
					result = element;
					break;
				}
			}

			if (add && result == null) {
				result = AddBehaviour<T>();
			}

			return result;
		}

		public T AddBehaviour<T>() where T : MonoBehaviour {
			Type t = typeof(T);

			if (!ComponentPools.ContainsKey(t)) {
				ComponentPools[t] = new List<MonoBehaviour>();
			}

			T added = gameObject.AddComponent<T>();
			ComponentPools[t].Add(added);

			return added;
		}
	}
}