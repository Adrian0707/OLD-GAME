using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    Dictionary<string, Sprite> tileSprites;
    // Start is called before the first frame update
    private void Awake()
    {
        tileSprites = new Dictionary<string, Sprite>();
    }
    void LoadSprites()
    {
      //  Resources.LoadAll<Sprite>()
    }
   /* public Sprite GetSprite(MyTile tile)
    {

    }*/
}
