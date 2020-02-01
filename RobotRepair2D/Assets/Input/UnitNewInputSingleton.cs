using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Gemserk.LD44.Game.Logic
{
    [CreateAssetMenu(menuName = "Gemserk/New Input Singleton")]
    public class UnitNewInputSingleton : ScriptableObject
    {
        [NonSerialized]
        private bool _init;

        [NonSerialized]
        private InputUser[] _users;

        [NonSerialized]
        private Gamepad[] _gamepads;

        [NonSerialized]
        private int lastPlayer;

        public Gamepad[] Gamepads => _gamepads;

        private void Init()
        {
            if (_init)
                return;

            lastPlayer = 0;
            _users = new InputUser[4];
            _gamepads = new Gamepad[4];

            Debug.Log("On input singleton enabled");

            InputUser.listenForUnpairedDeviceActivity = 4;

            InputUser.onChange += OnControlsChanged;
            InputUser.onUnpairedDeviceUsed += InputUser_onUnpairedDeviceUsed;

            for (var i = 0; i < _users.Length; i++)
            {
                _users[i] = InputUser.CreateUserWithoutPairedDevices();
            }

            _init = true;
        }

        private void InputUser_onUnpairedDeviceUsed(InputControl control, UnityEngine.InputSystem.LowLevel.InputEventPtr arg2)
        {
            if (control.device is Gamepad)
            {
                for (var i = 0; i < _users.Length; i++)
                {
                    if (_users[i].pairedDevices.Count == 0)
                    {
                        _users[i] = InputUser.PerformPairingWithDevice(control.device, _users[i]);
                        return;
                    }
                }
            }
        }

        private void OnControlsChanged(InputUser user, InputUserChange change, InputDevice device)
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

        public int RegisterPlayer()
        {
            Init();
            return lastPlayer++;
        }

        public Gamepad GetGamepad(int playerId)
        {
            Init();
            // Debug.Log("On get gamepad: " + playerId);
            return _gamepads[playerId];
        }

        public void Reset()
        {
            Init();
            lastPlayer = 0;
        }

    }
}
