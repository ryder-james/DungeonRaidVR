using UnityEngine;

using DungeonRaid.Characters;

namespace DungeonRaid.Abilities.Effects {
	[CreateAssetMenu(fileName = "ApplyToSphere", menuName = SuperEffectMenuPrefix + "Apply to Sphere")]
	public class ApplyToSphereEffect : Effect {
		[SerializeField] private LayerMask layerMask = 0;
		[SerializeField] private float radius = 1;
		[SerializeField] private Vector3 offset = Vector3.zero;
		[SerializeField] private bool useReticlePosition = false;
		[SerializeField] private Effect effect = null;

		public override void Apply(Character caster, Character target, Vector3 point) {
			Vector3 center = (useReticlePosition ? point : caster.transform.position) + offset;
			Collider[] overlaps = Physics.OverlapSphere(center, radius, layerMask, QueryTriggerInteraction.Collide);

			foreach (Collider collider in overlaps) {
				Character character = collider.GetComponentInParent<Character>();
				if (character != null) {
					effect.Apply(caster, character, point);
				}
			}
		}
	}
}