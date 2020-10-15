using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	[CreateAssetMenu(fileName = "TravelTo", menuName = EffectMenuPrefix + "Travel To")]
	public class TravelToEffect : Effect {
		[SerializeField] private float speed = 1;
		[SerializeField] private Vector3 worldSpacePoint = Vector3.zero;
		public override void Apply(Character target) {
			// TODO
		}
	}
}
