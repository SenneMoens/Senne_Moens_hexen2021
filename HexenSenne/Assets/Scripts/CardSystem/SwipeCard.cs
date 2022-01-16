using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCard : BaseCard<Piece<HexTileHandler>, HexTileHandler>
{
	public override List<HexTileHandler> Positions(Piece<HexTileHandler> piece, HexTileHandler tile)
	{
		_board.TryGetTile(piece, out HexTileHandler playerTile);
		List<HexTileHandler> tiles = GetNeighbours(playerTile);

		if (tiles.Contains(tile))
		{
			int direction = tile.GetDirectionFromTile(playerTile);

			_validTiles = new List<HexTileHandler>();

			HexTileHandler neighbourTile = null;
			if (TryGetNeighbour(playerTile, (((direction - 1) % 6) + 6) % 6, out neighbourTile))
				_validTiles.Add(neighbourTile);

			if (TryGetNeighbour(playerTile, ((direction % 6) + 6) % 6, out neighbourTile))
				_validTiles.Add(neighbourTile);

			if (TryGetNeighbour(playerTile, (((direction + 1) % 6) + 6) % 6, out neighbourTile))
				_validTiles.Add(neighbourTile);
		}
		else
		{
			_validTiles = new List<HexTileHandler>(tiles);
		}

		return _validTiles;
	}

	public override bool Execute(Piece<HexTileHandler> piece, HexTileHandler tile)
	{
		if (!_validTiles.Contains(tile)) return false;

		TakePiecesOnValidTiles();

		base.Execute(piece, tile);

		return true;
	}

	private List<HexTileHandler> GetNeighbours(HexTileHandler tile)
	{
		List<HexTileHandler> tiles = new List<HexTileHandler>();

		for (int i = 0; i < 6; i++)
		{
			if (TryGetNeighbour(tile, i, out HexTileHandler neighbour))
				tiles.Add(neighbour);
		}

		return tiles;
	}

	protected bool TryGetNeighbour(HexTileHandler startingTile, int direction, out HexTileHandler neighbourTile)
	{
		neighbourTile = null;
		Vector3Int offset = startingTile.HexTile.Direction(direction);

		return _grid.TryGetTileAt(startingTile.HexTile.Q + offset.x, startingTile.HexTile.R + offset.y, startingTile.HexTile.S + offset.z, out neighbourTile);
	}
}
