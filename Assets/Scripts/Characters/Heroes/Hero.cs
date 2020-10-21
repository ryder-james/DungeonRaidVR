﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DungeonRaid.Abilities;
using DungeonRaid.Input;

namespace DungeonRaid.Characters.Heroes {
	[RequireComponent(typeof(HeroController))]
	public class Hero : Character {
		[SerializeField] private float speed = 1;
		[SerializeField] private float attacksPerSecond = 1;
		[SerializeField] private Weapon weapon = null;
		[SerializeField] private Ability[] abilities = null;
		[SerializeField] private LayerMask targetableLayers = 0;

		public HeroController Controller { get; private set; }
		public Character TargetCharacter { get; set; }
		public Vector3? TargetPoint { get; set; }
		public Ability[] Abilities { get => abilities; set => abilities = value; }

		public float Speed { get => speed; set => speed = Mathf.Max(value, 0); }
		public float AttackSpeed { 
			get => attacksPerSecond;
			set { 
				attacksPerSecond = Mathf.Max(value, 0);
				fixedAttackDelay = 1 / attacksPerSecond;
				if (attackDelay > fixedAttackDelay) {
					attackDelay = fixedAttackDelay;
				}
			}
		}

		public bool IsAttacking { get; set; } = false;

		private readonly List<Ability> onCooldown = new List<Ability>();

		private float fixedAttackDelay, attackDelay;

		private void Awake() {
			Controller = GetComponent<HeroController>();

			for (int i = 0; i < abilities.Length; i++) {
				abilities[i] = Instantiate(abilities[i]);
				abilities[i].Owner = this;
			}

			Initialized = true;
		}

		protected override void Start() {
			base.Start();

			fixedAttackDelay = 1 / attacksPerSecond;
			attackDelay = 0;
		}

		protected override void Update() {
			base.Update();

			attackDelay -= Time.deltaTime;
			if (IsAttacking) {
				Attack();
			}
		}

		public bool CheckForTarget(Camera cam, Vector3 reticlePosition) {
			Vector3 dir = reticlePosition - cam.transform.position;
			dir.Normalize();
			Ray ray = new Ray(cam.transform.position, dir);
			if (Physics.Raycast(ray, out RaycastHit hit, 300, targetableLayers)) {
				Character target = hit.collider.GetComponentInParent<Character>();
				if (target != null) {
					TargetCharacter = target;
					TargetPoint = null;
				} else {
					TargetCharacter = null;
					TargetPoint = hit.point;
				}
				return true;
			} else {
				TargetCharacter = null;
				TargetPoint = null;
				return false;
			}
		}

		public bool Cast(int index) {
			if (index >= 0 && index < Abilities.Length) {
				return Cast(Abilities[index]);
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

		private void Attack() {
			if (attackDelay <= 0) {
				weapon.Attack();
				attackDelay = fixedAttackDelay;
			}
		}

		private IEnumerator RunCooldown(Ability ability) {
			onCooldown.Add(ability);
			yield return new WaitForSeconds(ability.Cooldown);
			onCooldown.Remove(ability);
		}
	}
}
