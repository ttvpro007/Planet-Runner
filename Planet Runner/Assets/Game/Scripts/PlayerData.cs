using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerController _playerControllerComp;
    public static PlayerController playerControllerComp { get { return _playerControllerComp; } }

    private static PlayerMovement _playerMovementComp;
    public static PlayerMovement playerMovementComp { get { return _playerMovementComp; } }

    private static PlayerAnimation _playerAnimationComp;
    public static PlayerAnimation playerAnimationComp { get { return _playerAnimationComp; } }

    private static Health _healthComp;
    public static Health healthComp { get { return _healthComp; } }

    private void Awake()
    {
        _playerControllerComp = GetComponent<PlayerController>();
        _playerMovementComp = GetComponent<PlayerMovement>();
        _playerAnimationComp = GetComponent<PlayerAnimation>();
        _healthComp = GetComponent<Health>();
    }

    public static void DisablePlayerCoreComponents()
    {
        _playerControllerComp.enabled = false;
        _playerMovementComp.enabled = false;
        _playerAnimationComp.enabled = false;
    }

    public static void EnablePlayerCoreComponents()
    {
        _playerControllerComp.enabled = true;
        _playerMovementComp.enabled = true;
        _playerAnimationComp.enabled = true;
    }
}