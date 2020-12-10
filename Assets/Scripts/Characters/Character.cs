﻿using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using DungeonRaid.Collections;
using DungeonRaid.Abilities;

namespace DungeonRaid.Characters {
	public abstract class Character : MonoBehaviour {
		[SerializeField] private Transform nozzle = null;
		[SerializeField] private Transform center = null;
		[Space]
		[SerializeField] protected float hpMod = 1;
		[SerializeField] private MeterComponent[] meters = null;
		[SerializeField] protected AmmoPool[] ammoPools = null;
		[Space]
		[SerializeField] private AudioSource hitSound = null;
		[SerializeField] private AudioSource deathSound = null;

		public MeterComponent[] Meters { get => meters; private set => meters = value; }

		public Action<MeterComponent, float> OnMeterSpent { get; set; }
		public Action<AmmoPool, float> OnAmmoSpent { get; set; }
		public GameHandler Game { get; set; }

		public bool IsDead => FindMeter("Health").Value <= 0;
		public bool IsStunned { get; set; } = false;

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

		public Action OnHit { get; set; }
		public Action OnDeath { get; set; }
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

		public void UpdateMeter(string meterName, float amount, bool isSpending = false) {
			if (IsDead) {
				return;
			}

			MeterComponent meter = FindMeter(meterName);
			if (meter != null) {
				if (isSpending) {
					OnMeterSpent?.Invoke(meter, amount);
				}
				meter.Value += amount;
			}

			if (meterName == "Health") {
				if (Animator != null) {
					if (!IsStunned) {
						Animator.SetTrigger("Hit");
					}
					Animator.SetFloat("Health", meter.NormalizedValue);
				}
				if (hitSound != null) {
					hitSound.Play();
				}
				OnHit?.Invoke();

				if (meter.Value <= 0) {
					if (deathSound != null) {
						deathSound.Play();
					}
					OnDeath?.Invoke();
				}
			}
		}

		public MeterComponent FindMeter(string meterName) {
			return Meters.Where(m => m.MeterName == meterName).FirstOrDefault();
		}

		public bool HasMeter(string meterName) {
			return FindMeter(meterName) != null;
		}

		public void UpdateAmmoPool(string ammoName, float amount, bool isSpending = false) {
			if (IsDead) {
				return;
			}

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
			if (IsDead) {
				return false;
			}

			return cost.CheckCharacterCanAfford(this);
		}

		public bool PayCost(Cost cost) {
			if (IsDead) {
				return false;
			}
			return cost.PayFromCharacter(this);
		}

		protected abstract float CalculateHealth(int heroCount);

		public T GetFreeBehaviour<T>(bool add = true) where T : MonoBehaviour {
			T result = null;

			if (!ComponentPools.ContainsKey(typeof(T))) {
				if (add) {
					result = AddBehaviour<T>();
					result.enabled = true;
					return result;
				} else {
					return null;
				}
			}

			foreach (T element in ComponentPools[typeof(T)]) {
				if (!element.enabled) {
					result = element;
					break;
				}
			}

			if (add && result == null) {
				result = AddBehaviour<T>();
			}

			result.enabled = true;
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