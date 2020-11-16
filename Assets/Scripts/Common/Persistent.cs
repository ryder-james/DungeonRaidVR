﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour
{
    protected virtual void Awake() {
		DontDestroyOnLoad(gameObject);

		foreach (Persistent persistentObj in FindObjectsOfType<Persistent>()) {
			if (persistentObj != this && persistentObj.GetType() == this.GetType()) {
				//DestroyImmediate(persistentObj);
			}
		}
	}
}