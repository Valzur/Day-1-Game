using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int initialHealth;
    [SerializeField] Image healthIndicator;
    int currentHealth;

    // Start is called before the first frame update
    void Start() => currentHealth = initialHealth;

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthIndicator.fillAmount = (float)currentHealth / initialHealth;
        if(currentHealth <= 0)
            Destroy(gameObject);
    }
}
