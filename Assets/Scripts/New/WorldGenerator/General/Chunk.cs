using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Sprite full;
    public Sprite empty;
    public int dim=3;
     public int TileSize = 1;
    MyTile[,] tiles;

   
    private void Awake()
    {
        tiles = new MyTile[dim, dim];
        for (int x = 0; x < dim; x++)
        {
            for (int y = 0; y < dim; y++)
            {
                tiles[x, y] = new MyTile(x*TileSize + (int)transform.position.x*TileSize, y*TileSize + (int)transform.position.y*TileSize);
                GameObject tileGo = new GameObject("Tile_" + tiles[x,y].x+ "_" + tiles[x, y].y);
                tileGo.transform.position = new Vector2(tiles[x, y].x, tiles[x, y].y);
                tileGo.transform.SetParent(this.transform,true);

                SpriteRenderer spriteRenderer = tileGo.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = full;

            }
        }
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject full;
    public Sprite empty;
    public static int size = 30;
    public int TileSize = 1;
    GameObject[,] tiles;


    private void Awake()
    {
        tiles = new GameObject[size, size];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                tiles[x, y] = Instantiate(full, new Vector3(x * TileSize + (int)transform.position.x * TileSize, y * TileSize + (int)transform.position.y * TileSize), Quaternion.identity);
                GameObject tileGo = new GameObject("Tile_" + tiles[x, y].transform.position.x + "_" + tiles[x, y].transform.position.y);
                tileGo.transform.position = new Vector2(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y);
                tileGo.transform.SetParent(this.transform, true);

                //SpriteRenderer spriteRenderer = tileGo.AddComponent<SpriteRenderer>();
                //spriteRenderer.sprite = full;

            }
        }
    }
}
*/
