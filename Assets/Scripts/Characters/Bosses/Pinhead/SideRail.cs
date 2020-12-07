using DungeonRaid.Characters.Heroes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRail : MonoBehaviour {
	[SerializeField] private float upHeight = 2;
	[SerializeField] private float liftSpeed = 2;
	[SerializeField] private float damage = 0;

	public System.Action<Collision> OnHit { get; set; }
	public System.Action<Collision> OnStay { get; set; }
	public System.Action<Collision> OnExit { get; set; }

	private Material material;
	private readonly Dictionary<Hero, float> heroHitCooldowns = new Dictionary<Hero, float>();
	private new Collider collider;

	private float downHeight = -2;

	private bool lifted = false;
	private bool lifting = false;
	private bool enteredThisFrame = false;
	private bool stayedThisFrame = false;
	private bool exitedThisFrame = false;

	private void Awake() {
		collider = GetComponent<Collider>();
		material = GetComponent<Renderer>().material;

		if (collider != null) {
			collider.enabled = false;
		}
		downHeight = material.GetFloat("_CutoffHeight");
	}

	private void LateUpdate() {
		enteredThisFrame = false;
		stayedThisFrame = false;
		exitedThisFrame = false;
	}

	public void Raise() {
		if (!lifted) {
			StartCoroutine(nameof(RaiseAsync), liftSpeed);
		}
	}

	public void Lower() {
		if (lifted) {
			StartCoroutine(nameof(LowerAsync), liftSpeed);
			heroHitCooldowns.Clear();
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (!enteredThisFrame && collision.gameObject.CompareTag("Hero")) {
			enteredThisFrame = true;

			Hero hero = collision.gameObject.GetComponent<Hero>();

			if (!heroHitCooldowns.ContainsKey(hero)) {
				OnHit?.Invoke(collision);

				hero.UpdateMeter("Health", -damage);
				heroHitCooldowns.Add(hero, 1);
			}
		}
	}

	private void OnCollisionStay(Collision collision) {
		if (!stayedThisFrame && collision.gameObject.CompareTag("Hero")) {
			stayedThisFrame = true;

			OnStay?.Invoke(collision);

			Hero hero = collision.gameObject.GetComponent<Hero>();

			if (heroHitCooldowns.ContainsKey(hero) && heroHitCooldowns[hero] <= 0) {
				hero.UpdateMeter("Health", -damage);
				heroHitCooldowns[hero] += 1;
			} else if (heroHitCooldowns.ContainsKey(hero)) {
				heroHitCooldowns[hero] -= Time.deltaTime;
			}
		}
	}

	private void OnCollisionExit(Collision collision) {
		if (!exitedThisFrame && collision.gameObject.CompareTag("Hero")) {
			exitedThisFrame = true;

			OnExit?.Invoke(collision);

			Hero hero = collision.gameObject.GetComponent<Hero>();
			heroHitCooldowns.Remove(hero);
		}
	}

	private IEnumerator RaiseAsync(float time) {
		if (lifting) {
			yield break;
		}

		if (collider != null) {
			collider.enabled = true;
		}

		float start = material.GetFloat("_CutoffHeight");
		float end = upHeight;

		lifting = true;
		for (float t = 0; t <= 1; t += Time.deltaTime * time) {
			material.SetFloat("_CutoffHeight", Mathf.Lerp(start, end, t));
			yield return null;
		}

		material.SetFloat("_CutoffHeight", end);
		lifted = true;
		lifting = false;
	}

	private IEnumerator LowerAsync(float time) {
		yield return new WaitUntil(() => !lifting);

		float start = material.GetFloat("_CutoffHeight");
		float end = downHeight;

		lifting = true;
		for (float t = 0; t <= 1; t += Time.deltaTime * time) {
			material.SetFloat("_CutoffHeight", Mathf.Lerp(start, end, t));
			yield return null;
		}

		material.SetFloat("_CutoffHeight", end);
		lifted = false;
		lifting = false;

		if (collider != null) {
			collider.enabled = false;
		}
	}
}
