using UnityEngine;

public class BuildingReqirements : MonoBehaviour
{

    public BuildingStatiscics buildingStatiscics;
    public bool canBeBuild(int gold,int stone,int wood)
    {
        if (gold >= buildingStatiscics.goldReq.Value && stone >= buildingStatiscics.stoneReq.Value && wood >= buildingStatiscics.woodReq.Value)
        {
            return true;
        }
        else
            return false;
    }
}
