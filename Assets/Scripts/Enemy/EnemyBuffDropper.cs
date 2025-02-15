using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyBuffDropper : MonoBehaviour
{
    [SerializeField] private int buffCount;
    [SerializeField] private Buff buffPrefab;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float spawnCooldown;

    private void Start()
    {
        StartCoroutine(BuffSpawner());
    }

    private IEnumerator BuffSpawner()
    {
        for (int i = 0; i < buffCount; i++)
        {
            Buff buff = Instantiate(buffPrefab);
            buff.transform.position = spawnPos.position;
            yield return BuffDrop(buff.transform);
        }

        Destroy(gameObject);
    }

    private YieldInstruction BuffDrop(Transform buff)
    {
        return buff
            .DOLocalJump(buff.position + new Vector3(Random.Range(-2f,2f), Random.Range(0f, 1f), Random.Range(-2f, 2f)), 0.5f, 1, spawnCooldown)
            .SetEase(Ease.Linear)
            .Play()
            .WaitForCompletion();
    }
}
