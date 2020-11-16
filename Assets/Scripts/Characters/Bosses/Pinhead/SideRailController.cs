using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRailController : ContinuousBehaviour {
	[SerializeField] private SideRail[] rails = null;

	protected override void Begin() {
		foreach (SideRail rail in rails) {
			rail.Raise();
		}
	}

	protected override void Channel() {

	}

	protected override void End() {
		foreach (SideRail rail in rails) {
			rail.Lower();
		}
	}

	protected override void ResetBehaviour() {

	}
}
