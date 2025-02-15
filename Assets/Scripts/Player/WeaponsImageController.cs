using UnityEngine;
using UnityEngine.UI;

public class WeaponsImageController : MonoBehaviour
{
    [SerializeField] private Image[] weaponImages;

    private void OnEnable()
    {
        WeaponChanger.OnWeaponChanged += ChangeCurrentWeapon;
    }

    private void OnDisable()
    {
        WeaponChanger.OnWeaponChanged -= ChangeCurrentWeapon;
    }

    private void ChangeCurrentWeapon(int needWeapon)
    {
        for(int i = 0; i < weaponImages.Length; i++)
        {
            Color color = weaponImages[i].color;
            color.a = 0.3f;

            Color choiseColor = weaponImages[i].color;
            choiseColor.a = 1f;

            weaponImages[i].sprite = WeaponChanger.instance.weapons[i].weaponImage;

            if (i == needWeapon)
                weaponImages[i].color = choiseColor;
            else
                weaponImages[i].color = color;
        }
    }
}
