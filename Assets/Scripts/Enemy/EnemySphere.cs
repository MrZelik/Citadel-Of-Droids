using System.Collections;
using UnityEngine;

public class EnemySphere : Enemy
{
    [SerializeField] private Transform[] projectilePos;
    [SerializeField] private float maxDistancePerAttack;
    [SerializeField] private float shotCooldown;
    [SerializeField] private Projectile projectilePrefab;

    private float lastShotTime;

    private void Update()
    {
        CheckShot();
    }

    private void CheckShot()
    {
        if (Vector3.Distance(transform.position, player.position) < maxDistancePerAttack)
        {
            if(lastShotTime < Time.time -  shotCooldown)
            {
                animator.Play(attackAnimName);
                lastShotTime = Time.time;
                StartCoroutine(Shot());
            }
        }
    }

    private IEnumerator Shot()
    {
        yield return new WaitForSeconds(1.2f);

        for(int i = 0; i < projectilePos.Length; i++)
        {
            projectilePos[i].LookAt(player.position);

            Projectile newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = projectilePos[i].position;
            newProjectile.transform.rotation = projectilePos[i].rotation;
            newProjectile.GetComponent<Rigidbody>().AddForce(projectilePos[i].forward * newProjectile.speed, ForceMode.Impulse);
            newProjectile.StartCoroutine(newProjectile.LiveTimer());
        }
    }
}
