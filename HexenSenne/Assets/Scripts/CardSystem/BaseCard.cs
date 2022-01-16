using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseCard<TPiece, TTile> : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, ICard<TPiece, TTile>
{
	public event EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> CardBeginDragging;
	public event EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> CardEndDragging;

	protected Board<TPiece, TTile> _board;
	protected Grid<TTile> _grid;
	protected List<TTile> _validTiles = new List<TTile>();

	private RectTransform _transform;
	private Image _image;
	private Vector3 _pos = Vector3.zero;

	private void Awake()
	{
		_transform = GetComponent<RectTransform>();
		_image = GetComponent<Image>();
	}

	public void Initialize(Board<TPiece, TTile> board, Grid<TTile> grid)
	{
		_board = board;
		_grid = grid;

		gameObject.SetActive(false);
	}
	public virtual bool Execute(TPiece piece, TTile tile)
	{
		gameObject.SetActive(false);

		return true;
	}

	public virtual List<TTile> Positions(TPiece piece, TTile tile)
	{
		throw new NotImplementedException();
	}

	protected void TakePiecesOnValidTiles()
	{
		foreach (TTile hexagonTile in _validTiles)
		{
			if (_board.TryGetPiece(hexagonTile, out TPiece pieceInRange))
				_board.Take(pieceInRange);
		}
	}

	public void OnCardBeginDrag(CardEventArgs<BaseCard<TPiece, TTile>> eventArgs)
	{
		EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> handler = CardBeginDragging;
		handler?.Invoke(this, eventArgs);
	}

	public void OnCardEndDrag(CardEventArgs<BaseCard<TPiece, TTile>> eventArgs)
	{
		EventHandler<CardEventArgs<BaseCard<TPiece, TTile>>> handler = CardEndDragging;
		handler?.Invoke(this, eventArgs);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_pos = _transform.position;
		_image.raycastTarget = false;

		OnCardBeginDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
	}

	public void OnDrag(PointerEventData eventData)
	{
		_transform.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_transform.position = _pos;
		_image.raycastTarget = true;

		OnCardEndDrag(new CardEventArgs<BaseCard<TPiece, TTile>>(this));
	}
}

