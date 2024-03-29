﻿using UnityEngine;

namespace JCommon.Collections {
	[CreateAssetMenu(fileName = "Game Object Array", menuName = "Common/Game Object Array")]
	public class StaticGameObjectArray : ScriptableObject {
		[SerializeField] private GameObject[] gameObjects = null;

		public GameObject this[int index] => gameObjects[index];
		public int Length => gameObjects.Length;
	}
}