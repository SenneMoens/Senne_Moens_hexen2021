using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<HexTile> HexTileList;

    [SerializeField] private GameObject _hexTilePf = null;

    [SerializeField] public int Radius;

    private void Start()
    {
        GenerateGrid(Radius);

        foreach(HexTile hexTile in HexTileList)
        {
            Instantiate(_hexTilePf, hexTile.ToWorldPosition(), Quaternion.identity, transform);
        }
    }

    public void GenerateGrid(int radius)
    {
        HexTileList = new List<HexTile>();

        for (int q = -radius; q <= radius; q++)
        {
            int r1 = Mathf.Max(-radius, -q - radius);
            int r2 = Mathf.Min(radius, -q + radius);

            for (int r = r1; r <= r2; r++)
            {
                HexTileList.Add(new HexTile(q, r, -q - r));
            }
        }
    }
}
