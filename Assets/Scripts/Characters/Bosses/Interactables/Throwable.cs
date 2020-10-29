using UnityEngine;

namespace DungeonRaid.Characters.Bosses.Interactables {
    public abstract class Throwable : Grabable {
		public override void Release(Vector3 releaseVelocity, Vector3 releaseAngularVelocity) {
			Throw(releaseVelocity, releaseAngularVelocity);
		}
		public abstract void Throw(Vector3 releaseVelocity, Vector3 releaseAngularVelocity);
    }
}
