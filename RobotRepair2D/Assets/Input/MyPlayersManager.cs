using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class MyPlayersManager : MonoBehaviour
{
    [SerializeField] private PlayerController[] playerControllers;

    private int playerCount = 2;

    InputUser[] _users;
    Gamepad[] _gamepads;

    int registeredPlayers;

    void Start()
    {
        _users = new InputUser[playerCount];
        _gamepads = new Gamepad[playerCount];

        InputUser.listenForUnpairedDeviceActivity = playerCount;

        InputUser.onChange += OnControlsChanged;
        InputUser.onUnpairedDeviceUsed += InputUser_onUnpairedDeviceUsed;

        for (var i = 0; i < _users.Length; i++)
        {
            _users[i] = InputUser.CreateUserWithoutPairedDevices();
        }
    }

    private void InputUser_onUnpairedDeviceUsed(InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr arg2)
    {
        if (control.device is Gamepad)
        {
            for (var i = 0; i < _users.Length; i++)
            {
                // find a user without a paired device
                if (_users[i].pairedDevices.Count == 0)
                {
                    // pair the new Gamepad device to that user
                    _users[i] = InputUser.PerformPairingWithDevice(control.device, _users[i]);
                    Debug.Log("Paired a user");
                    registeredPlayers++;

                    if (registeredPlayers == 2)
                        SetupPlayers();

                    return;
                }
            }
        }
    }

    private void SetupPlayers()
    {
        playerControllers[0].RegisterControls(_users[0], _users[1]);
        playerControllers[1].RegisterControls(_users[1], _users[0]);


        GameManager.Instance.MoveToNextLevel(); // Moves into first level once both controllers have been detected!
    }

    void OnControlsChanged(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.DevicePaired)
        {
            var playerId = _users.ToList().IndexOf(user);
            _gamepads[playerId] = device as Gamepad;
        }
        else if (change == InputUserChange.DeviceUnpaired)
        {
            var playerId = _users.ToList().IndexOf(user);
            _gamepads[playerId] = null;
        }
    }
}