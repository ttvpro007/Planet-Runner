using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        PlayerData.EnablePlayerCoreComponents();
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        PlayerData.DisablePlayerCoreComponents();
        Time.timeScale = 0;
    }
}
