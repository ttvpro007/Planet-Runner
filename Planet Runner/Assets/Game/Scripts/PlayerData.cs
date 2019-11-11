using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private static PlayerController _playerController;
    public static PlayerController playerController { get { return _playerController; } }

    private static PlayerMovement _playerMovement;
    public static PlayerMovement playerMovement { get { return _playerMovement; } }

    private static PlayerAnimation _playerAnimation;
    public static PlayerAnimation playerAnimation { get { return _playerAnimation; } }

    private static Health _health;
    public static Health health { get { return _health; } }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _health = GetComponent<Health>();
    }
}