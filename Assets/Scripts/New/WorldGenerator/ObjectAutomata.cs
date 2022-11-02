using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ObjectAutomata : MonoBehaviour
{
    public Signal2 updatePath;
    
    [Range(0, 100)]
    public int iniChanceTree;
    [Range(1, 8)]
    public int birthLimitTree;
    [Range(1, 8)]
    public int deathLimitTree;
    [Range(1, 10)]
    public int numRTree;
    private int[,] terrainMap2;
    public GameObject[] tree;
    public GameObject Trees;

    [Range(0, 100)]
    public int iniChanceStone;
    [Range(1, 8)]
    public int birthLimitStone;
    [Range(1, 8)]
    public int deathLimitStone;
    [Range(1, 10)]
    public int numRStone;
    private int[,] terrainMap1;
    public GameObject stone;
    public GameObject Stones;

    [Range(0, 100)]
    public int iniChanceEnemy;
    [Range(1, 8)]
    public int birthLimitEnemy;
    [Range(1, 8)]
    public int deathLimitEnemy;
    [Range(1, 10)]
    public int numREnemy;
    private int[,] terrainMap3;
    public List<GameObject> enemies;
    public GameObject Enemies;

    public Vector3Int tmpSize;
   
    
    public Tilemap topMap;
    public Tilemap botMap;
    public Tile topTile;
    public Tile botTile;


    private GameObject obj;
    private GameObject objs;
    private TileGenerator tileAutomat;

    int width;
    int height;
    public Signal2 endOfLoadingSignal;
    public void doSim(int nu,ref int[,] terrainMap,int iniChance,int deathLimit,int birthLimit)
    {
        ClearMap(false,  terrainMap);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos(terrainMap,iniChance);
        }


        for (int i = 0; i < nu; i++)
        {
            terrainMap = genTilePos(terrainMap,deathLimit,birthLimit);
        }
        //tuuu
    }
    void Build() {
        int[,] terrainMap = terrainMap1;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap2[x, y] != 0)
                {
                    terrainMap[x, y] = 2;
                }
                if(terrainMap3[x,y] !=0)
                {
                    terrainMap[x, y] = 3;
                }

                if (terrainMap[x, y] == 1 && topMap.GetTile(new Vector3Int(-x - 1 + width / 2, -y + height / 2, 0)) !=null)
                {
                    GameObject t1 = Instantiate(stone, new Vector3(-x + width / 2 + transform.position.x, -y + height / 2 + transform.position.y, 0), Quaternion.identity);
                    t1.transform.parent = Stones.transform;
                    t1.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)t1.transform.position.y;
                    // topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);

                }
                else if (terrainMap[x, y] == 2 && topMap.GetTile(new Vector3Int(-x - 1 + width / 2, -y + height / 2, 0)) != null)
                {
                    GameObject t1 = Instantiate(tree[Random.Range(0, tree.Length)], new Vector3(-x-0.6f + width / 2 + transform.position.x, -y + height / 2 + transform.position.y, 0), Quaternion.identity);
                    t1.transform.parent = Trees.transform;
                    t1.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)t1.transform.position.y;
                    // topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                }
                else if (terrainMap[x, y] == 3 && topMap.GetTile(new Vector3Int(-x - 1 + width / 2, -y + height / 2, 0)) != null)
                {
                    GameObject t1 = Instantiate(enemies[Random.Range(0,enemies.Count)], new Vector3(-x - 0.6f + width / 2 + transform.position.x, -y+1 + height / 2 + transform.position.y, 0), Quaternion.identity);
                    t1.transform.parent = Enemies.transform;
                    t1.SetActive(true);
                    t1.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)t1.transform.position.y;
                    // topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                }
            }
        }

        updatePath.Raise();
        this.GetComponent<ObjectAutomata>().enabled = false;

    }

    IEnumerator BuildCo()
    {
        int[,] terrainMap = terrainMap1;
        for (int x = 0; x < width; x++)
        {
            if(x%3==0)
            yield return new WaitForEndOfFrame();
            for (int y = 0; y < height; y++)
            {
                if (terrainMap2[x, y] != 0)
                {
                    terrainMap[x, y] = 2;
                }
                if (terrainMap3[x, y] != 0)
                {
                    terrainMap[x, y] = 3;
                }

                if (terrainMap[x, y] == 1 && topMap.GetTile(new Vector3Int(-x - 1 + width / 2, -y + height / 2, 0)) != null)
                {
                    GameObject t1 = Instantiate(stone, new Vector3(-x + width / 2 + transform.position.x, -y + height / 2 + transform.position.y, 0), Quaternion.identity);
                    t1.transform.parent = Stones.transform;
                    t1.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)t1.transform.position.y;
                    // topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);

                }
                else if (terrainMap[x, y] == 2 && topMap.GetTile(new Vector3Int(-x - 1 + width / 2, -y + height / 2, 0)) != null)
                {
                    GameObject t1 = Instantiate(tree[Random.Range(0, tree.Length)], new Vector3(-x - 0.6f + width / 2 + transform.position.x, -y + height / 2 + transform.position.y, 0), Quaternion.identity);
                    t1.transform.parent = Trees.transform;
                    t1.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)t1.transform.position.y+2;
                    // topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                }
                else if (terrainMap[x, y] == 3 && topMap.GetTile(new Vector3Int(-x - 1 + width / 2, -y + height / 2, 0)) != null)
                {
                    GameObject t1 = Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(-x - 0.6f + width / 2 + transform.position.x, -y + 1 + height / 2 + transform.position.y, 0), Quaternion.identity);
                    t1.transform.parent = Enemies.transform;
                    t1.SetActive(true);
                    t1.GetComponentInChildren<SpriteRenderer>().sortingOrder = -(int)t1.transform.position.y+3;
                    // topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                }
            }
            

        }

        updatePath.Raise();
        this.GetComponent<ObjectAutomata>().enabled = false;
       // yield return new WaitForSeconds(3);
       
       if(GameObject.FindGameObjectWithTag("Fireplace")!=null&&!GameObject.FindGameObjectWithTag("Fireplace").GetComponent<Fireplace>().isActiveAndEnabled)
        endOfLoadingSignal.Raise();
    }

    public void initPos(int[,] terrainMap, int iniChance)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }

        }

    }


    public int[,] genTilePos(int[,] oldMap, int deathLimit,int birthLimit)
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
                    if (neighb < deathLimit) newMap[x, y] = 0;

                    else
                    {
                        newMap[x, y] = 1;

                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;

                    else
                    {
                        newMap[x, y] = 0;
                    }
                }

            }

        }



        return newMap;
    }
  /*  private void OnEnable()
    {
        enemies.AddRange(GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemiesInGame);

        doSim(numRStone, ref terrainMap1, iniChanceStone, deathLimitStone, birthLimitStone);


        doSim(numRTree, ref terrainMap2, iniChanceTree, deathLimitTree, birthLimitStone);

        doSim(numREnemy, ref terrainMap3, iniChanceEnemy, deathLimitEnemy, birthLimitEnemy);

        // Build();
        StartCoroutine(BuildCo());
    }*/
    private void Start()
    {

        enemies.AddRange(GameObject.FindGameObjectWithTag("EnemiesSystem").GetComponent<EnemiesSystem>().playerNormalEnemiesInGame);

        doSim(numRStone, ref terrainMap1, iniChanceStone,deathLimitStone,birthLimitStone);
       

        doSim(numRTree, ref terrainMap2, iniChanceTree,deathLimitTree,birthLimitStone);

        doSim(numREnemy, ref terrainMap3,iniChanceEnemy, deathLimitEnemy, birthLimitEnemy);

       // Build();
        StartCoroutine(BuildCo());
        
    }

    public void ClearMap(bool complete, int[,] terrainMap)
    {
        { 
        

        if (complete)
       
            terrainMap = null;
        }


    }
    public void Clear()
    {
        foreach (Transform item in Enemies.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in Stones.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in Trees.transform)
        {
            Destroy(item.gameObject);
        }
        this.enabled = false;
    }



}