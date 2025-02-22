using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public enum ShotSources
{
    Player, Enemy
}
public enum ShotTypes
{
    Target, Direction
}

public class GameLibrary : MonoBehaviour
{
    public List<SplineContainer> randomSplines;
    public Transform playerPos;

    //Singleton for getter
    private static GameLibrary instance;
    public static GameLibrary Instance { get { return instance; } }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }



}
