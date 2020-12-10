using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Interactables {
	public abstract class Interactable : MonoBehaviour {
		public bool IsInteractable { get; set; } = true;
	}
}
