using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HexTileHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, ITile
{
	[SerializeField] 
	private UnityEvent OnActivate;
	[SerializeField] 
	private UnityEvent OnDeactivate;

	public event EventHandler<HexagonTileEventArgs> Entered;
	public event EventHandler<HexagonTileEventArgs> Exited;

	public HexTile HexTile { get; set; }

	public bool Highlight
	{
		set
		{
			if (value)
				OnActivate.Invoke();
			else
				OnDeactivate.Invoke();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GameLoop.Instance.Highlight(this);
	}

	protected virtual void OnEntered(HexagonTileEventArgs eventArgs)
	{
		EventHandler<HexagonTileEventArgs> handler = Entered;
		handler?.Invoke(this, eventArgs);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GameLoop.Instance.UnhighlightAll();
	}

	protected virtual void OnExited(HexagonTileEventArgs eventArgs)
	{
		EventHandler<HexagonTileEventArgs> handler = Exited;
		handler?.Invoke(this, eventArgs);
	}
	public int GetDirectionFromTile(HexTileHandler other)
	{
		return DirectionVectors.Get(HexTile.Subtract(HexTile, other.HexTile).Normalized().ToVector3Int());
	}
	public void OnDrop(PointerEventData eventData)
	{
		GameLoop.Instance.Execute(this);
	}
}

public class HexagonTileEventArgs : EventArgs
{
	public HexTileHandler Tile { get; }

	public HexagonTileEventArgs(HexTileHandler tile)
	{
		Tile = tile;
	}
}
