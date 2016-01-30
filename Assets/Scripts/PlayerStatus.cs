using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RuneId
{
	Life = 0,
	Earth = 1,
	Death = 2,
	Fire = 3}
;

public class PlayerStatus: MonoBehaviour
{
	public Vector2i playerGridPosition = new Vector2i (0, 0);

	// The player starts each day with 40 energy by default
	public int playerEnergy = 40;

	// Initialize the rune inventory count with 8 runes for the tutorial sake
	public int[] runeCounts = new int [4] { 2, 2, 2, 2 };

	// Inidcates whether the player has a bonus in Life(0), Earth(1), Death(2), or Fire(3)
	// Life: +1 vision radius
	// Earth: 1.5x energy
	//	Death: 2/3 energy cost on all tiles
	//	Fire: 50% chance to get double resources per tile
	public bool[] bonuses = new bool[4] {false, false, false, false};

	// The number of ingredients missing in last night's ritual, from 0 to 8
	// For each ingredient missing, the player takes an energy penalty of 5 the next day
	// At a penalty of 8, the game ends
	public int penalty = 0;

	public void AddRuneToInventory (int runeId)
	{
		runeCounts [runeId]++;
	}

	public void RemoveRuneFromInventory (int runeId)
	{
		runeCounts [runeId]--;
	}
}
