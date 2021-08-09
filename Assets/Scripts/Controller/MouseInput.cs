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
                },
                {
                    ""name"": ""Camera2DMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3c4a203f-1e64-4f61-a892-ccd2b7e74e14"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""CameraHeightMovement"",
                    ""type"": ""Value"",
                    ""id"": ""2d8fe319-8cb4-40df-b041-e55218098856"",
                    ""expectedControlType"": ""Axis"",
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
                },
                {
                    ""name"": ""Scroll"",
                    ""id"": ""e85a3790-ea80-43ee-ac8e-2f1e214f98fb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraHeightMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""36f274e5-b2d0-4bd7-988b-ee853d047b5f"",
                    ""path"": ""<Mouse>/scroll/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""CameraHeightMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""149dd9cb-0b70-49da-a161-a7fc8b3c1b48"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""CameraHeightMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""e2771c1f-2068-412d-8365-b83d19a5b981"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7cdbca18-2188-45f4-9faf-898627bfb56f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b1650cb2-ff4d-448d-97a0-669b68bb8799"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2c746c7c-e05b-45a2-8b56-384ef0eec125"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a86b1ad9-2299-48de-ac28-b0bfc6e0fb59"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
        m_GamePlay_Camera2DMovement = m_GamePlay.FindAction("Camera2DMovement", throwIfNotFound: true);
        m_GamePlay_CameraHeightMovement = m_GamePlay.FindAction("CameraHeightMovement", throwIfNotFound: true);
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
    private readonly InputAction m_GamePlay_Camera2DMovement;
    private readonly InputAction m_GamePlay_CameraHeightMovement;
    public struct GamePlayActions
    {
        private @MouseInput m_Wrapper;
        public GamePlayActions(@MouseInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseClick => m_Wrapper.m_GamePlay_MouseClick;
        public InputAction @MousePosition => m_Wrapper.m_GamePlay_MousePosition;
        public InputAction @Camera2DMovement => m_Wrapper.m_GamePlay_Camera2DMovement;
        public InputAction @CameraHeightMovement => m_Wrapper.m_GamePlay_CameraHeightMovement;
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
                @Camera2DMovement.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamera2DMovement;
                @Camera2DMovement.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamera2DMovement;
                @Camera2DMovement.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamera2DMovement;
                @CameraHeightMovement.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraHeightMovement;
                @CameraHeightMovement.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraHeightMovement;
                @CameraHeightMovement.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraHeightMovement;
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
                @Camera2DMovement.started += instance.OnCamera2DMovement;
                @Camera2DMovement.performed += instance.OnCamera2DMovement;
                @Camera2DMovement.canceled += instance.OnCamera2DMovement;
                @CameraHeightMovement.started += instance.OnCameraHeightMovement;
                @CameraHeightMovement.performed += instance.OnCameraHeightMovement;
                @CameraHeightMovement.canceled += instance.OnCameraHeightMovement;
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
        void OnCamera2DMovement(InputAction.CallbackContext context);
        void OnCameraHeightMovement(InputAction.CallbackContext context);
    }
}
