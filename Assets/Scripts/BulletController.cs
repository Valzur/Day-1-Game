using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    WeaponModel weapon;
    bool isEnemy;
    float distanceTravelled = 0;
    void FixedUpdate()
    {
        //Move
        Vector3 dist = transform.forward * weapon.bulletSpeed;
        distanceTravelled += dist.magnitude;
        transform.position += dist * Time.fixedDeltaTime;
        if(distanceTravelled >= weapon.range)
            Destroy(gameObject);
    }

    public void Setup(WeaponModel weaponModel, bool isEnemy = false)
    {
        weapon = weaponModel;
        this.isEnemy = isEnemy;
    }
    
    void OnTriggerEnter(Collider other) 
    {
        
        if(!isEnemy && other.GetComponent<Enemy>())
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.GetDamage(weapon.damage);
            Destroy(gameObject);
        }

        else if(isEnemy && other.GetComponent<Player>())
        {
            Player player = other.GetComponent<Player>();
            player.GetDamage(weapon.damage);
            Destroy(gameObject);
        }
    }
}
