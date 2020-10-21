using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "Jump", menuName = EffectMenuPrefix + "Jump")]
	public class JumpEffect : Effect {
		[SerializeField] private float height = 1;

		public override void Apply(Character target) {
			Debug.Log($"jumped {height}");
		}
	}
}
