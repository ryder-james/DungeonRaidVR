using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	[CreateAssetMenu(fileName = "TravelEffect", menuName = EffectMenuPrefix + "Travel")]
	public class TravelEffect : Effect {
		[SerializeField] private float speed = 1;
		[SerializeField] private Vector3 worldSpacePoint = Vector3.zero;
		public override void Apply(Character target) {
			// TODO
		}
	}
}
