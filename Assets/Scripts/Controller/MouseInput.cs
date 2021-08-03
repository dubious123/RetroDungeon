// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controller/InputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MouseInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MouseInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""2e93325e-c8c7-446c-a8e4-6c84c4a93f84"",
            ""actions"": [
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""aed84300-f12c-457e-a754-cecae2f792b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""456d8563-311e-4e91-b022-e628adeccfbb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""acf1afc8-7167-4f2d-bf3b-9673e0927b34"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49688e15-ad10-4e5b-854d-5547b807c5a0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse+KeyBoard"",
            ""bindingGroup"": ""Mouse+KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_MouseClick = m_GamePlay.FindAction("MouseClick", throwIfNotFound: true);
        m_GamePlay_MousePosition = m_GamePlay.FindAction("MousePosition", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_MouseClick;
    private readonly InputAction m_GamePlay_MousePosition;
    public struct GamePlayActions
    {
        private @MouseInput m_Wrapper;
        public GamePlayActions(@MouseInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseClick => m_Wrapper.m_GamePlay_MouseClick;
        public InputAction @MousePosition => m_Wrapper.m_GamePlay_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @MouseClick.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMouseClick;
                @MousePosition.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    private int m_MouseKeyBoardSchemeIndex = -1;
    public InputControlScheme MouseKeyBoardScheme
    {
        get
        {
            if (m_MouseKeyBoardSchemeIndex == -1) m_MouseKeyBoardSchemeIndex = asset.FindControlSchemeIndex("Mouse+KeyBoard");
            return asset.controlSchemes[m_MouseKeyBoardSchemeIndex];
        }
    }
    public interface IGamePlayActions
    {
        void OnMouseClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
