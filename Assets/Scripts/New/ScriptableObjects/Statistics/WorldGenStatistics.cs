using UnityEngine;
[CreateAssetMenu(menuName = "Stats/World")]
public class WorldGenStatistics : ScriptableObject
{
    [Header("Ground")]
    [Range(0, 100)]
    public int groundIniChance;
    [Range(1, 8)]
    public int groundBirthLimit;
    [Range(1, 8)]
    public int groundDeathLimit;
    [Range(1, 10)]
    public int groundNumR;
    [Range(1, 100)]
    public int groundBeautyChance;
    [Range(1, 100)]
    public int watherBeautyChance;
    [Header("Tree")]
    [Range(1, 100)]
    public int iniChanceTree;
    [Range(1, 8)]
    public int birthLimitTree;
    [Range(1, 8)]
    public int deathLimitTree;
    [Range(1, 10)]
    public int numRTree;
    [Header("Stone")]
    [Range(0, 100)]
    public int iniChanceStone;
    [Range(1, 8)]
    public int birthLimitStone;
    [Range(1, 8)]
    public int deathLimitStone;
    [Range(1, 10)]
    public int numRStone;
    [Header("Enemy")]
    [Range(0, 100)]
    public int iniChanceEnemy;
    [Range(1, 8)]
    public int birthLimitEnemy;
    [Range(1, 8)]
    public int deathLimitEnemy;
    [Range(1, 10)]
    public int numREnemy;
}
