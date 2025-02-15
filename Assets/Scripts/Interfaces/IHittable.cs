using UnityEngine;

public interface IHittable
{
    public void ApplyHit(Vector3 playerPosition, Vector3 hitPosition);
}
