using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponModel
{
    public int damage;
    public float range;
    public float fireRate;
    public float bulletSpeed;
    public BulletController projectilePrefab;
}
