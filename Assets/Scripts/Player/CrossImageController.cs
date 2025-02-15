using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrossImageController : MonoBehaviour
{
    [SerializeField] private Image crossImage;
    [SerializeField] private Color stockColor;
    [SerializeField] private Color mainHitColor;
    [SerializeField] private Color headHitColor;

    public static CrossImageController Instance;

    private Sequence crossAnim = DOTween.Sequence();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        crossAnim
            .Append(crossImage.transform.DOScale(0.7f, 0.1f))
            .Append(crossImage.transform.DOScale(0.5f, 0.1f))
            .Restart();
    }

    public void CheckHit()
    {
        StopAllCoroutines();
        StartCoroutine(CrossHit(mainHitColor));
    }

    private IEnumerator CrossHit(Color needColor)
    {
        crossImage.color = needColor;
        yield return CrossAnimatorUp();
        yield return CrossAnimatorDown();
        crossImage.color = stockColor;
    }

    private YieldInstruction CrossAnimatorUp()
    {
        return crossImage.transform.DOScale(0.6f, 0.05f).Play().WaitForCompletion();
    }

    private YieldInstruction CrossAnimatorDown()
    {
        return crossImage.transform.DOScale(0.5f, 0.05f).Play().WaitForCompletion();
    }
}
