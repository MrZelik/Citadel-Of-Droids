using System.Collections;
using UnityEngine;

public class WeaponKatana : Weapon
{
    [SerializeField] private string[] attackAnimationsName;
    [SerializeField] private string alternativeAttackAnimationName;

    public Collider Collider;

    public override void Hit()
    {
        if (lastHitTime > Time.time - hitCooldown || lastAlternativeHitTime > Time.time - alternativeHitCooldown)
            return;

        lastHitTime = Time.time;

        animator.Play(attackAnimationsName[Random.Range(0, attackAnimationsName.Length)]);
        StartCoroutine(ColliderHolder());
        PlayHitSound(false);
    }

    public override void AlternativeHit()
    {
        if (lastAlternativeHitTime > Time.time - alternativeHitCooldown || lastHitTime > Time.time - hitCooldown)
            return;

        lastAlternativeHitTime = Time.time;

        animator.Play(alternativeAttackAnimationName);
        StartCoroutine(AlternativeAttackHolder());
        PlayHitSound(true);
    }

    private IEnumerator AlternativeAttackHolder()
    {
        yield return new WaitForSeconds(0.15f);
        Projectile newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.position = projectileSpawnPoint.position;
        newProjectile.transform.rotation = transform.parent.transform.rotation;
        newProjectile.GetComponent<Rigidbody>().AddForce(transform.parent.transform.forward * newProjectile.speed, ForceMode.Impulse);
        newProjectile.StartCoroutine(newProjectile.LiveTimer());
    }

    private IEnumerator ColliderHolder()
    {
        yield return new WaitForSeconds(0.2f);
        Collider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        Collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IHittable>(out  IHittable hittable))
        {
            hittable.ApplyHit(transform.position, other.ClosestPoint(transform.position));
        }
    }
}   
