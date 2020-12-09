using UnityEngine;

using Valve.VR;

namespace DungeonRaid.Characters.Bosses.Interactables {
	[RequireComponent(typeof(FixedJoint))]
    public class TriggerGrab : MonoBehaviour {
        [SerializeField] private SteamVR_Input_Sources hand = SteamVR_Input_Sources.RightHand;
        [SerializeField] private SteamVR_Behaviour_Pose controllerPose = null;
        [SerializeField] private SteamVR_Action_Boolean grabAction = null;

        private Grabable colliding, inHand;
		private FixedJoint joint;

		private void Start() {
			joint = GetComponent<FixedJoint>();
		}

		private void Update() {
			if (grabAction.GetLastStateDown(hand)) {
				if (colliding != null) {
					Grab();
				}
			} 
			
			if (grabAction.GetLastStateUp(hand)) {
				if (inHand != null) {
					Release();
				}
			}
		}

		private void Grab() {
			inHand = colliding;
			colliding = null;

			inHand.Grab();

			joint.connectedBody = inHand.GetComponent<Rigidbody>();
		}

		private void Release() {
			if (inHand != null) {
				joint.connectedBody = null;

				inHand.Release(controllerPose.GetVelocity(), controllerPose.GetAngularVelocity());

				inHand = null;
			}
		}

		private void SetGrabable(Grabable obj) {
			if (colliding != null) {
				return;
			}

			colliding = obj;
		}

		private void OnTriggerEnter(Collider other) {
            Grabable component = other.GetComponent<Grabable>();
            if (component != null) {
                SetGrabable(component);
			}
		}

		private void OnTriggerStay(Collider other) {
			OnTriggerEnter(other);
		}

		private void OnTriggerExit(Collider other) {
			colliding = null;
		}
	}
}
