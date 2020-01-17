using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    SoundManager soundManager = SoundManager.instance;

    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(soundManager.PlayButtonSound);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
