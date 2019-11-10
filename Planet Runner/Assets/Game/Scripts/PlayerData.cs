using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerController _playerController;
    public static PlayerController playerController { get { return _playerController; } }

    private static PlayerAnimation _playerAnimation;
    public static PlayerAnimation playerAnimation { get { return _playerAnimation; } }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }
}