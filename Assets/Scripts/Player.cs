using UnityEngine;

[RequireComponent(typeof(PlayerController))]

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] int initialHealth;
    int currentHealth;
    PlayerController playerController;

    void Awake()
    {
        Instance = this;
        currentHealth = initialHealth;
        playerController = GetComponent<PlayerController>();
    }
    void Start() => UIManager.Instance.UpdateHealth(currentHealth, initialHealth);

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            playerController.Die();
            GameManager.Instance.Lose();
        }
        UIManager.Instance.UpdateHealth(currentHealth, initialHealth);
    }
    
    public void Heal()
    { 
        currentHealth = initialHealth;
        UIManager.Instance.UpdateHealth(currentHealth, initialHealth);
    }

}