using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAmmoController : MonoBehaviour
{
    public static GlobalAmmoController Instance;

    public int plasmAmmoCount;
    public int fireAmmoCount;
    public int greenAmmoCount;

    public Sprite plasmIcon;
    public Sprite fireIcon;
    public Sprite greenIcon;

    public TextMeshProUGUI ammoCountText;
    public Image ammoImage;

    public bool godMode;

    public enum AmmoTypes
    {
        plasm,
        fire,
        green,
        none
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void GodModeChange(int value)
    {
        if (value == 0)
            godMode = false;
        else
            godMode = true;
    }

    public void AddAmmo(AmmoTypes ammoType, int countAmmo)
    {
        switch(ammoType)
        {
            case AmmoTypes.plasm:
                plasmAmmoCount += countAmmo;
                break;

            case AmmoTypes.fire:
                fireAmmoCount += countAmmo; 
                break;

            case AmmoTypes.green:
                greenAmmoCount += countAmmo;
                break;
        }

        UpdateAmmoView(ammoType);
    }

    public void MinusAmmo(AmmoTypes ammoType, int countAmmo)
    {
        if (godMode)
            return;

        switch (ammoType)
        {
            case AmmoTypes.plasm:
                plasmAmmoCount -= countAmmo;
                break;

            case AmmoTypes.fire:
                fireAmmoCount -= countAmmo;
                break;

            case AmmoTypes.green:
                greenAmmoCount -= countAmmo;
                break;
        }

        UpdateAmmoView(ammoType);
    }

    public bool CheckAmmoAvailability(AmmoTypes ammoType, int needAmmo)
    {
        switch (ammoType)
        {
            case AmmoTypes.plasm:
                if (plasmAmmoCount >= needAmmo)
                    return true;
                else
                    return false;
                break;

            case AmmoTypes.fire:
                if (fireAmmoCount >= needAmmo)
                    return true;
                else
                    return false;
                break;

            case AmmoTypes.green:
                if (greenAmmoCount >= needAmmo)
                    return true;
                else
                    return false;
                break;

            default:
                return false;
                break;
        }
    }

    public void UpdateAmmoView(AmmoTypes ammoType)
    {
        switch (ammoType)
        {
            case AmmoTypes.plasm:
                if (WeaponChanger.instance.weapons[WeaponChanger.instance.currentWeapon].ammoType != ammoType)
                    return;
                ammoCountText.text = plasmAmmoCount.ToString();
                ammoImage.sprite = plasmIcon;
                break;

            case AmmoTypes.fire:
                if (WeaponChanger.instance.weapons[WeaponChanger.instance.currentWeapon].ammoType != ammoType)
                    return;
                ammoCountText.text = fireAmmoCount.ToString();
                ammoImage.sprite = fireIcon;
                break;

            case AmmoTypes.green:
                if (WeaponChanger.instance.weapons[WeaponChanger.instance.currentWeapon].ammoType != ammoType)
                    return;
                ammoCountText.text = greenAmmoCount.ToString();
                ammoImage.sprite = greenIcon;
                break;
        }
    }
}
