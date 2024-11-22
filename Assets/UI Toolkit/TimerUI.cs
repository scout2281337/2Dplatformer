using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerUI : MonoBehaviour
{
    private UIDocument document;
    private Label label;

    private void Start()
    {
        document = GetComponent<UIDocument>();

        label = document.rootVisualElement.Q<Label>("TimerText");
    }

    private void Update()
    {
        int minutes = (int)TimeManager.Instance.timeDifficulty / 60;
        int seconds = (int)TimeManager.Instance.timeDifficulty % 60;

        if (seconds < 10)
            label.text = $"{minutes}:0{seconds}";
        else
            label.text = $"{minutes}:{seconds}";
    }
}
