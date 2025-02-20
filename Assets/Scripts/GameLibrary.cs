using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class GameLibrary : MonoBehaviour
{
    public List<SplineContainer> randomSplines;

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
