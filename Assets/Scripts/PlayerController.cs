using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] WeaponModel weapon;
    [SerializeField] Transform shootPoint;
    [SerializeField] float velocity;
    [Header("Camera Properties")]
    [SerializeField] Camera camera;
    [SerializeField] float yOffset;
    [SerializeField] float cameraVelocity;
    bool isAlive = true;
    float timeLeftToShoot;
    void FixedUpdate()
    {
        if(!isAlive)
            return;
        Move();
        Rotate();
        CamFollow();
    }
    void Update() => Shoot();

    void Shoot()
    {
        timeLeftToShoot -= Time.deltaTime;
        if(Input.GetMouseButton(0) && timeLeftToShoot <= 0)
        {
            //Pew pew.. 
            Instantiate(
                weapon.projectilePrefab, 
                shootPoint.position,
                shootPoint.rotation 
                ).Setup(weapon);
            
            timeLeftToShoot = 1/ weapon.fireRate;
        }
    }

    void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 dist = new Vector3(horizontalInput, 0, verticalInput);
        dist *= velocity;
        transform.position += dist * Time.fixedDeltaTime;
    }

    void Rotate()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        transform.LookAt(mousePos, Vector3.up);
    }

    void CamFollow()
    {
        Vector3 newPos = new Vector3( transform.position.x, yOffset, transform.position.z);
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, newPos, cameraVelocity * Time.fixedDeltaTime);
    }
    
    public void Die() => isAlive = false;
}