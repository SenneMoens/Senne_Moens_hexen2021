using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece<TTile> : MonoBehaviour, IPointerClickHandler where TTile : MonoBehaviour, ITile
{
	public event EventHandler<PieceEventArgs<TTile>> Placed;
	public event EventHandler<PieceEventArgs<TTile>> Taken;
	public event EventHandler<PieceEventArgs<TTile>> Moved;

	public event EventHandler<ClickEventArgs<TTile>> Clicked;
	internal bool _hasMoved { get; set; }

	public void MoveTo(TTile tile)
	{
		OnMoved(new PieceEventArgs<TTile>(tile));
	}

	public void TakeFrom(TTile tile)
	{
		OnTaken(new PieceEventArgs<TTile>(tile));
	}

	public void PlaceAt(TTile tile)
	{
		OnPlaced(new PieceEventArgs<TTile>(tile));
	}

	protected virtual void OnPlaced(PieceEventArgs<TTile> eventArgs)
	{
		EventHandler<PieceEventArgs<TTile>> handler = Placed;
		handler?.Invoke(this, eventArgs);

		transform.position = eventArgs.Tile.transform.position;
		gameObject.SetActive(true);
	}

	protected virtual void OnMoved(PieceEventArgs<TTile> eventArgs)
	{
		EventHandler<PieceEventArgs<TTile>> handler = Moved;
		handler?.Invoke(this, eventArgs);

		transform.position = eventArgs.Tile.transform.position;
	}

	protected virtual void OnTaken(PieceEventArgs<TTile> eventArgs)
	{
		EventHandler<PieceEventArgs<TTile>> handler = Taken;
		handler?.Invoke(this, eventArgs);

		gameObject.SetActive(false);
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		OnClicked(new ClickEventArgs<TTile>(this));
	}

	protected virtual void OnClicked(ClickEventArgs<TTile> eventArgs)
	{
		EventHandler<ClickEventArgs<TTile>> handler = Clicked;
		handler?.Invoke(this, eventArgs);
	}
}

public class PieceEventArgs<TTile> : EventArgs
{
	public TTile Tile { get; }

	public PieceEventArgs(TTile tile)
	{
		Tile = tile;
	}
}

public class ActivateEventArgs : EventArgs
{
	public bool Status { get; }

	public ActivateEventArgs(bool status)
	{
		Status = status;
	}
}

public class ClickEventArgs<TTile> : EventArgs where TTile : MonoBehaviour, ITile
{
	public Piece<TTile> Piece { get; }

	public ClickEventArgs(Piece<TTile> piece)
	{
		Piece = piece;
	}
} 

