using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] WeaponModel weapon;
    [SerializeField] Transform shootPoint;
    [SerializeField] float startShootRange;
    [SerializeField] float stopMoveRange;
    [SerializeField] float velocity;
    [SerializeField] float rotationSpeed;
    float timeLeftToShoot = 0;
    void FixedUpdate()
    {
        timeLeftToShoot -= Time.fixedDeltaTime;
        MoveAndRotate();
    }

    void MoveAndRotate()
    {
        //Move
        Vector3 direction = Player.Instance.transform.position - transform.position;
        if(direction.magnitude < startShootRange)
            Shoot();
        
        if(direction.magnitude > stopMoveRange)
        {
            

            direction = direction.normalized;
            transform.position += velocity * direction * Time.fixedDeltaTime;
        }

        //Rotate
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
    }
    
    void Shoot()
    {
        Vector3 direction = Player.Instance.transform.position - shootPoint.transform.position;
        if(timeLeftToShoot>0)
            return;

        timeLeftToShoot = 1/weapon.fireRate;
        Instantiate(
            weapon.projectilePrefab, 
            shootPoint.position,
            Quaternion.LookRotation(direction, Vector3.up)
            ).Setup(weapon, true);

    }
}
