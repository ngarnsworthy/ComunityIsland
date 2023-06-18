// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""DEBUG"",
            ""id"": ""a9678595-4d67-42c3-b9f6-2a3c92939089"",
            ""actions"": [
                {
                    ""name"": ""Spawn"",
                    ""type"": ""Button"",
                    ""id"": ""466c4c74-15ed-44c1-a791-06058e3a23ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select Building"",
                    ""type"": ""Value"",
                    ""id"": ""7320ddae-e6f1-4a6a-8351-ab314f8f541b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d8fb77d6-905b-4e71-820e-13273a36accd"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Spawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9fc7199-f172-47d3-959b-0818e2c3682d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Select Building"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
        // DEBUG
        m_DEBUG = asset.FindActionMap("DEBUG", throwIfNotFound: true);
        m_DEBUG_Spawn = m_DEBUG.FindAction("Spawn", throwIfNotFound: true);
        m_DEBUG_SelectBuilding = m_DEBUG.FindAction("Select Building", throwIfNotFound: true);
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

    // DEBUG
    private readonly InputActionMap m_DEBUG;
    private IDEBUGActions m_DEBUGActionsCallbackInterface;
    private readonly InputAction m_DEBUG_Spawn;
    private readonly InputAction m_DEBUG_SelectBuilding;
    public struct DEBUGActions
    {
        private @Input m_Wrapper;
        public DEBUGActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Spawn => m_Wrapper.m_DEBUG_Spawn;
        public InputAction @SelectBuilding => m_Wrapper.m_DEBUG_SelectBuilding;
        public InputActionMap Get() { return m_Wrapper.m_DEBUG; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DEBUGActions set) { return set.Get(); }
        public void SetCallbacks(IDEBUGActions instance)
        {
            if (m_Wrapper.m_DEBUGActionsCallbackInterface != null)
            {
                @Spawn.started -= m_Wrapper.m_DEBUGActionsCallbackInterface.OnSpawn;
                @Spawn.performed -= m_Wrapper.m_DEBUGActionsCallbackInterface.OnSpawn;
                @Spawn.canceled -= m_Wrapper.m_DEBUGActionsCallbackInterface.OnSpawn;
                @SelectBuilding.started -= m_Wrapper.m_DEBUGActionsCallbackInterface.OnSelectBuilding;
                @SelectBuilding.performed -= m_Wrapper.m_DEBUGActionsCallbackInterface.OnSelectBuilding;
                @SelectBuilding.canceled -= m_Wrapper.m_DEBUGActionsCallbackInterface.OnSelectBuilding;
            }
            m_Wrapper.m_DEBUGActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Spawn.started += instance.OnSpawn;
                @Spawn.performed += instance.OnSpawn;
                @Spawn.canceled += instance.OnSpawn;
                @SelectBuilding.started += instance.OnSelectBuilding;
                @SelectBuilding.performed += instance.OnSelectBuilding;
                @SelectBuilding.canceled += instance.OnSelectBuilding;
            }
        }
    }
    public DEBUGActions @DEBUG => new DEBUGActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IDEBUGActions
    {
        void OnSpawn(InputAction.CallbackContext context);
        void OnSelectBuilding(InputAction.CallbackContext context);
    }
}
