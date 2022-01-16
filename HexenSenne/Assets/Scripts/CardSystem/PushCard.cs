using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCard : SwipeCard
{
	public override bool Execute(Piece<HexTileHandler> piece, HexTileHandler tile)
	{
		if (!_validTiles.Contains(tile)) return false;

		_board.TryGetTile(piece, out HexTileHandler playerTile);

		foreach (HexTileHandler targetTile in _validTiles)
		{
			if (_board.TryGetPiece(targetTile, out Piece<HexTileHandler> targetPiece))
			{
				PushPiece(targetPiece, targetTile.GetDirectionFromTile(playerTile));
			}
		}

		gameObject.SetActive(false);

		return true;
	}

	private void PushPiece(Piece<HexTileHandler> piece, int direction)
	{
		if (_board.TryGetTile(piece, out HexTileHandler pieceTile) == false) return;


		if (!TryGetNeighbour(pieceTile, direction, out HexTileHandler targetTile))
		{
			_board.Take(piece);
			return;
		}

		if (_board.TryGetPiece(targetTile, out _)) return;

		_board.Move(piece, targetTile);
	}
}
