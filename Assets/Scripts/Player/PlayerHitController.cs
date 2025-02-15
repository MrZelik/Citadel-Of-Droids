using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] private InputActionProperty hitButtonInput;
    [SerializeField] private InputActionProperty alternativeHitButtonInput;

    public Weapon currentWeapon;

    public bool curentWeaponIsRiffle;

    private bool canFire;

    public static PlayerHitController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void OnEnable()
    {
        hitButtonInput.action.started += StartWeaponHit;
        hitButtonInput.action.canceled += StopHitting;
        alternativeHitButtonInput.action.started += StartAlternativeWeaponHit;
    }

    private void OnDisable()
    {
        hitButtonInput.action.started -= StartWeaponHit;
        hitButtonInput.action.canceled -= StopHitting;
        alternativeHitButtonInput.action.started -= StartAlternativeWeaponHit;
    }


    public void StartWeaponHit(InputAction.CallbackContext callback)
    {
        if (currentWeapon != null)
        {
            if (curentWeaponIsRiffle)
                currentWeapon.canHitting = true;
            else
                currentWeapon.Hit();
        }
            
    }

    public void StopHitting(InputAction.CallbackContext callback)
    {
        currentWeapon.canHitting = false;
    }

    public void StartAlternativeWeaponHit(InputAction.CallbackContext callback)
    {
        if (currentWeapon != null)
            currentWeapon.AlternativeHit();
    }
}
