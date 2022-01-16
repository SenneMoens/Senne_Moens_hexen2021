using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard<TPiece, TTile>
{

	List<TTile> Positions(TPiece piece, TTile tile);
	void Initialize(Board<TPiece, TTile> board, Grid<TTile> grid);
	bool Execute(TPiece piece, TTile tile);

}
