using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float liveTime;
    [SerializeField] private int damage;
    public float speed;
    [SerializeField] private bool needRotate;
    [SerializeField] private Vector3 rotAxis;

    private void Start()
    {
        if (!needRotate)
            return;

        transform
            .DOLocalRotate(rotAxis, 0.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .Play();

        StartCoroutine(LiveTimer());
    }

    public IEnumerator LiveTimer()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHittable>(out IHittable hittable))
        {
            //hittable.ApplyHit(transform.position, other.ClosestPoint(transform.position));

            transform.DOKill();

            Destroy(gameObject);
        }

        if(other.gameObject.TryGetComponent<PlayerHealthController>(out PlayerHealthController playerhealthcontroller))
        {
            transform.DOKill();

            playerhealthcontroller.MinusHealth(damage);

            Destroy(gameObject);
        }
    }
}
