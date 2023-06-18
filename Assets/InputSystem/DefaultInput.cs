// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/DefaultInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DefaultInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultInput"",
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
                }
            ]
        },
        {
            ""name"": ""PlayerMove"",
            ""id"": ""0c31dc9c-3151-4c4c-907c-10f495b41a03"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1d41d4e6-48e4-4578-8648-33dcf334801e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""1c1f1a5c-1f27-4c20-a573-dde4af5733c0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""964bc4f4-64c6-4103-bd9f-96eda42f73c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c5d40778-f743-4c57-9f71-c26f80f605fb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7693518e-7e01-476c-be3f-47df80bfd713"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2d14aa5b-d6ae-42a6-82ea-c7c044941b2a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""234b8696-7731-4841-83e9-02831447ed41"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e200bb88-c5bb-4baa-80e7-7dacd77e2aa7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""70f099db-df8e-4a2c-8095-100feed51598"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1d4220b-2373-4f71-bdb2-03bdb5f0f4dc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Control"",
            ""id"": ""f856c78a-23ab-42e0-afca-0016592cbbb2"",
            ""actions"": [
                {
                    ""name"": ""Place Building"",
                    ""type"": ""Button"",
                    ""id"": ""cc37214a-2a2a-4475-b5c8-3d81b4c44435"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select Building"",
                    ""type"": ""Value"",
                    ""id"": ""beac602e-f5c0-42b7-a3f6-3e38a8c82ebc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""126ad29e-4906-410e-ac6b-1e1257d858c4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Place Building"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""642f6b58-2c17-4725-b538-2625e3c03d03"",
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
        // PlayerMove
        m_PlayerMove = asset.FindActionMap("PlayerMove", throwIfNotFound: true);
        m_PlayerMove_Move = m_PlayerMove.FindAction("Move", throwIfNotFound: true);
        m_PlayerMove_Rotate = m_PlayerMove.FindAction("Rotate", throwIfNotFound: true);
        m_PlayerMove_Jump = m_PlayerMove.FindAction("Jump", throwIfNotFound: true);
        // Control
        m_Control = asset.FindActionMap("Control", throwIfNotFound: true);
        m_Control_PlaceBuilding = m_Control.FindAction("Place Building", throwIfNotFound: true);
        m_Control_SelectBuilding = m_Control.FindAction("Select Building", throwIfNotFound: true);
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
    public struct DEBUGActions
    {
        private @DefaultInput m_Wrapper;
        public DEBUGActions(@DefaultInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Spawn => m_Wrapper.m_DEBUG_Spawn;
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
            }
            m_Wrapper.m_DEBUGActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Spawn.started += instance.OnSpawn;
                @Spawn.performed += instance.OnSpawn;
                @Spawn.canceled += instance.OnSpawn;
            }
        }
    }
    public DEBUGActions @DEBUG => new DEBUGActions(this);

    // PlayerMove
    private readonly InputActionMap m_PlayerMove;
    private IPlayerMoveActions m_PlayerMoveActionsCallbackInterface;
    private readonly InputAction m_PlayerMove_Move;
    private readonly InputAction m_PlayerMove_Rotate;
    private readonly InputAction m_PlayerMove_Jump;
    public struct PlayerMoveActions
    {
        private @DefaultInput m_Wrapper;
        public PlayerMoveActions(@DefaultInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMove_Move;
        public InputAction @Rotate => m_Wrapper.m_PlayerMove_Rotate;
        public InputAction @Jump => m_Wrapper.m_PlayerMove_Jump;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMoveActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMoveActions instance)
        {
            if (m_Wrapper.m_PlayerMoveActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnRotate;
                @Jump.started -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMoveActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerMoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerMoveActions @PlayerMove => new PlayerMoveActions(this);

    // Control
    private readonly InputActionMap m_Control;
    private IControlActions m_ControlActionsCallbackInterface;
    private readonly InputAction m_Control_PlaceBuilding;
    private readonly InputAction m_Control_SelectBuilding;
    public struct ControlActions
    {
        private @DefaultInput m_Wrapper;
        public ControlActions(@DefaultInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceBuilding => m_Wrapper.m_Control_PlaceBuilding;
        public InputAction @SelectBuilding => m_Wrapper.m_Control_SelectBuilding;
        public InputActionMap Get() { return m_Wrapper.m_Control; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlActions set) { return set.Get(); }
        public void SetCallbacks(IControlActions instance)
        {
            if (m_Wrapper.m_ControlActionsCallbackInterface != null)
            {
                @PlaceBuilding.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnPlaceBuilding;
                @PlaceBuilding.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnPlaceBuilding;
                @PlaceBuilding.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnPlaceBuilding;
                @SelectBuilding.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnSelectBuilding;
                @SelectBuilding.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnSelectBuilding;
                @SelectBuilding.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnSelectBuilding;
            }
            m_Wrapper.m_ControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlaceBuilding.started += instance.OnPlaceBuilding;
                @PlaceBuilding.performed += instance.OnPlaceBuilding;
                @PlaceBuilding.canceled += instance.OnPlaceBuilding;
                @SelectBuilding.started += instance.OnSelectBuilding;
                @SelectBuilding.performed += instance.OnSelectBuilding;
                @SelectBuilding.canceled += instance.OnSelectBuilding;
            }
        }
    }
    public ControlActions @Control => new ControlActions(this);
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
    }
    public interface IPlayerMoveActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IControlActions
    {
        void OnPlaceBuilding(InputAction.CallbackContext context);
        void OnSelectBuilding(InputAction.CallbackContext context);
    }
}
