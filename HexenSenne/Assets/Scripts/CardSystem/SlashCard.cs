using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashCard : BaseCard<Piece<HexTileHandler>, HexTileHandler>
{
	public override List<HexTileHandler> Positions(Piece<HexTileHandler> piece, HexTileHandler tile)
	{
		List<HexTileHandler> tiles = Direction(0);
		tiles.AddRange(Direction(1));
		tiles.AddRange(Direction(2));
		tiles.AddRange(Direction(3));
		tiles.AddRange(Direction(4));
		tiles.AddRange(Direction(5));

		if (tiles.Contains(tile))
		{
			_board.TryGetTile(piece, out HexTileHandler pieceTile);
			int direction = tile.GetDirectionFromTile(pieceTile);

			_validTiles = Direction(direction);
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

	private List<HexTileHandler> Direction(int direction, int maxSteps = int.MaxValue)
	{
		Vector3Int directionVector = DirectionVectors.Get(direction);
		return Collect(directionVector.x, directionVector.y, directionVector.z, maxSteps);
	}

	private List<HexTileHandler> Collect(int qOffset, int rOffset, int sOffset, int maxSteps = int.MaxValue)
	{
		List<HexTileHandler> tiles = new List<HexTileHandler>();

		if (!_board.TryGetTile(GameLoop.Instance.PlayerPiece, out HexTileHandler currentTile))
			return tiles;

		if (!_grid.TryGetCoordinatesAt(currentTile, out (int q, int r, int s) currentCoordinates))
			return tiles;

		int nextCoordinateQ = currentCoordinates.q + qOffset;
		int nextCoordinateR = currentCoordinates.r + rOffset;
		int nextCoordinateS = currentCoordinates.s + sOffset;

		_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out HexTileHandler nextPosition);

		int steps = 0;
		while (steps < maxSteps && nextPosition != null)
		{
			tiles.Add(nextPosition);

			nextCoordinateQ += qOffset;
			nextCoordinateR += rOffset;
			nextCoordinateS += sOffset;

			_grid.TryGetTileAt(nextCoordinateQ, nextCoordinateR, nextCoordinateS, out nextPosition);
			steps++;
		}

		return tiles;
	}
}
