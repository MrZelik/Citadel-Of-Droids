using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class WeaponChanger : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionProperty choiseFirstWeaponButton;
    [SerializeField] private InputActionProperty choiseSecondWeaponButton;
    [SerializeField] private InputActionProperty choiseThirdWeaponButton;
    [SerializeField] private InputActionProperty choiseFourthWeaponButton;
    [SerializeField] private InputActionProperty mouseWheelUp;
    [SerializeField] private InputActionProperty mouseWheelDown;

    [Header("other")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip changeWeaponSound;

    private int _currentWeapon = 0;

    public Weapon[] weapons;

    public PlayerHitController playerHitController;

    public static WeaponChanger instance;

    public static event Action<int> OnWeaponChanged;

    public int currentWeapon => _currentWeapon;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        playerHitController = GetComponent<PlayerHitController>();

        StartCoroutine(ChangeWeapon(_currentWeapon));
    }

    private void OnEnable()
    {
        choiseFirstWeaponButton.action.started += ChoiseFirstWeapon;
        choiseSecondWeaponButton.action.started += ChoiseSecondWeapon;
        choiseThirdWeaponButton.action.started += ChoiseThirdWeapon;
        choiseFourthWeaponButton.action.started += ChoiseFourthWeapon;
        mouseWheelUp.action.started += MouseWheelMoveUp;
        mouseWheelDown.action.started += MouseWheelMoveDown;
    }

    private void OnDisable()
    {
        choiseFirstWeaponButton.action.started -= ChoiseFirstWeapon;
        choiseSecondWeaponButton.action.started -= ChoiseSecondWeapon;
        choiseThirdWeaponButton.action.started -= ChoiseThirdWeapon;
        choiseFourthWeaponButton.action.started -= ChoiseFourthWeapon;
        mouseWheelUp.action.started -= MouseWheelMoveUp;
        mouseWheelDown.action.started -= MouseWheelMoveDown;
    }

    private void MouseWheelMoveUp(InputAction.CallbackContext callback)
    {
        if(currentWeapon == weapons.Length-1)
            StartCoroutine(ChangeWeapon(0));
        else
            StartCoroutine(ChangeWeapon(currentWeapon + 1));
    }

    private void MouseWheelMoveDown(InputAction.CallbackContext callback)
    {
        if(currentWeapon == 0)
            StartCoroutine(ChangeWeapon(weapons.Length-1));
        else
            StartCoroutine(ChangeWeapon(currentWeapon - 1));
    }

    private void ChoiseFirstWeapon(InputAction.CallbackContext callback)
    {
        if (_currentWeapon == 0)
            return;

        StartCoroutine(ChangeWeapon(0));
    }

    private void ChoiseSecondWeapon(InputAction.CallbackContext callback)
    {
        if (_currentWeapon == 1)
            return;

        StartCoroutine(ChangeWeapon(1));
    }

    private void ChoiseThirdWeapon(InputAction.CallbackContext callback)
    {
        if (_currentWeapon == 2)
            return;

        StartCoroutine(ChangeWeapon(2));
    }

    private void ChoiseFourthWeapon(InputAction.CallbackContext callback)
    {
        if (_currentWeapon == 3)
            return;

        StartCoroutine(ChangeWeapon(3));
    }

    public void AddNewWeapon(Weapon newWeapon)
    {
        bool equip = false;

        for(int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i] == null)
            {
                weapons[i] = newWeapon;
                equip = true;
                return;
            }
        }

        if (!equip)
        {
            weapons[_currentWeapon] = newWeapon;
        }
    }

    private IEnumerator ChangeWeapon(int needWeaponId)
    {
        playerHitController.currentWeapon = null;

        yield return CloseWeapon();

        OnWeaponChanged?.Invoke(needWeaponId);

        audioSource.PlayOneShot(changeWeaponSound);

        weapons[_currentWeapon].gameObject.SetActive(false);
        _currentWeapon = needWeaponId;
        weapons[_currentWeapon].gameObject.SetActive(true);

        GlobalAmmoController.Instance.UpdateAmmoView(weapons[_currentWeapon].ammoType);

        yield return OpenWeapon();
        
        playerHitController.currentWeapon = weapons[_currentWeapon];

        playerHitController.curentWeaponIsRiffle = weapons[_currentWeapon].isRiffle;
    }

    private YieldInstruction CloseWeapon()
    {
        return weaponHolder
            .DOLocalRotate(new Vector3(50, 0, 0), 0.2f)
            .Play()
            .WaitForCompletion();
    }

    private YieldInstruction OpenWeapon()
    {
        return weaponHolder
            .DOLocalRotate(new Vector3(0, 0, 0), 0.2f)
            .Play()
            .WaitForCompletion();
    }
}
