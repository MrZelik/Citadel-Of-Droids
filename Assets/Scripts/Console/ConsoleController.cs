using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleController : MonoBehaviour
{
    [SerializeField] private InputActionProperty openConsoleButton;
    [SerializeField] private InputActionProperty sendButton;
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private List<string> lines = new List<string>();
    [SerializeField] private List<Color> colorLines = new List<Color>();
    [SerializeField] private List<TextMeshProUGUI> storyText = new List<TextMeshProUGUI>();
    [SerializeField] private int maxLines;

    [SerializeField] private Color rightColor;
    [SerializeField] private Color wrongColor;
    [SerializeField] private Color emptyValueColor;

    private bool isOpen;

    public static ConsoleController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnEnable()
    {
        openConsoleButton.action.started += OpenConsole;
        sendButton.action.started += Send;
    }

    private void OnDisable()
    {
        openConsoleButton.action.started -= OpenConsole;
        sendButton.action.started -= Send;
    }

    public void OpenConsole(InputAction.CallbackContext callback)
    {
        if(!isOpen)
        {
            PlayerMoveState.instance.DisableMove();
            consolePanel.SetActive(true);
            isOpen = true;
        }
        else
        {
            CloseConsole();
        }
    }

    public void CloseConsole()
    {
        PlayerMoveState.instance.EnableMove();
        consolePanel.SetActive(false);
        isOpen = false;
    }

    public void Send(InputAction.CallbackContext callback)
    {
        if(isOpen && input.text != "")
        {
            UpdateStory(input.text, true);
        }
    }

    public void Send()
    {
        if (isOpen && input.text != "")
        {
            UpdateStory(input.text, true);
        }
    }

    public void UpdateStory(string text, bool needCheck)
    {
        if(lines.Count >= maxLines)
        {
            lines.RemoveAt(0);
            colorLines.RemoveAt(0);
        }

        lines.Add(text);

        if(needCheck)
        {
            string commandRight = CommandsCollector.Instance.CheckCommand(text);

            if (commandRight == "right")
            {
                colorLines.Add(rightColor);
            }
            else if (commandRight == "wrong")
            {
                lines[lines.Count - 1] += " - Команда не найдена (help - Вывести команды)";
                colorLines.Add(wrongColor);
            }
            else if (commandRight == "empty")
            {
                lines[lines.Count - 1] += " - у команды отсутствует значение";
                colorLines.Add(emptyValueColor);
            }
        }
        else
            colorLines.Add(Color.white);

        for (int i = 0; i < lines.Count; i++)
        {
            storyText[i].text = lines[i];
            storyText[i].color = colorLines[i];
        }

        input.text = "";
    }

    
}
