using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy_Builder
{
    Enemy_Data enemyData;
    SplineContainer spline;

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

    public GameObject Build()
    {
        GameObject instance = GameObject.Instantiate(enemyData.enemyPrefab);

        SplineAnimate anim = instance.GetOrAddComponent<SplineAnimate>();
        anim.Container = spline;
        anim.AnimationMethod = SplineAnimate.Method.Speed;
        anim.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis;
        anim.ObjectForwardAxis = SplineAnimate.AlignAxis.XAxis;
        anim.MaxSpeed = enemyData.speed;

        // set starting spline position
        instance.transform.position = spline.EvaluatePosition(0f);
        anim.Play();

        // TODO add shot type

        return instance;
    }
}
