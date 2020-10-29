using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Physics {
	[RequireComponent(typeof(Collider))]
	public class ConveyorBelt : MonoBehaviour {
		[SerializeField] private Vector3 conveyorDirection = Vector3.forward;
		[SerializeField] private float speed = 3;

		private List<Rigidbody> bodies = new List<Rigidbody>();

		private void Update() {
			foreach (Rigidbody body in bodies) {
				body.velocity = conveyorDirection * speed;
			}
		}

		private void OnCollisionEnter(Collision collision) {
			Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
			if (body != null) {
				bodies.Add(body);
			}
		}

		private void OnCollisionExit(Collision collision) {
			Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
			if (body != null) {
				bodies.Remove(body);
			}
		}
	}
}

