using StarterAssets;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
    public Sprite weaponImage;

    public float hitCooldown;
    public float alternativeHitCooldown;

    public Animator animator;

    public float lastHitTime;
    public float lastAlternativeHitTime;

    public Transform projectileSpawnPoint;

    public Projectile projectilePrefab;
    public ParticleSystem hitPrefab;    //TEST
    public ParticleSystem shotParticle;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip alternativeHitSound;

    public bool isRiffle;

    public bool canHitting;

    public GlobalAmmoController.AmmoTypes ammoType;
    public int ammoPerShot;

    private void OnEnable()
    {
        FirstPersonController.OnMoveChange += ChangeMoveAnim;
    }

    private void OnDisable()
    {
        FirstPersonController.OnMoveChange -= ChangeMoveAnim;
    }

    public virtual void Hit()
    {

    }

    public virtual void AlternativeHit()
    {

    }

    public virtual void ChangeMoveAnim()
    {
        animator.SetBool("Move", FirstPersonController.Instance.isMove);
    }

    public void PlayHitSound(bool alternative)
    {
        if (alternative)
            audioSource.PlayOneShot(alternativeHitSound);
        else 
            audioSource.PlayOneShot(hitSound);
    }

    public void MinusAmmo()
    {
        GlobalAmmoController.Instance.MinusAmmo(ammoType, ammoPerShot);
    }

    public Vector3 RandomizeHitPoint(Vector3 hitPoint)
    {
        float randX = Random.Range(hitPoint.x -0.3f, hitPoint.x +0.3f);
        float randY = Random.Range(hitPoint.y -0.3f, hitPoint.y +0.3f);
        float randZ = Random.Range(hitPoint.z -0.3f, hitPoint.z +0.3f);

        return new Vector3(randX, randY, randZ);
    }

}
