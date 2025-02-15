using UnityEngine;

public abstract class LevelPart : MonoBehaviour 
{
    public Transform inPoint;
    public Transform outPoint;
    public Vector3 offset;

    private void Start()
    {
        offset = inPoint.position - transform.position;
        print(offset);
    }
}
