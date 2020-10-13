using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Effects {
	[CreateAssetMenu(fileName = "JumpEffect", menuName = EffectMenuPrefix + "Jump")]
	public class JumpEffect : Effect {
		[SerializeField] private float height = 1;
		public override void Apply(Character target) {
			// TODO
		}
	}
}
