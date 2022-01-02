using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TMP_Text levelText; 
    [SerializeField] TMP_Text healthText;
    [SerializeField] Image healthIndicator;
    void Awake() => Instance = this;

    public void UpdateLevel(int level)
    {
        levelText.text = "Current Level: " + level.ToString();
    }

    public void UpdateHealth(int currentHealth, int totalHealth)
    {
        healthIndicator.fillAmount = (float)currentHealth / (float)totalHealth;
        healthText.text = currentHealth + " / " + totalHealth;
    }
}
