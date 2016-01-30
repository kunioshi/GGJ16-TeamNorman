using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RuneId
{
	Air = 0,
	Earth = 1,
	Fire = 2,
	Water = 3}
;

public class PlayerStatus: MonoBehaviour
{
	public Vector2i playerGridPosition = new Vector2i (0, 0);

	public int playerEnergy = 120;

	// Initialize the rune inventory count with 8 runes for the tutorial sake
	public int[] runeCounts = new int [4] { 2, 2, 2, 2 };

	public void AddRuneToInventory (int runeId)
	{
		runeCounts [runeId]++;
	}

	public void RemoveRuneFromInventory (int runeId)
	{
		runeCounts [runeId]--;
	}
}
