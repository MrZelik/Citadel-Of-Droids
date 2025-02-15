using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossUIController : MonoBehaviour
{
    [SerializeField] private GameObject bossUIPanel;
    [SerializeField] private TextMeshProUGUI bossHealth;
    [SerializeField] private Image bossHealthImae;

    public static EnemyBossUIController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void StartBoosFightUI(Enemy boss)
    {
        bossUIPanel.SetActive(true);

        StartCoroutine(UiUpdate(boss));
    }

    private IEnumerator UiUpdate(Enemy boss)
    {
        while(boss != null)
        {
            bossHealth.text = boss.currentHealth / boss.health * 100f + "%";
            bossHealthImae.fillAmount = boss.currentHealth / boss.health;

            yield return new WaitForSeconds(0.3f);
        }

        bossUIPanel.SetActive(false);
    }
}
