using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "TravelTo", menuName = EffectMenuPrefix + "Travel To")]
	public class TravelToEffect : Effect {
		[SerializeField] private float speed = 1;

		public override void Apply(Hero caster, Character target, Vector3 point) {
			//Debug.Log($"travel to {worldSpacePoint} at {speed}");
		}
	}
}
