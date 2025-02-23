using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy_Builder
{
    Enemy_Data enemyData;
    SplineContainer spline;
    GameObject shotType;

    public Enemy_Builder SetBaseStats(Enemy_Data enemyData)
    {
        this.enemyData = enemyData;
        return this;
    }

    public Enemy_Builder SetBaseSpline(SplineContainer spline)
    {
        this.spline = spline;
        return this;
    }

    public Enemy_Builder SetShotType(GameObject shotType)
    {
        this.shotType = shotType;
        return this;
    }

    public GameObject Build()
    {
        GameObject instance = GameObject.Instantiate(enemyData.enemyPrefab);

        SplineAnimate anim = instance.GetOrAddComponent<SplineAnimate>();
        anim.Container = spline;
        anim.AnimationMethod = SplineAnimate.Method.Speed;
        anim.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis;
        anim.ObjectForwardAxis = SplineAnimate.AlignAxis.XAxis;
        anim.MaxSpeed = enemyData.speed;
        instance.GetOrAddComponent<Enemy>().SetShotType(enemyData.enemyShotTypePrefab);


        // set starting spline position
        instance.transform.position = spline.EvaluatePosition(0f);
        anim.Play();

        return instance;
    }

    public GameObject BuildBoss()
    {
        GameObject instance = GameObject.Instantiate(enemyData.enemyPrefab);

        SplineAnimate anim = instance.GetOrAddComponent<SplineAnimate>();
        anim.Container = spline;
        anim.AnimationMethod = SplineAnimate.Method.Speed;
        anim.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis;
        anim.ObjectForwardAxis = SplineAnimate.AlignAxis.XAxis;
        anim.MaxSpeed = enemyData.speed;
        instance.GetOrAddComponent<Boss>().SetShotType(enemyData.enemyShotTypePrefab);


        // set starting spline position
        instance.transform.position = spline.EvaluatePosition(0f);
        anim.Play();

        return instance;
    }
}
