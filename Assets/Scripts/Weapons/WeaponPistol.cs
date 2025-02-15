using DG.Tweening;
using UnityEngine;

public class WeaponPistol : Weapon
{
    [SerializeField] private string attackAnimationName;
    [SerializeField] private string alternativeAttackAnimationName;

    public override void Hit()
    {
        if (lastHitTime > Time.time - hitCooldown || lastAlternativeHitTime > Time.time - alternativeHitCooldown)
            return;

        if (!GlobalAmmoController.Instance.CheckAmmoAvailability(ammoType, ammoPerShot))
            return;

        lastHitTime = Time.time;

        ParticleSystem particle = Instantiate(shotParticle);
        particle.transform.position = projectileSpawnPoint.position;
        particle.Play();

        PlayHitSound(false);

        animator.Play(attackAnimationName);

        MinusAmmo();

        GameObject hitObject = Raycaster.instance.hit.collider.gameObject;

        Vector3 hitPos = Raycaster.instance.hit.point;

        Projectile projectilce = Instantiate(projectilePrefab);
        projectilce.transform.position = projectileSpawnPoint.transform.position;

        projectilce.transform.DOMove(hitPos, 0.1f).Play().OnComplete(() => Destroy(projectilce.gameObject));

        CameraShaking.Instance.ShakeCameraShot();

        if(hitObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.ApplyHit(transform.position, hitPos);
            CrossImageController.Instance.CheckHit();
        }
        else
        {
            ParticleSystem hitParticle = Instantiate(hitPrefab);
            hitParticle.transform.position = hitPos;
            hitParticle.transform.LookAt(transform.position);
            hitParticle.transform.parent = hitObject.transform;
            hitParticle.Play();
        }
    }
}
