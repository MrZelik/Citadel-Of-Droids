using UnityEngine;

public class Raycaster : MonoBehaviour
{
    int layerMask = 18;

    public RaycastHit hit;

    public static Raycaster instance;

    private void Awake() 
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        layerMask = 1 << 0;
        layerMask += 1 << 15;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
        }
    }
}
