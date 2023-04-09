//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.1
//     from Assets/ImputSystem/PlayerInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""fbf3a550-83c6-46e3-bd73-2a9bc3483ad1"",
            ""actions"": [
                {
                    ""name"": ""Axes"",
                    ""type"": ""Value"",
                    ""id"": ""6e1e6a6c-d0d7-44f2-a879-57c1398345eb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1fa43041-250d-4b97-a06c-c8f7eaba9a5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Positive"",
                    ""type"": ""Button"",
                    ""id"": ""e91211ba-79d6-473c-8d27-a1a8db4f112a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Negative"",
                    ""type"": ""Button"",
                    ""id"": ""972b8ebe-a5f1-472a-b6ed-529bbaca16c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""faff7f2d-5030-4465-b2d5-6ef2c6a83fcc"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7824e751-4df7-448e-ac01-2ac2efb3f1a4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9554c157-6a8a-4141-904f-590de0e2baec"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dbff840a-81b7-471c-b18a-7dc696a97cbc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fa93742a-4a83-45c0-bff6-b895c52967a0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""dce3a99a-3213-44bc-90ce-41375a1652c2"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""508062be-0e91-4ac7-a25f-a43c39f06f79"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b7156a79-130b-4774-b74e-c460735a28c0"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9d9aa1d7-9b25-4c58-945c-d28206561eeb"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ddefcc2f-aae1-42a4-beac-9e0875079125"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4a2ae4bc-e962-4446-a290-47efcc94bbb2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ceddd1c-e3db-400f-867f-91222bbcba15"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Positive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd9398a5-8691-4983-ab56-67f4e700219f"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Positive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b54b518d-0c67-43b1-82bc-d6c6811c2609"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Negative"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe57b0f3-7613-4e7e-ae5d-6f7cbfc9b4a6"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Negative"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Axes = m_GamePlay.FindAction("Axes", throwIfNotFound: true);
        m_GamePlay_Jump = m_GamePlay.FindAction("Jump", throwIfNotFound: true);
        m_GamePlay_Positive = m_GamePlay.FindAction("Positive", throwIfNotFound: true);
        m_GamePlay_Negative = m_GamePlay.FindAction("Negative", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Axes;
    private readonly InputAction m_GamePlay_Jump;
    private readonly InputAction m_GamePlay_Positive;
    private readonly InputAction m_GamePlay_Negative;
    public struct GamePlayActions
    {
        private @PlayerInputAction m_Wrapper;
        public GamePlayActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Axes => m_Wrapper.m_GamePlay_Axes;
        public InputAction @Jump => m_Wrapper.m_GamePlay_Jump;
        public InputAction @Positive => m_Wrapper.m_GamePlay_Positive;
        public InputAction @Negative => m_Wrapper.m_GamePlay_Negative;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Axes.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAxes;
                @Axes.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAxes;
                @Axes.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnAxes;
                @Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Positive.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPositive;
                @Positive.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPositive;
                @Positive.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPositive;
                @Negative.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnNegative;
                @Negative.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnNegative;
                @Negative.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnNegative;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Axes.started += instance.OnAxes;
                @Axes.performed += instance.OnAxes;
                @Axes.canceled += instance.OnAxes;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Positive.started += instance.OnPositive;
                @Positive.performed += instance.OnPositive;
                @Positive.canceled += instance.OnPositive;
                @Negative.started += instance.OnNegative;
                @Negative.performed += instance.OnNegative;
                @Negative.canceled += instance.OnNegative;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    public interface IGamePlayActions
    {
        void OnAxes(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPositive(InputAction.CallbackContext context);
        void OnNegative(InputAction.CallbackContext context);
    }
}
