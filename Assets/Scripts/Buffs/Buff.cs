using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private Material[] buffMaterials;
    [SerializeField] private Renderer renderer;
    [SerializeField] private int buffCount;

    [SerializeField] private BuffTypes buffType;
    [SerializeField] private GlobalAmmoController.AmmoTypes ammoType;

    public enum BuffTypes
    {
        health,
        ammo
    }

    private void Start()
    {
        SelectBuff();
    }

    private void SelectBuff()
    {
        int buffId = Random.Range(0, buffMaterials.Length);

        renderer.material = buffMaterials[buffId];

        switch (buffId)
        {
            case 0:
                ammoType = GlobalAmmoController.AmmoTypes.plasm;
                buffType = BuffTypes.ammo;
                break;

            case 1:
                ammoType = GlobalAmmoController.AmmoTypes.fire;
                buffType = BuffTypes.ammo;
                break;

            case 2:
                ammoType = GlobalAmmoController.AmmoTypes.green;
                buffType = BuffTypes.ammo;
                break;

            case 3:
                buffType = BuffTypes.health;
                ammoType = GlobalAmmoController.AmmoTypes.none;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerHealthController>(out PlayerHealthController healthController)
            && buffType == BuffTypes.health)
        {
            healthController.AddHealth(buffCount);

            transform
                .DOJump(transform.position, 1.2f, 1, 0.3f)
                .Play()
                .OnComplete(() => Destroy(gameObject));
        }

        if(other.gameObject.TryGetComponent<PlayerHealthController>(out PlayerHealthController anotherController)
            && buffType == BuffTypes.ammo)
        {
            GlobalAmmoController.Instance.AddAmmo(ammoType, buffCount);

            transform
                .DOJump(transform.position, 1.2f, 1, 0.3f)
                .Play()
                .OnComplete(() => Destroy(gameObject));

        }
    }
}
