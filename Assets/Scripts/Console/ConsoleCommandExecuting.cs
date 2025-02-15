using System;
using System.Collections;
using System.Security.Permissions;
using UnityEngine;

public class ConsoleCommandExecuting : MonoBehaviour
{
    [SerializeField] private PlayerHealthController healthController;
    [SerializeField] private GlobalAmmoController ammoController;

    public static ConsoleCommandExecuting Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void ExecutCommand(int commandId, string commandValue)
    {

        switch (commandId)
        {
            case 0:
                StartCoroutine(PrintAllCommands());
                break;

            case 2: 
                healthController.AddHealth(Int32.Parse(commandValue)); 
                break;

            case 3:
                healthController.maxHealth = Int32.Parse(commandValue);
                break;

            case 4:
                ammoController.AddAmmo(GlobalAmmoController.AmmoTypes.plasm, Int32.Parse(commandValue));
                ammoController.AddAmmo(GlobalAmmoController.AmmoTypes.fire, Int32.Parse(commandValue));
                ammoController.AddAmmo(GlobalAmmoController.AmmoTypes.green, Int32.Parse(commandValue));
                break;

            case 6:
                healthController.GodModeChange(Int32.Parse(commandValue));
                ammoController.GodModeChange(Int32.Parse(commandValue));
                break;

            default:
                break;
        }
    }

    public IEnumerator PrintAllCommands()
    {
        yield return new WaitForSeconds(0.1f);
        ConsoleController.Instance.UpdateStory("noclip '1/0' - режим свободного перемещения", false);
        ConsoleController.Instance.UpdateStory("add_health 'значение' - добавляет указанное кол-во здоровья", false);
        ConsoleController.Instance.UpdateStory("set_max_health 'значние' - повышает макс здоровье на 'значение'", false);
        ConsoleController.Instance.UpdateStory("add_ammo 'значение' - добавление указанное кол-во патрон", false);
        ConsoleController.Instance.UpdateStory("god_mode '1/0' - включает режим бога", false);
    }
}
