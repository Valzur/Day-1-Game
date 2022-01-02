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
    
    void OnCollisionEnter(Collision other) 
    {
        
        if(!isEnemy && other.collider.GetComponent<Enemy>())
        {
            Enemy enemy = other.collider.GetComponent<Enemy>();
            enemy.GetDamage(weapon.damage);
            Destroy(gameObject);
        }

        else if(isEnemy && other.collider.GetComponent<Player>())
        {
            Player player = other.collider.GetComponent<Player>();
            player.GetDamage(weapon.damage);
            Destroy(gameObject);
        }
    }
}
