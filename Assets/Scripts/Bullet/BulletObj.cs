using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create BulletAsset")]
public class BulletObj : ScriptableObject
{
    [Header("Initial Bullet")]
    public float linearSpeed;
    public float angularSpeed;
    public float linearAccelerate;
    public float angularAccelerate;

    public float mx = int.MaxValue;
    public float lifeCycle;

    [Header("Shooter init config")]
    public float initRotation = 0f;
    public float angularVelocity = 0f;
    public float angularVMax = int.MaxValue;

    public int count = 0;
    public float lineAngle = 0f;
    public float CoolDown = 0;

    [Header("Prefab")]
    public GameObject prefab;
}
