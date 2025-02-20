using UnityEngine;
using UnityEngine.Splines;

[CreateAssetMenu(fileName = "Enemy_Data", menuName = "Scriptable Objects/Enemy_Data")]
public class Enemy_Data : ScriptableObject
{
    public GameObject enemyPrefab;
    public GameObject enemyShotTypePrefab;
    public float speed;
}
