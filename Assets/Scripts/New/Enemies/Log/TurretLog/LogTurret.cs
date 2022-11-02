using UnityEngine;

public class LogTurret : Log
{
    public GenericHealth genericHealth;
    public DamageLog damageLog;
    [Header("One of")]
    public LogChase logChase;
    public LogChaseOptimalization logChaseOptimalization;
    [Header("One of")]
    public Shoot shoot;
    public ShootOptimalization shootOptimalization;
    public override void Start()
    {
        base.Start();
        genericHealth.statistics = this.enemyStatistics;
        damageLog.statistics = this.enemyStatistics;
        if (logChaseOptimalization != null)
        {
            logChaseOptimalization.enemyStatistics = this.enemyStatistics;
        }
        else
        {
            logChase.enemyStatistics = this.enemyStatistics;
        }
        if (shootOptimalization != null)
        {
            logChaseOptimalization.enemyStatistics = this.enemyStatistics;
        }
        else
        {
            shoot.enemyStatistics = this.enemyStatistics;
        }
    }
}
