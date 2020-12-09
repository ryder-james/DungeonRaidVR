using UnityEngine;

using DungeonRaid.Characters;
using DungeonRaid.Characters.Heroes;

namespace DungeonRaid.Abilities.Effects.StatusEffects {
	[CreateAssetMenu(fileName = "Stun", menuName = StatusEffectMenuPrefix + "Stun")]
	public class StunStatusEffect : StatusEffect {
		protected override void StartEffect(Character target) {
			Hero hero = target as Hero;
			hero.IsStunned = true;
			if (hero.Animator != null) {
				hero.Animator.SetTrigger("Hit");
				hero.Animator.SetBool("IsDizzy", true);
			}
		}

		protected override void StopEffect(Character target) {
			Hero hero = target as Hero;
			if (hero.Animator != null) {
				hero.Animator.SetBool("IsDizzy", false);
				InvokeDelayed(Caster, target, Point, (c, t, p) => { t.IsStunned = false; }, 1.35f);
			} else {
				hero.IsStunned = false;
			}
		}
	}
}
