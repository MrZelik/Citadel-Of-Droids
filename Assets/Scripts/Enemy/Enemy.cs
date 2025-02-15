using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Collections;
using StarterAssets;
using NUnit.Framework.Internal;

public class Enemy : MonoBehaviour, IHittable
{
    [SerializeField] private float _health;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem destroyParticle;
    [SerializeField] private Transform destroyParticlePos;
    [SerializeField] private EnemyBuffDropper buffDropperPrefab;

    private float _currentHealth;
    
    public string attackAnimName;

    public Transform player;

    public NavMeshAgent agent;

    public Animator animator;

    public float health => _health;   
    public float currentHealth => _currentHealth;

    private void Start()
    {
        player = FindObjectOfType<CameraShaking>().gameObject.transform;
        agent.enabled = false;
        agent.enabled = true;

        _currentHealth = _health;

        StartCoroutine(CheckPlayer());
    }

    private IEnumerator CheckPlayer()
    {
        agent.destination = player.position;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CheckPlayer());
    }

    public void ApplyHit(Vector3 playerPosition, Vector3 hitPosition)
    {
        _currentHealth--;

        transform.DOPunchPosition(-transform.forward * 0.3f, 0.3f).Play();

        ParticleSystem newHitParticle = Instantiate(hitParticle);
        newHitParticle.transform.position = hitPosition;
        newHitParticle.transform.LookAt(playerPosition);
        newHitParticle.Play();

        if (_currentHealth <= 0)
        {
            Instantiate(buffDropperPrefab, destroyParticlePos.position, Quaternion.identity);

            ParticleSystem newDestroyParticle = Instantiate(destroyParticle);
            newDestroyParticle.transform.position = destroyParticlePos.position;
            newDestroyParticle.Play();

            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
