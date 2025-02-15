using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth;

    private int _currentHealth;

    public bool godMode;

    public int currentHealth => _currentHealth;

    public PlayerHealthTextController playerHealthTextController;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        _currentHealth = maxHealth;

        playerHealthTextController.UpdateHealthText();
    }

    public void GodModeChange(int value)
    {
        if (value == 0)
            godMode = false;
        else
            godMode = true;
    }

    public void MinusHealth(int damage)
    {
        if (godMode)
            return;

        _currentHealth -= damage;

        playerHealthTextController.UpdateHealthText();

        if(_currentHealth <= 0)
        {

        }
    }

    public void AddHealth(int treatment)
    {
        _currentHealth += treatment;

        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }

        playerHealthTextController.UpdateHealthText();
    }
}
