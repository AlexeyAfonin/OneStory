using Core.Base;
using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class HelpWindow : BaseWindow<HelpWindow>
{
    [Space(10f)]
    [SerializeField] private TextMeshProUGUI promptText;

    public void ShowPrompt(string prompt)
    {
        promptText.text = prompt;
        Show();
    }
}
