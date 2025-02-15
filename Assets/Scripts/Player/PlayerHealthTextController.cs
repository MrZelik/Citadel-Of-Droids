using TMPro;
using UnityEngine;

public class PlayerHealthTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    public void UpdateHealthText()
    {
        healthText.text = PlayerHealthController.instance.currentHealth.ToString();
    }
}
