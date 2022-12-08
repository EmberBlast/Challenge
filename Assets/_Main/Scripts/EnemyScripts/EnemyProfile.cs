using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/New Enemy Profile")]
public class EnemyProfile : ScriptableObject
{
    [SerializeField] private string enemyProfileName;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemyRange;

    public string EnemyProfileName { get => enemyProfileName; private set => enemyProfileName = value; }
    public float EnemySpeed { get => enemySpeed; private set => enemySpeed = value; }
    public float EnemyRange { get => enemyRange; private set => enemyRange = value; }
}
