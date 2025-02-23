using UnityEngine;
using UnityEngine.Splines;

public class Enemy_Factory
{
    public GameObject CreateEnemy(Enemy_Data enemyData, SplineContainer spline)
    {
        Enemy_Builder builder = new Enemy_Builder()
            .SetBaseStats(enemyData)
            .SetBaseSpline(spline);
        return builder.Build();
    }

    public GameObject CreateBoss(Enemy_Data enemyData, SplineContainer spline)
    {
        Enemy_Builder builder = new Enemy_Builder()
            .SetBaseStats(enemyData)
            .SetBaseSpline(spline);
        return builder.BuildBoss();
    }
}
