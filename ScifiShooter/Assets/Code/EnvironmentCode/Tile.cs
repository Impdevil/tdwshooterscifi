using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    floor,
    wall,
    other,
}

public class Tile : MonoBehaviour
{
    //X and Z coords
    Vector2 pos;
    public TileType type;
    private void Start()
    {
        pos.x = this.transform.position.x;
        pos.y = this.transform.position.z;

    }

}