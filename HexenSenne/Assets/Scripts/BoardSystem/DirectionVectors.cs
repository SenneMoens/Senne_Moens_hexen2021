using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionVectors
{
	private static Vector3Int[] _directionVectors =
{
		new Vector3Int(1, 0, -1),
		new Vector3Int(1, -1, 0),
		new Vector3Int(0, -1, 1),
		new Vector3Int(-1, 0, 1),
		new Vector3Int(-1, 1, 0),
		new Vector3Int(0, 1, -1),
	};
	public static Vector3Int Get(int direction)
	{
		if (direction < 0 && direction >= 6)
			direction = ((direction % 6) + 6) % 6;

		return _directionVectors[direction];
	}

	public static int Get(Vector3Int direction)
	{
		for (int i = 0; i < _directionVectors.Length; i++)
		{
			if (direction == _directionVectors[i])
				return i;
		}

		return 0;
	}
}
