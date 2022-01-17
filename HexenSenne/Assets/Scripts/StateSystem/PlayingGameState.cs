using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingGameState : GameStateBase
{
    //private SelectionManager<HexTile> _selectionManager;
    //private DeckManager<ICard<HexTile>, HexTile> _deckManager;

    public PlayingGameState(StateMachine<GameStateBase> stateMachine) :base(stateMachine)
    {
        //_selectionManager = new SelectionManager<HexTile>();
        //_deckManager = deckManager;
    }

    public override void OnEnter()
    {
        GameLoop.Instance. += OnHexTileSelected;
        _selectionManager.Deselected += OnHexTileDeselected;
    }

    public override void OnExit()
    {
        _selectionManager.Selected -= OnHexTileSelected;
        _selectionManager.Deselected -= OnHexTileDeselected;
    }

    private void DeselectAll() => GameLoop.Instance._selectedCard = null;

    public override void Deselect(ICard<HexTile> card, HexTile hexTile)
    {
        foreach (var validHexTile in card.Positions(hexTile))
            _selectionManager.Deselect(validHexTile);
    }

    public override void Select(ICard<HexTile> card, HexTile hexTile)
    {
        if (card.Type == PlayableCardName.Teleport && card.Positions(hexTile).Contains(hexTile))
        {
            _selectionManager.Select(hexTile);
        }
        else
        {
            foreach (var validHexTile in card.Positions(hexTile))
            {
                _selectionManager.Select(validHexTile);
            }
        }
    }

    public override void Play(ICard<HexTile> eventArgsCard, HexTile eventArgsHexTile)
    {
        if (eventArgsCard.Positions(eventArgsHexTile).Contains(eventArgsHexTile))
        {
            _deckManager.Play(eventArgsCard, eventArgsHexTile);
        }

        DeselectAll();
    }

    public override void Backward() => StateMachine.MoveTo(ReplayingState);
    private void OnHexTileDeselected(object source, SelectionEventArgs<HexTile> eventArgs) => eventArgs.SelectionItem.Highlight = false;
    private void OnHexTileSelected(object source, SelectionEventArgs<HexTile> eventArgs) => eventArgs.SelectionItem.Highlight = true;
}
