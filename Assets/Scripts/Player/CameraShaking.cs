using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System.Collections;

public class CameraShaking : MonoBehaviour
{
    public static CameraShaking Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(this);
    }

    public void ShakeCameraShot()
    {
        //StartCoroutine(HeadShake());
    }

    private IEnumerator HeadShake()
    {
        yield return UpHead();
        yield return DownHead();
    }

    private YieldInstruction UpHead()
    {
        return transform
            .DOLocalRotate(new Vector3(transform.localEulerAngles.x - 1, 0, 0), 0.1f)
            .Play()
            .WaitForCompletion();
    }

    private YieldInstruction DownHead()
    {
        return transform
            .DOLocalRotate(new Vector3(transform.localEulerAngles.x + 1, 0, 0), 0.1f)
            .Play()
            .WaitForCompletion();
    }
}
