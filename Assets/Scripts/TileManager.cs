using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Vector2i
{
    int x, y;

    public override bool Equals(object obj)
    {
        return obj is Vector2i && this == (Vector2i)obj;
    }

    public static bool operator ==(Vector2i v1, Vector2i v2)
    {
        return (v1.x == v2.x) && (v1.y == v2.y);
    }

    public static bool operator !=(Vector2i v1, Vector2i v2)
    {
        return !(v1 == v2);
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }
}

public class Tile
{
    public enum TileType
    {
        Plains = 0,
        Forest,
        Mountain,
        Cave,
        Lake,
        Graveyard,
        Volcano
    };

    public TileType Type { get; private set; }


    public Tile(TileType t)
    {
        Type = t;
    }
}

public class TileManager : MonoBehaviour {

    private Dictionary<Vector2i, Tile> _tiles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
