using UnityEngine;

public class LogRange : Log
{
    public GenericHealth genericHealth;
    [Header("Chase type")]
    public LogChase logChase;
    public LogChaseOptimalization logChaseOptimalization;
    public DamageLog damageLog;
    public override void Start()
    {
        base.Start();
        damageLog.statistics = this.enemyStatistics;
        if(logChase!=null)
        logChase.enemyStatistics = this.enemyStatistics;
        if (logChaseOptimalization != null)
            logChaseOptimalization.enemyStatistics = this.enemyStatistics;
        genericHealth.statistics = this.enemyStatistics;
    }
  
}
