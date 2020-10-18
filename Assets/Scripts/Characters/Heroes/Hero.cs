﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRaid.Characters.Heroes {
	[RequireComponent(typeof(HeroController))]
	public class Hero : Character {
		[SerializeField] private float speed = 1;
		[SerializeField] private float attackSpeed = 1;
		[SerializeField] private Weapon weapon = null;
		[SerializeField] private Ability[] abilities = null;

		public HeroController Controller { get; private set; }

		public float Speed { get => speed; set => speed = Mathf.Max(value, 0); }
		public float AttackSpeed { get => attackSpeed; set => attackSpeed = Mathf.Max(value, 0); }

		private readonly List<Ability> onCooldown = new List<Ability>();

		protected override void Start() {
			base.Start();

			Controller = GetComponent<HeroController>();

			foreach (Ability ability in abilities) {
				ability.Owner = this;
			}
		}

		protected override void Update() {
			base.Update();
		}

		public bool Cast(int index) {
			if (index >= 0 && index < abilities.Length) {
				return Cast(abilities[index]);
			}

			return false;
		}

		public bool Cast(Ability ability) {
			if (onCooldown.Contains(ability)) {
				return false;
			}

			StartCoroutine(nameof(RunCooldown), ability);
			return ability.Cast();
		}

		protected override float CalculateHealth(int heroCount) {
			return 1;
		}

		private IEnumerator RunCooldown(Ability ability) {
			onCooldown.Add(ability);
			yield return new WaitForSeconds(ability.Cooldown);
			onCooldown.Remove(ability);
		}
	}
}
