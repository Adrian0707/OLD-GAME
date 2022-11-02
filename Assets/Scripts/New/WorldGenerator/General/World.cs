using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
   // public AstarPath path;
    //private Pathfinding.GridGraph gGraph;
    public Transform playerPosition;
    public Dictionary<Vector2, Chunk> chunkMap;
    public GameObject chunkGO;
    public int chunkSize;
    public int chunkDimX;
    public int chunkDimY;
    public float renderDistance;
  //  private int worldx;
   // private int worldy;
 
    private void Awake()
    {
        chunkMap = new Dictionary<Vector2, Chunk>();
       // worldx = 0;
      //  worldy = 0;
        
    }
    void Start()
    {
        //gGraph = AstarPath.active.astarData.gridGraph;
      
    }

   
    void Update()
    {
        FindChunksToLoad();
       
    }
    void FindChunksToLoad()
    {
        int xPos = (int)playerPosition.position.x/ chunkSize;
        int yPos = (int)playerPosition.position.y/ chunkSize;
        for (int x = xPos - chunkDimX; x < xPos + 2* chunkDimX; x += chunkDimX)
        {
            for (int y = yPos - chunkDimY; y < yPos + 2*chunkDimY; y += chunkDimY)
            {
                MakeChunkAt(x, y,xPos,yPos);
            }

        }
    }
        void MakeChunkAt(int x, int y, int xPos,int yPos)
        {
            x = Mathf.FloorToInt(x / chunkDimX) * chunkDimX;
            y = Mathf.FloorToInt(y / chunkDimY) * chunkDimY;
            if (!chunkMap.ContainsKey(new Vector2(x, y)))
            {
            ////Zwiększyać maksymalny rozmiar świata żeby zwiększać graf 
            if (y > yPos)
            {
               // worldy += 2*chunkDimY * chunkSize;
               
                GameObject gameObject = Instantiate(chunkGO, new Vector3(x, y, 0), Quaternion.identity);
                gameObject.transform.parent = this.gameObject.transform;
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(chunkDimX * chunkSize, chunkDimY * chunkSize);
                // gameObject.GetComponent<BoxCollider2D>().size=new Vector2(x, y);
                //   Debug.LogError(worldy);

            }
            else
            {
               // worldx += 2*chunkDimX * chunkSize;
              //  Debug.LogError(worldx);
                
                GameObject gameObject = Instantiate(chunkGO, new Vector3(x, y, -1), Quaternion.identity);
                gameObject.transform.parent = this.gameObject.transform;
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(chunkDimX * chunkSize, chunkDimY * chunkSize);
            }
            //  gGraph.unclampedSize = new Vector2(worldx, worldy);
         //  gameObject.transform.parent = this.gameObject.transform;
           // gameObject.GetComponent<BoxCollider2D>().size = new Vector2(chunkDimX * chunkSize, chunkDimY*chunkSize);
            chunkMap.Add(new Vector2(x, y), gameObject.GetComponent<Chunk>());
            }
        }
        void DeleteChunks()
        {
            List < Chunk > deleteChunks = new List<Chunk>(chunkMap.Values);
            Queue<Chunk> deleteQueue = new Queue<Chunk>();
             for (int i = 0; i < deleteChunks.Count; i++)
        {
            float distance = Vector3.Distance(this.transform.position/ chunkSize, deleteChunks[i].transform.position);
            if(distance> renderDistance * chunkSize)
            {
                deleteQueue.Enqueue(deleteChunks[i]);
            }
        }
        while (deleteQueue.Count > 0)
        {
            Chunk delChunk = deleteQueue.Dequeue();
            chunkMap.Remove(delChunk.transform.position);
            Destroy(delChunk.gameObject);
        }
        }
    }

