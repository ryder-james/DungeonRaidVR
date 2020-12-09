using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "PlaySound", menuName = AVEffectMenuPrefix + "Play Sound")]
	public class PlaySoundEffect : Effect {
		[SerializeField] private bool playAtTarget = false;
		[SerializeField, Range(0, 1)] private float volume = 1;
		[SerializeField] private AudioClip clip = null;

		public override void Apply(Character caster, Character target, Vector3 point) {
			AudioSource.PlayClipAtPoint(clip, playAtTarget ? target.transform.position : point, volume * 50);
		}
	}
}
