using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : IEquatable<HexTile>
{
	public int Q { get; }
	public int R { get; }
	public int S { get; }

	public float Width
		=> Mathf.Sqrt(3) * _size;

	public float Height
		=> 2 * _size;

	public HexTileHandler HexTileHandler { get; set; }

	private float _size = 1f;

	public HexTile(int q, int r, int s)
	{
		if (q + r + s != 0)
			throw new Exception("Faulty coordinates");

		Q = q;
		R = r;
		S = s;
	}

	public static bool operator ==(HexTile a, HexTile b)
		=> a.Q == b.Q && a.R == b.R && a.S == b.S;

	public static bool operator !=(HexTile a, HexTile b)
		=> !(a == b);

	public bool Equals(HexTile other)
		=> this == other;

	public override bool Equals(object o)
	{
		if (o == null)
			return false;

		HexTile hexTile = o as HexTile;

		return Equals(hexTile);
	}

	public override int GetHashCode()
	{
		int hashCode = -1013937445;
		hashCode = hashCode * -1521134295 + Q.GetHashCode();
		hashCode = hashCode * -1521134295 + R.GetHashCode();
		hashCode = hashCode * -1521134295 + S.GetHashCode();
		return hashCode;
	}

	public static HexTile Add(HexTile a, HexTile b)
	=> new HexTile(a.Q + b.Q, a.R + b.R, a.S + b.S);

	public static HexTile Add(HexTile a, Vector3Int vector)
		=> Add(a, new HexTile(vector.x, vector.y, vector.z));

	public static HexTile Subtract(HexTile a, HexTile b)
		=> new HexTile(a.Q - b.Q, a.R - b.R, a.S - b.S);

	public static HexTile Subtract(HexTile a, Vector3Int vector)
		=> Subtract(a, new HexTile(vector.x, vector.y, vector.z));

	public static HexTile Multiply(HexTile a, int k)
		=> new HexTile(a.Q * k, a.R * k, a.S * k);

	public static int Length(HexTile hexTile)
		=> Mathf.RoundToInt(Math.Abs(hexTile.Q) + Math.Abs(hexTile.R) + Math.Abs(hexTile.S) / 2);

	public int Length()
		=> Length(this);

	public static int Distance(HexTile a, HexTile b)
		=> Length(Subtract(a, b));

	public HexTile Normalized()
		=> Normalized(this);

	public static HexTile Normalized(HexTile hexTile)
	{
		int normalizedQ = Mathf.Clamp(hexTile.Q, -1, 1);
		int normalizedR = Mathf.Clamp(hexTile.R, -1, 1);
		int normalizedS = Mathf.Clamp(hexTile.S, -1, 1);

		return new HexTile(normalizedQ, normalizedR, normalizedS);
	}

	public Vector3Int Direction(int direction)
		=> DirectionVectors.Get(direction);

	public static HexTile Neighbour(HexTile hexTile, int direction)
		=> Add(hexTile, hexTile.Direction(direction));

	public Vector3 ToWorldPosition()
	{
		float x = _size * Mathf.Sqrt(3) * Q + Mathf.Sqrt(3) / 2 * R;
		float y = _size * 3 / 2 * R;

		return new Vector3(x, 0, y);
	}

	public Vector3Int ToVector3Int()
		=> new Vector3Int(Q, R, S);

	public override string ToString()
		=> $"({Q}, {R}, {S})";
}
