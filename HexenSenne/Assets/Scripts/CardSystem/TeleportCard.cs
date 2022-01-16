using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeleportCard : BaseCard<Piece<HexTileHandler>, HexTileHandler>
{
	public override List<HexTileHandler> Positions(Piece<HexTileHandler> piece, HexTileHandler tile)
	{
		List<HexTileHandler> tiles = _grid.GetTiles()
			.Where(tile => _board.TryGetPiece(tile, out _) == false)
			.ToList();

		if (tiles.Contains(tile))
		{
			_validTiles = new List<HexTileHandler> { tile };
		}
		else
		{
			_validTiles = new List<HexTileHandler>();
		}

		return _validTiles;
	}

	public override bool Execute(Piece<HexTileHandler> piece, HexTileHandler tile)
	{
		if (!_validTiles.Contains(tile)) return false;

		_board.Move(piece, tile);

		base.Execute(piece, tile);

		return true;
	}
}
