﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DungeonRaid.Characters.Heroes;
using DungeonRaid.Characters.Bosses.Interactables;
using DungeonRaid.Abilities.Effects;

namespace DungeonRaid.Characters.Bosses.Pinhead {
	public class BowlingBall : Throwable {
		[SerializeField] private float damage = 2;
		[SerializeField] private float speed = 10;
		[SerializeField] private Effect dizzyEffect = null;
		[SerializeField] private AudioSource rollingSound = null;
		[SerializeField] private AudioSource hitSound = null;

		private List<Hero> hit = new List<Hero>();

		private bool hasLanded = true;

		protected override void Start() {
			base.Start();
			dizzyEffect = Instantiate(dizzyEffect);
		}

		public override void Grab() {

		}

		public void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Hero")) {
				float dmg = -damage * Boss.DamageMultiplier;
				if (other.TryGetComponent(out Hero hero) && !hit.Contains(hero))  {
					hit.Add(hero);
					dizzyEffect.Apply(hero, hero, Vector3.zero);
					hero.UpdateMeter("Health", dmg);
				}
			}
		}

		public void StopRollingSound() {
			StartCoroutine(nameof(Decrescendo));
			hasLanded = true;
		}

		public override void Throw(Vector3 releaseVelocity, Vector3 releaseAngularVelocity) {
			hit.Clear();
			body.constraints |= RigidbodyConstraints.FreezePositionX;
			body.velocity = new Vector3(0, -speed, speed);
			body.angularVelocity = releaseAngularVelocity;
			hasLanded = false;
		}

		private void OnCollisionEnter(Collision collision) {
			if (!hasLanded && collision.gameObject.CompareTag("Ground")) {
				hitSound.Play();
				rollingSound.Play();
				hasLanded = true;
			}
		}

		private IEnumerator Decrescendo() {
			if (!rollingSound.isPlaying) {
				yield break;
			}

			float start = rollingSound.volume;
			float end = 0;
			for (float t = 0; t < 1; t += Time.deltaTime) {
				rollingSound.volume = Mathf.Lerp(start, end, t);
				yield return new WaitForEndOfFrame();
			}

			rollingSound.Stop();
			rollingSound.volume = start;
		}
	}
}
