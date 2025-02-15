using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class WeaponDublePistols : Weapon
{
    [SerializeField] private string attackAnimationName;
    [SerializeField] private string alternativeAttackAnimationName;

    public Animator secondHandAnimator;
    public Transform secondParticlePos;

    private bool nowRightShot = true;

    public override void ChangeMoveAnim()
    {
        base.ChangeMoveAnim();

        secondHandAnimator.SetBool("Move", FirstPersonController.Instance.isMove);
    }

    public override void Hit()
    {
        if (lastHitTime > Time.time - hitCooldown || lastAlternativeHitTime > Time.time - alternativeHitCooldown)
            return;

        if (!GlobalAmmoController.Instance.CheckAmmoAvailability(ammoType, ammoPerShot))
            return;

        lastHitTime = Time.time;

        ParticleSystem particle = Instantiate(shotParticle);
        particle.transform.position = projectileSpawnPoint.position;

        PlayHitSound(false);

        Vector3 hitPos = Raycaster.instance.hit.point;

        if (nowRightShot)
        {
            animator.Play(attackAnimationName);
            particle.transform.position = projectileSpawnPoint.position;

            Projectile projectilce = Instantiate(projectilePrefab);
            projectilce.transform.position = projectileSpawnPoint.transform.position;
            projectilce.transform.DOMove(hitPos, 0.1f).Play().OnComplete(() => Destroy(projectilce.gameObject));

            nowRightShot = false;
        }
        else
        {
            secondHandAnimator.Play(attackAnimationName);
            particle.transform.position = secondParticlePos.position;

            Projectile projectilce = Instantiate(projectilePrefab);
            projectilce.transform.position = secondParticlePos.transform.position;
            projectilce.transform.DOMove(hitPos, 0.1f).Play().OnComplete(() => Destroy(projectilce.gameObject));

            nowRightShot = true;
        }

        particle.Play();

        MinusAmmo();

        GameObject hitObject = Raycaster.instance.hit.collider.gameObject;

        CameraShaking.Instance.ShakeCameraShot();

        if (hitObject.TryGetComponent<Enemy>(out Enemy enemy))
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

    public override void AlternativeHit()
    {
        if (lastHitTime > Time.time - hitCooldown || lastAlternativeHitTime > Time.time - alternativeHitCooldown)
            return;

        if (!GlobalAmmoController.Instance.CheckAmmoAvailability(ammoType, ammoPerShot * 2))
            return;

        lastAlternativeHitTime = Time.time;

        ParticleSystem particle = Instantiate(shotParticle);
        particle.transform.position = projectileSpawnPoint.position;
        particle.Play();
        particle = Instantiate(shotParticle);
        particle.transform.position = secondParticlePos.position;
        particle.Play();

        PlayHitSound(true);

        animator.Play(attackAnimationName);
        secondHandAnimator.Play(attackAnimationName);

        MinusAmmo();
        MinusAmmo();

        GameObject hitObject = Raycaster.instance.hit.collider.gameObject;

        Vector3 hitPos = Raycaster.instance.hit.point;

        Projectile projectilce = Instantiate(projectilePrefab);
        projectilce.transform.position = projectileSpawnPoint.transform.position;
        projectilce.transform.DOMove(hitPos, 0.1f).Play().OnComplete(() => Destroy(projectilce.gameObject));

        Projectile secondProjectilce = Instantiate(projectilePrefab);
        secondProjectilce.transform.position = secondParticlePos.transform.position;
        secondProjectilce.transform.DOMove(hitPos, 0.1f).Play().OnComplete(() => Destroy(secondProjectilce.gameObject));

        CameraShaking.Instance.ShakeCameraShot();

        if (hitObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.ApplyHit(transform.position, hitPos);
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
