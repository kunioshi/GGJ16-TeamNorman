using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RuneId {
	Air = 0, Earth, Fire, Water
};

public class PlayerInventory: MonoBehaviour {
	// Initialize the rune count with 8 runes for the tutorial sake
	private int[] runeCounts = new int [4] {2, 2, 2, 2};

	public void AddRune (int runeId) {
		runeCounts [runeId]++;
	}

	public void RemoveRune (int runeId) {
		runeCounts [runeId]--;
	}
}
