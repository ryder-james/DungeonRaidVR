using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlinthManager : MonoBehaviour {
    [SerializeField] private HeroSelector[] plinths = null;

    public void OnPlayerJoined(PlayerInput playerInput) {
        if (!playerInput.name.Contains("Selector"))
            return;

        foreach (HeroSelector plinth in plinths) {
            if (!plinth.Active) {
                plinth.Enable(playerInput);
                playerInput.transform.SetParent(plinth.transform);
                break;
			}
		}
	}

	public void GameStart() {
        GameHandler handler = FindObjectOfType<GameHandler>();
        if (handler == null) {
            return;
        }

        List<int> activeHeroes = new List<int>();
	    foreach (HeroSelector plinth in plinths) {
            if (plinth.Active) {
                activeHeroes.Add(plinth.Current);
			}
		}
        handler.OnCharacterSelectDone(activeHeroes.ToArray());
	}
}
