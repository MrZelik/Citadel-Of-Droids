using DG.Tweening;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private Transform[] doors;
    [SerializeField] private Enemy[] enemyesPrefab;
    [SerializeField] private int enemyCount;
    [SerializeField] private float delayPerSpawn;
    [SerializeField] private int maxEnemyOnZone;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private NavMeshSurface navMeshSurface;

    public List<Enemy> enemyes = new List<Enemy>();

    private IEnumerator SpawnEnemyes()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < enemyCount; i++)
        {
            if(enemyes.Count >= maxEnemyOnZone)
            {
                i--;

                for (int j = 0; j < enemyes.Count; j++)
                {
                    if (enemyes[j] == null)
                        enemyes.RemoveAt(j);
                }

                yield return new WaitForSeconds(delayPerSpawn);
            }
            else
            {
                Enemy newEnemy = Instantiate(enemyesPrefab[Random.Range(0, enemyesPrefab.Length)]);
                newEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                enemyes.Add(newEnemy);

                yield return new WaitForSeconds(delayPerSpawn);
            }
        }

        StartCoroutine(WaitForDeathEnemy());
    }

    private IEnumerator WaitForDeathEnemy()
    {
        while(enemyes.Count > 0)
        {
            for (int j = 0; j < enemyes.Count; j++)
            {
                if (enemyes[j] == null)
                    enemyes.RemoveAt(j);

                yield return new WaitForSeconds(1f);
            }
        }

        MoveDoors(true);
    }


    private void MoveDoors(bool open)
    {
        if (open)
        {
            Destroy(navMeshSurface);

            foreach(Transform door in doors)
            {
                door.DOLocalMoveY(door.localPosition.y + 5, 2f).SetEase(Ease.Linear).Play().OnComplete(() => Destroy(this));
            }
        }
        else
        {
            foreach (Transform door in doors)
            {
                door.DOLocalMoveY(door.localPosition.y - 5, 2f).SetEase(Ease.Linear).Play();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent<FirstPersonController>(out FirstPersonController firstPersonController))
        {
            StartCoroutine(SpawnEnemyes());

            MoveDoors(false);

            collider.enabled = false;
        }
    }
}
