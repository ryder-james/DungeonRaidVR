using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "TravelTo", menuName = MovementEffectMenuPrefix + "Travel To")]
	public class TravelToEffect : Effect {
		[SerializeField] private float speed = 1;

		public override void Apply(Character caster, Character target, Vector3 point) {
			Debug.Log($"travel to {point} at {speed}");
		}
	}
}
