using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CommandsCollector : MonoBehaviour
{
    public static CommandsCollector Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public Dictionary<int, string> commands = new Dictionary<int, string>()
    {
        {0, "help" },
        {1, "noclip" },
        {2, "add_health" },
        {3, "set_max_health" },
        {4, "add_ammo" },
        {6, "god_mode" }
    };

    public string CheckCommand(string command)
    {
        string separateCommad = SeparateWords(command, true);

        if(separateCommad == "help")
        {
            ConsoleCommandExecuting.Instance.ExecutCommand(0, "");
            return "right";
        }

        foreach (KeyValuePair<int, string> cmd in commands)
        {
            if (cmd.Value == separateCommad)
            {
                if (SeparateWords(command, false) == "")
                    return "empty";
                else
                {
                    ConsoleCommandExecuting.Instance.ExecutCommand(cmd.Key, SeparateWords(command, false));
                    return "right";
                }
            }
        }

        return "wrong";
    }

    public string SeparateWords(string command, bool needCommand)
    {
        string separateCommad = "";
        string value = "";

        bool isValue = false;

        foreach (char c in command)
        {
            if (c == 32)
            {
                isValue = true;
                continue;
            }
            else if (!isValue)
                separateCommad += c;
            else if (isValue)
                value += c;
        }
        
        if(needCommand)
            return separateCommad;
        else 
            return value;
    }
}
