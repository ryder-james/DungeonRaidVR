using System.Collections;

using UnityEngine;

using DungeonRaid.Characters.Bosses.Interactables;

namespace DungeonRaid.Characters.Bosses.Behaviours {
	public class BehaviourButton : TriggerBehaviourToggle {
		[SerializeField] private float pressSpeed = 2;
		[SerializeField] private Vector3 pressOffset = Vector3.down;

		protected override IEnumerator ChangeState(bool toActiveState) {
			yield return new WaitUntil(() => toggleState != ToggleState.Transitioning);

			toggleState = ToggleState.Transitioning;

			Vector3 start = transform.position;
			Vector3 end = transform.position + (toActiveState ? pressOffset : -pressOffset);

			//Debug.Log(end);

			for (float t = 0; t < 1; t += Time.deltaTime * pressSpeed) {
				transform.position = Vector3.Lerp(start, end, t);
				yield return new WaitForEndOfFrame();
			}

			transform.position = end;

			toggleState = toActiveState ? ToggleState.On : ToggleState.Off;
		}
	}
}