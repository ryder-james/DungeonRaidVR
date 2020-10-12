﻿using UnityEngine;

namespace DungeonRaid.Collections {
	public abstract class MeterComponent : MonoBehaviour {
		[SerializeField] private float maxValue = 100;

		[Tooltip("Recharge rate, in units per second")]
		[SerializeField] protected float rechargeRate = 0;

		public bool IsRecharging { get; set; } = true;

		private float value;

		public float Value {
			get => value;
			set { this.value = Mathf.Clamp(value, 0, MaxValue); }
		}

		public abstract Color MeterColor { get; }
		public float MaxValue { get => maxValue; set => maxValue = value; }

		private void Awake() {
			Value = MaxValue;
		}
	}
}
