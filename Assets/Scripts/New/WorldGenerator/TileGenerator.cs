using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class TileGround
{
   public Tile[] tile;
   public int up;
   public int down;
   public int left;
   public int right;
    
}
public class TileGenerator : MonoBehaviour
{
   // public bool tileCreated = false;
    public Signal2 updatePath;
   /* [Range(0, 100)]
    public int iniChance;
    [Range(1, 8)]
    public int birthLimit;
    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;
    [Range(1, 100)]
    public int groundBeutChance;
    [Range(1, 100)]
    public int watherBeutChance;*/

    public WorldGenStatistics genStatistics;
    //private int count = 0;

    private int[,] terrainMap;
    public Vector3Int tmpSize;
    public Tilemap topMap;
    public Tilemap topMapBeautify;
    public Tilemap botMap;
    public Tilemap botMapBeautify;
    public Tile topTile;
    public TileGround[] topTiles;
    public Tile botTile;
    public Tile[] topTileBeautify;
    public Tile[] botTileBeautify;

    int width;
    int height;
    public Tile FindTile(int up, int down,int left,int right)
    {
        foreach (var item in topTiles)
        {
            if (up == item.up && left == item.left && down == item.down && right == item.right)
            {
                if (item.tile.Length > 0)
                {
                    return item.tile[Random.Range(0, item.tile.Length)];
                }
                else
                {                   
                    return item.tile[0];
                }
            }
        }
        return topTile;
    }
    public void doSim(int nu)
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }


        for (int i = 0; i < nu; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                {
                    if (x + 1 < width && x - 1 >= 0)
                    {
                        if (terrainMap[x + 1, y] == 0 && terrainMap[x - 1, y] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                    if (y + 1 < height && y - 1 >= 0)
                    {
                        if (terrainMap[x, y + 1] == 0 && terrainMap[x, y - 1] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                }
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                {
                    if (x + 1 < width && x - 1 >= 0)
                    {
                        if (terrainMap[x + 1, y] == 0 && terrainMap[x - 1, y] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                    if (y + 1 < height && y - 1 >= 0)
                    {
                        if (terrainMap[x, y + 1] == 0 && terrainMap[x, y - 1] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                }
                if (terrainMap[x, y] == 1)
                {
                    if (x + 1 < width && x - 1 >= 0 && y + 1 < height && y - 1 >= 0)
                    {
                        topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), FindTile(terrainMap[x, y - 1], terrainMap[x, y + 1], terrainMap[x + 1, y], terrainMap[x - 1, y]));
                        if (terrainMap[x, y - 1] == 1 && terrainMap[x, y + 1] == 1 && terrainMap[x + 1, y] == 1 && terrainMap[x - 1, y] == 1)
                        {
                            if (Random.Range(0, 100) <= genStatistics.groundBeautyChance)
                            {
                                topMapBeautify.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTileBeautify[Random.Range(0, topTileBeautify.Length)]);
                            }
                        }
                    }
                    else
                    {
                        topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), FindTile(1, 1, 1, 1));
                        if (Random.Range(0, 100) <= genStatistics.groundBeautyChance)
                        {
                            topMapBeautify.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTileBeautify[Random.Range(0, topTileBeautify.Length)]);
                        }
                    }
                }
                else
                {
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                    if (Random.Range(0, 100) <= genStatistics.watherBeautyChance)
                    {
                        botMapBeautify.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTileBeautify[Random.Range(0, botTileBeautify.Length)]);
                    }
                }

            }
        }

        this.GetComponent<ObjectAutomata>().enabled = true;
        updatePath.Raise();
        this.GetComponent<TileGenerator>().enabled = false;
    }


    IEnumerator doSimCorutine(int nu)
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }


        for (int i = 0; i < nu; i++)
        {
            terrainMap = genTilePos(terrainMap);
            yield return null;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                {
                    //print("..");
                   // yield return new WaitForSeconds(0.00000001f);
                    if (x + 1 < width && x - 1 >= 0)
                    {
                        if (terrainMap[x + 1, y] == 0 && terrainMap[x - 1, y] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                    if (y + 1 < height && y - 1 >= 0)
                    {
                        if (terrainMap[x, y + 1] == 0 && terrainMap[x, y - 1] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                }
            }
        }
        for (int x = 0; x < width; x++)
        {
           // yield return new WaitForEndOfFrame();
            for (int y = 0; y < height; y++)
            {
               // print(".");
               // yield return new WaitForEndOfFrame();
                if (terrainMap[x, y] == 1)
                {
                    if (x + 1 < width && x - 1 >= 0)
                    {
                        if (terrainMap[x + 1, y] == 0 && terrainMap[x - 1, y] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                    if (y + 1 < height && y - 1 >= 0)
                    {
                        if (terrainMap[x, y + 1] == 0 && terrainMap[x, y - 1] == 0)
                        {
                            terrainMap[x, y] = 0;
                        }
                    }
                }
                if (terrainMap[x, y] == 1)
                {
                    if (x + 1 < width && x - 1 >= 0 && y + 1 < height && y - 1 >= 0)
                    {
                        topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), FindTile(terrainMap[x, y - 1], terrainMap[x, y + 1], terrainMap[x + 1, y], terrainMap[x - 1, y]));
                        if (terrainMap[x, y - 1] == 1 && terrainMap[x, y + 1] == 1 && terrainMap[x + 1, y] == 1 && terrainMap[x - 1, y] == 1)
                        {
                            if (Random.Range(0, 100) <= genStatistics.groundBeautyChance)
                            {
                                topMapBeautify.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTileBeautify[Random.Range(0, topTileBeautify.Length)]);
                            }
                        }
                    }
                    else
                    {
                        topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), FindTile(1, 1, 1, 1));
                        if (Random.Range(0, 100) <= genStatistics.groundBeautyChance)
                        {
                            topMapBeautify.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTileBeautify[Random.Range(0, topTileBeautify.Length)]);
                        }
                    }
                }
                else
                {
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                    if (Random.Range(0, 100) <= genStatistics.watherBeautyChance)
                    {
                        botMapBeautify.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTileBeautify[Random.Range(0, botTileBeautify.Length)]);
                    }
                }
            }
        }

        this.GetComponent<ObjectAutomata>().enabled = true;
        updatePath.Raise();
        this.GetComponent<TileGenerator>().enabled = false;


    }
    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < genStatistics.groundIniChance ? 1 : 0;
            }

        }

    }


    public int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighb < genStatistics.groundDeathLimit) newMap[x, y] = 0;

                    else
                    {
                        newMap[x, y] = 1;

                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > genStatistics.groundBirthLimit) newMap[x, y] = 1;

                    else
                    {
                        newMap[x, y] = 0;
                    }
                }

            }

        }



        return newMap;
    }
    private void Start()
    {
        StartCoroutine(doSimCorutine(20));
       // doSim(30);
        
       /* this.GetComponent<ObjectAutomata>().enabled = true;
        updatePath.Raise();
        this.GetComponent<test>().enabled = false;*/

        //this.transform.position = new Vector3(this.transform.position.x + 10, this.transform.position.y, this.transform.position.z);
        
    }

   /* void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            doSim(numR);
        }


        if (Input.GetMouseButtonDown(1))
        {
            clearMap(true);
        }




    }*/

    public void Refresh()
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        topMapBeautify.ClearAllTiles();
        botMapBeautify.ClearAllTiles();
        this.GetComponent<ObjectAutomata>().Clear();
        terrainMap = null;
        StartCoroutine(doSimCorutine(20));
    }


    public void clearMap(bool complete)
    {

        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        topMapBeautify.ClearAllTiles();
        botMapBeautify.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }


    }



}