using StarterAssets;
using UnityEngine;

public class PlayerMoveState : MonoBehaviour
{
    [SerializeField] private FirstPersonController personController;
    [SerializeField] private PlayerHitController hitController;
    [SerializeField] private WeaponChanger weaponChanger;

    public static PlayerMoveState instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void DisableMove()
    {
        personController.enabled = false;
        hitController.enabled = false;
        weaponChanger.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableMove()
    {
        personController.enabled = true;
        hitController.enabled = true;
        weaponChanger.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
