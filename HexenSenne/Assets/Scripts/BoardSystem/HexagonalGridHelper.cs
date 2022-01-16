using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DAE/Grid Helper")]
public class HexagonalGridHelper : ScriptableObject
{
	public int Radius = 3;
	public HexTileHandler HexTilePf = null;
	public Piece<HexTileHandler> PlayerPf = null;
	public Piece<HexTileHandler> EnemyPf = null;

}
