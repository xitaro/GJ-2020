// GENERATED AUTOMATICALLY FROM 'Assets/Input/New Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NewControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""New Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""bd7be063-39a6-4838-9652-28d02350d9e0"",
            ""actions"": [
                {
                    ""name"": ""Follow"",
                    ""type"": ""Value"",
                    ""id"": ""56f9dd6e-4c7e-4142-afc8-029b0207f5ce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc8f8cf5-291f-48d4-ae49-730268eadd41"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Kewboard & Mouse"",
                    ""action"": ""Follow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Kewboard & Mouse"",
            ""bindingGroup"": ""Kewboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Follow = m_Player.FindAction("Follow", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Follow;
    public struct PlayerActions
    {
        private @NewControls m_Wrapper;
        public PlayerActions(@NewControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Follow => m_Wrapper.m_Player_Follow;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Follow.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFollow;
                @Follow.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFollow;
                @Follow.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFollow;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Follow.started += instance.OnFollow;
                @Follow.performed += instance.OnFollow;
                @Follow.canceled += instance.OnFollow;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KewboardMouseSchemeIndex = -1;
    public InputControlScheme KewboardMouseScheme
    {
        get
        {
            if (m_KewboardMouseSchemeIndex == -1) m_KewboardMouseSchemeIndex = asset.FindControlSchemeIndex("Kewboard & Mouse");
            return asset.controlSchemes[m_KewboardMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnFollow(InputAction.CallbackContext context);
    }
}
