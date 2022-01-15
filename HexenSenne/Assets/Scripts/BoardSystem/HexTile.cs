using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : IEquatable<HexTile>
{
    public int q;
    public int r;
    public int s;

    public float Size = 1f;

    public float Width = Mathf.Sqrt(3);

    public float Height = 2f;

    //public HexTile[] _directionvectors =
    //{
    //    new HexTile(1,0,-1),
    //    new HexTile(1,-1,0),
    //    new HexTile(0,-1,1),
    //    new HexTile(-1,0,1),
    //    new HexTile(-1,1,0),
    //    new HexTile(0,1,-1),
    //};


    public HexTile(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }

    public bool Equals(HexTile other)
    {
        throw new NotImplementedException();
    }

    public Vector3 ToWorldPosition()
    {
        float x = Size * Mathf.Sqrt(3) * q + Mathf.Sqrt(3) / 2 * r;
        float y = Size * 3 / 2 * r;

        return new Vector3(x, 0, y);
    }
}
