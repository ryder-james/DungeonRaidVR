using DungeonRaid.Characters.Heroes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroSelector : MonoBehaviour {
	[SerializeField, Range(1, 4)] private int initial = 1;
	[SerializeField] private StaticGameObjectArray heroPrefabs = null;
	[SerializeField] private GameObject[] lights = null;
	[SerializeField] private PlinthManager manager = null;

	public bool Active { get; set; } = false;
	public int Current { get; set; }
	
	private GameObject[] heroes;
	private PlayerInput input;

	private void Start() {
		Current = initial - 1;

		heroes = new GameObject[heroPrefabs.Length];
		heroes[Current] = CreateHero(Current);
	}

	public void Enable(PlayerInput input) {
		this.input = input;

		for (int i = 0; i < input.actionEvents.Count(); i++) {
			var evt = input.actionEvents[i];
			if (evt.actionName.Contains("Next")) {
				evt.AddListener(OnNext);
			} else if (evt.actionName.Contains("Prev")) {
				evt.AddListener(OnNext);
			} else if (evt.actionName.Contains("Start")) {
				evt.AddListener(OnStart);
			}
		}

		foreach (GameObject light in lights) {
			light.SetActive(true);
		}

		Active = true;
	}

	public void Disable() {
		if (input == null) {
			return;
		}

		for (int i = 0; i < input.actionEvents.Count(); i++) {
			var evt = input.actionEvents[i];
			if (evt.actionName.Contains("Next")) {
				evt.RemoveListener(OnNext);
			} else if (evt.actionName.Contains("Prev")) {
				evt.RemoveListener(OnNext);
			} else if (evt.actionName.Contains("Start")) {
				evt.RemoveListener(OnStart);
			}
		}

		foreach (GameObject light in lights) {
			light.SetActive(false);
		}

		Active = false;
	}

	public void OnNext(InputAction.CallbackContext ctx) {
		if (ctx.performed) {
			heroes[Current].SetActive(false);

			Current = (Current + 1) % heroes.Length;

			if (heroes[Current] == null) {
				heroes[Current] = CreateHero(Current);
			}
			heroes[Current].SetActive(true);
		}

	}

	public void OnPrev(InputAction.CallbackContext ctx) {
		if (ctx.performed) {
			heroes[Current].SetActive(false);

			Current--;
			if (Current < 0)
				Current = heroes.Length - 1;

			if (heroes[Current] == null) {
				heroes[Current] = CreateHero(Current);
			}
			heroes[Current].SetActive(true);
		}
	}

	public void OnStart(InputAction.CallbackContext ctx) {
		if (ctx.performed) {
			manager.GameStart();
		}
	}

	private GameObject CreateHero(int index) {
		GameObject heroObj = Instantiate(heroPrefabs[index], transform.parent, false);

		heroObj.transform.localScale = Vector3.one * 2.5f;
		heroObj.GetComponent<PlayerInput>().enabled = false;
		heroObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

		return heroObj;
	}

	private void OnDestroy() {
		Disable();
	}
}
