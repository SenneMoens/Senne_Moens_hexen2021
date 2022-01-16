using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : Singleton<GameLoop>
{
	[SerializeField] 
	private HexagonalGridHelper _helper = null;
	[SerializeField] 
	private List<BaseCard<Piece<HexTileHandler>, HexTileHandler>> _cardPrefabs = new List<BaseCard<Piece<HexTileHandler>, HexTileHandler>>();
	[SerializeField] 
	private Transform _deckTransform = null;

	public Piece<HexTileHandler> PlayerPiece => _playerPiece;
	public BaseCard<Piece<HexTileHandler>, HexTileHandler> SelectedCard => _selectedCard;

	private Board<Piece<HexTileHandler>, HexTileHandler> _board = new Board<Piece<HexTileHandler>, HexTileHandler>();
	private Grid<HexTileHandler> _grid = new Grid<HexTileHandler>();
	private Piece<HexTileHandler> _playerPiece = null;
	private Deck<BaseCard<Piece<HexTileHandler>, HexTileHandler>, Piece<HexTileHandler>, HexTileHandler> _deck;
	private BaseCard<Piece<HexTileHandler>, HexTileHandler> _selectedCard;

	private void Start()
	{
		Board<Piece<HexTileHandler>, HexTileHandler> board = new Board<Piece<HexTileHandler>, HexTileHandler>();
		HexagonalGrid hexagonalGrid = new HexagonalGrid(_helper.Radius);

		PlaceTiles(hexagonalGrid);
		RegisterTiles(hexagonalGrid, _grid);

		SpawnPlayer();
		SpawnEnemies();

		_deck = new Deck<BaseCard<Piece<HexTileHandler>, HexTileHandler>, Piece<HexTileHandler>, HexTileHandler>(_board, _grid);

		for (int i = 0; i < 10; i++)
		{
			BaseCard<Piece<HexTileHandler>, HexTileHandler> cardPrefab = _cardPrefabs[UnityEngine.Random.Range(0, _cardPrefabs.Count)];
			BaseCard<Piece<HexTileHandler>, HexTileHandler> card = Instantiate(cardPrefab, _deckTransform);
			_deck.Register(card);

			card.CardBeginDragging += (sender, eventArgs) => Select(eventArgs.Card);
			card.CardEndDragging += (sender, eventArgs) => DeselectAll();
		}

		_deck.FillHand();
	}

	private static void RegisterTiles(HexagonalGrid hexagonalGrid, Grid<HexTileHandler> grid)
	{
		foreach (HexTile hexTiles in hexagonalGrid.HexTiles)
		{
			grid.Register(hexTiles.HexTileHandler, hexTiles.Q, hexTiles.R, hexTiles.S);
		}
	}

	private void PlaceTiles(HexagonalGrid grid)
	{
		foreach (HexTile hexTile in grid.HexTiles)
		{
			HexTileHandler tile = Instantiate(_helper.HexTilePf, hexTile.ToWorldPosition(), Quaternion.identity, transform);
			tile.HexTile = hexTile;
			hexTile.HexTileHandler = tile;
		}
	}

	private void SpawnPlayer()
	{
		_playerPiece = SpawnPiece(_helper.PlayerPf, 0, 0, 0);
	}

	private void SpawnEnemies()
	{
		SpawnPiece(_helper.EnemyPf, 0, -3, 3);
		SpawnPiece(_helper.EnemyPf, 2, -2, 0);
		SpawnPiece(_helper.EnemyPf, 0, -1, 1);
		SpawnPiece(_helper.EnemyPf, 1, -1, 0);
		SpawnPiece(_helper.EnemyPf, -1, 1, 0);
		SpawnPiece(_helper.EnemyPf, 3, 0, -3);
		SpawnPiece(_helper.EnemyPf, -2, 2, 0);
		SpawnPiece(_helper.EnemyPf, 1, 2, -3);
	}

	private Piece<HexTileHandler> SpawnPiece(Piece<HexTileHandler> piecePrefab, int q, int r, int s)
	{
		if (_grid.TryGetTileAt(q, r, s, out HexTileHandler tile))
		{
			Piece<HexTileHandler> piece = Instantiate(piecePrefab, tile.transform.position, Quaternion.identity);
			_board.Place(piece, tile);

			_board.PieceMoved += (sender, eventArgs) => eventArgs.Piece.MoveTo(eventArgs.ToTile);
			_board.PiecePlaced += (sender, eventArgs) => eventArgs.Piece.PlaceAt(eventArgs.AtTile);
			_board.PieceTaken += (sender, eventArgs) => eventArgs.Piece.TakeFrom(eventArgs.FromTile);

			return piece;
		}
		else
		{
			throw new Exception("Given coordinates don't exist!");
		}
	}

	public void Highlight(HexTileHandler hexTileHandler)
	{
		if (hexTileHandler == null) return;
		if (SelectedCard == null) return;

		List<HexTileHandler> tiles = SelectedCard.Positions(PlayerPiece, hexTileHandler);

		foreach (HexTileHandler tile in tiles)
			if (tile != null) tile.Highlight = true;
	}

	public void Execute(HexTileHandler hexTileHandler)
	{
		_deck.PlayCard(SelectedCard, PlayerPiece, hexTileHandler);
	}

	public void UnhighlightAll()
	{
		List<HexTileHandler> tiles = _grid.GetTiles();

		foreach (HexTileHandler tile in tiles)
			tile.Highlight = false;
	}

	private void Select(BaseCard<Piece<HexTileHandler>, HexTileHandler> card)
	{
		_selectedCard = card;
	}

	public void DeselectAll()
	{
		_selectedCard = null;
	}
}
