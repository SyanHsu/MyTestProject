using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    public Tile tile;
    private Tilemap tilemap;
    private Camera cam;
    private Vector3Int deltaCell;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        cam = Camera.main;
        GetDeltaCell();
    }

    private void Update()
    {
        SetTiles();
    }

    private void GetDeltaCell()
    {
        Vector3 leftDownPos = cam.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightUpPos = cam.ViewportToWorldPoint(Vector2.one);
        Vector3Int leftDownCell = tilemap.WorldToCell(leftDownPos);
        Vector3Int rightUpCell = tilemap.WorldToCell(rightUpPos);
        deltaCell = rightUpCell - leftDownCell;
    }

    private void SetTiles()
    {
        Vector3 leftDownPos = cam.ViewportToWorldPoint(Vector2.zero);
        Vector3Int leftDownCell = tilemap.WorldToCell(leftDownPos);
        Vector3Int rightUpCell = leftDownCell + deltaCell;
        Vector3Int cell = new Vector3Int(leftDownCell.x - 1, leftDownCell.y - 1, 0);

        //if (HasTiled(leftDownCell, rightUpCell, ref cell)) return;
        //for (cell.y = leftDownCell.y - 1; cell.y <= rightUpCell.y + 1; cell.y++)
        //{
        //    for (cell.x = leftDownCell.x - 1; cell.x <= rightUpCell.x + 1; cell.x++)
        //    {
        //        if (!tilemap.HasTile(cell))
        //        {
        //            tilemap.SetTile(cell, tile);
        //        }
        //    }
        //}

        for (cell.y = leftDownCell.y - 1; cell.y <= rightUpCell.y + 1; cell.y++)
        {
            for (cell.x = leftDownCell.x - 1; cell.x <= rightUpCell.x + 1; cell.x++)
            {
                if (!tilemap.HasTile(cell))
                {
                    tilemap.SetTile(cell, tile);
                }
            }
        }
    }

    private bool HasTiled(Vector3Int leftDownCell, Vector3Int rightUpCell, ref Vector3Int cell)
    {
        for (; cell.x <= rightUpCell.x + 1; cell.x++)
        {
            if (!tilemap.HasTile(cell)) return false;
        }
        cell.y = rightUpCell.y + 1;
        for (cell.x = leftDownCell.x - 1; cell.x <= rightUpCell.x + 1; cell.x++)
        {
            if (!tilemap.HasTile(cell)) return false;
        }
        cell.x = leftDownCell.x - 1;
        for (cell.y = leftDownCell.y; cell.y <= rightUpCell.y; cell.y++)
        {
            if (!tilemap.HasTile(cell)) return false;
        }
        cell.x = rightUpCell.x + 1;
        for (cell.y = leftDownCell.y; cell.y <= rightUpCell.y; cell.y++)
        {
            if (!tilemap.HasTile(cell)) return false;
        }
        return true;
    }
}
