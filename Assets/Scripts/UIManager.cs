using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] TMP_Text levelText; 
    [SerializeField] TMP_Text healthText;
    [SerializeField] Image healthIndicator;
    [SerializeField] GameObject losePanel;
    [SerializeField] TMP_Text loseText;
    [SerializeField] GameObject winPanel;

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

    public void Win()=> winPanel.SetActive(true);

    public void Lose(int levelsNo)
    {
        losePanel.SetActive(true);
        loseText.text = "You finished " + levelsNo.ToString() + " levels.., Wanna try again?";
    }
}
