using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalGrid
{
	public List<HexTile> HexTiles = new List<HexTile>();

	public HexagonalGrid(int radius)
	{
		for (int q = -radius; q <= radius; q++)
		{
			int r1 = Mathf.Max(-radius, -q - radius);
			int r2 = Mathf.Min(radius, -q + radius);

			for (int r = r1; r <= r2; r++)
			{
				HexTiles.Add(new HexTile(q, r, -q - r));
			}
		}
	}
}
