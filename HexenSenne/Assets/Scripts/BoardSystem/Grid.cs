using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.Commons;
using System.Linq;

public class Grid<TTile>
{
	private BidirectionalDictionary<TTile, (int q, int r, int s)> _tiles = new BidirectionalDictionary<TTile, (int q, int r, int s)>();

	public void Register(TTile tile, int q, int r, int s)
	{
		_tiles.Add(tile, (q, r, s));
	}
	public bool TryGetTileAt(int q, int r, int s, out TTile tile)
		=> _tiles.TryGetKey((q, r, s), out tile);

	public bool TryGetCoordinatesAt(TTile tile, out (int q, int r, int s) coordinate)
		=> _tiles.TryGetValue(tile, out coordinate);

	public List<TTile> GetTiles()
		=> _tiles.Keys.ToList();
}
