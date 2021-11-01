// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controller/InputController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""2e93325e-c8c7-446c-a8e4-6c84c4a93f84"",
            ""actions"": [
                {
                    ""name"": ""OnMouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""aed84300-f12c-457e-a754-cecae2f792b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OnMouseMove"",
                    ""type"": ""Value"",
                    ""id"": ""456d8563-311e-4e91-b022-e628adeccfbb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera2DMovement"",
                    ""type"": ""Value"",
                    ""id"": ""d0fa2965-f9de-40b5-8261-913c6c45dddc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraScrollMovement"",
                    ""type"": ""Value"",
                    ""id"": ""2d8fe319-8cb4-40df-b041-e55218098856"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OnMouseRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""e53fc801-6b90-4e4a-a6ca-f66783df53ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnterTestMode"",
                    ""type"": ""Button"",
                    ""id"": ""a5103704-18cc-4310-8989-62e72e40b167"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InteractionKey"",
                    ""type"": ""Button"",
                    ""id"": ""35348bc3-c305-4ec7-a085-6d97946b4180"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayerInfoKey"",
                    ""type"": ""Button"",
                    ""id"": ""44d333dd-3023-4505-8dd8-b6a55a700a32"",
                    ""expectedControlType"": ""Button"",
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
                    ""action"": ""OnMouseClick"",
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
                    ""action"": ""OnMouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bac882d8-ef21-4c20-904c-8453c37ae1c7"",
                    ""path"": ""Mouse/Scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""CameraScrollMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""84b4e9e0-e59c-4aaa-97fa-02377e59ea75"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""177a0ac8-09f6-4b79-919b-48f89e3b2128"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""955f1d59-4032-46dc-8309-1cebbeab9944"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cf4a9833-35a9-4d61-a19d-459ef52c83a7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""37176626-4aea-462e-ae8c-5158d297b1ff"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""Camera2DMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2314b452-9362-46a9-9307-190a0e70e58d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""OnMouseRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With Two Modifiers"",
                    ""id"": ""2fd904c6-aa60-4ee1-b9cf-a4f44b1ac4d4"",
                    ""path"": ""ButtonWithTwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnterTestMode"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier1"",
                    ""id"": ""d2344dbb-1eed-4b69-90cf-5e7c385bc577"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""EnterTestMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier2"",
                    ""id"": ""79c76b0d-01c9-4f5e-80e8-a6c09fda5c81"",
                    ""path"": ""<Keyboard>/period"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""EnterTestMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""20fb399a-ec96-49a5-a903-3ff751476a77"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""EnterTestMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""188cf16e-19e3-4c65-9d2d-b4bb2d490761"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""PlayerInfoKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56108822-0576-4ec5-9084-235241395b0d"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse+KeyBoard"",
                    ""action"": ""InteractionKey"",
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
        m_GamePlay_OnMouseClick = m_GamePlay.FindAction("OnMouseClick", throwIfNotFound: true);
        m_GamePlay_OnMouseMove = m_GamePlay.FindAction("OnMouseMove", throwIfNotFound: true);
        m_GamePlay_Camera2DMovement = m_GamePlay.FindAction("Camera2DMovement", throwIfNotFound: true);
        m_GamePlay_CameraScrollMovement = m_GamePlay.FindAction("CameraScrollMovement", throwIfNotFound: true);
        m_GamePlay_OnMouseRightClick = m_GamePlay.FindAction("OnMouseRightClick", throwIfNotFound: true);
        m_GamePlay_EnterTestMode = m_GamePlay.FindAction("EnterTestMode", throwIfNotFound: true);
        m_GamePlay_InteractionKey = m_GamePlay.FindAction("InteractionKey", throwIfNotFound: true);
        m_GamePlay_PlayerInfoKey = m_GamePlay.FindAction("PlayerInfoKey", throwIfNotFound: true);
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
    private readonly InputAction m_GamePlay_OnMouseClick;
    private readonly InputAction m_GamePlay_OnMouseMove;
    private readonly InputAction m_GamePlay_Camera2DMovement;
    private readonly InputAction m_GamePlay_CameraScrollMovement;
    private readonly InputAction m_GamePlay_OnMouseRightClick;
    private readonly InputAction m_GamePlay_EnterTestMode;
    private readonly InputAction m_GamePlay_InteractionKey;
    private readonly InputAction m_GamePlay_PlayerInfoKey;
    public struct GamePlayActions
    {
        private @InputController m_Wrapper;
        public GamePlayActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @OnMouseClick => m_Wrapper.m_GamePlay_OnMouseClick;
        public InputAction @OnMouseMove => m_Wrapper.m_GamePlay_OnMouseMove;
        public InputAction @Camera2DMovement => m_Wrapper.m_GamePlay_Camera2DMovement;
        public InputAction @CameraScrollMovement => m_Wrapper.m_GamePlay_CameraScrollMovement;
        public InputAction @OnMouseRightClick => m_Wrapper.m_GamePlay_OnMouseRightClick;
        public InputAction @EnterTestMode => m_Wrapper.m_GamePlay_EnterTestMode;
        public InputAction @InteractionKey => m_Wrapper.m_GamePlay_InteractionKey;
        public InputAction @PlayerInfoKey => m_Wrapper.m_GamePlay_PlayerInfoKey;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @OnMouseClick.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseClick;
                @OnMouseClick.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseClick;
                @OnMouseClick.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseClick;
                @OnMouseMove.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseMove;
                @OnMouseMove.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseMove;
                @OnMouseMove.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseMove;
                @Camera2DMovement.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamera2DMovement;
                @Camera2DMovement.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamera2DMovement;
                @Camera2DMovement.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCamera2DMovement;
                @CameraScrollMovement.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraScrollMovement;
                @CameraScrollMovement.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraScrollMovement;
                @CameraScrollMovement.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnCameraScrollMovement;
                @OnMouseRightClick.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseRightClick;
                @OnMouseRightClick.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseRightClick;
                @OnMouseRightClick.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnOnMouseRightClick;
                @EnterTestMode.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEnterTestMode;
                @EnterTestMode.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEnterTestMode;
                @EnterTestMode.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEnterTestMode;
                @InteractionKey.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteractionKey;
                @InteractionKey.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteractionKey;
                @InteractionKey.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnInteractionKey;
                @PlayerInfoKey.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPlayerInfoKey;
                @PlayerInfoKey.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPlayerInfoKey;
                @PlayerInfoKey.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPlayerInfoKey;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OnMouseClick.started += instance.OnOnMouseClick;
                @OnMouseClick.performed += instance.OnOnMouseClick;
                @OnMouseClick.canceled += instance.OnOnMouseClick;
                @OnMouseMove.started += instance.OnOnMouseMove;
                @OnMouseMove.performed += instance.OnOnMouseMove;
                @OnMouseMove.canceled += instance.OnOnMouseMove;
                @Camera2DMovement.started += instance.OnCamera2DMovement;
                @Camera2DMovement.performed += instance.OnCamera2DMovement;
                @Camera2DMovement.canceled += instance.OnCamera2DMovement;
                @CameraScrollMovement.started += instance.OnCameraScrollMovement;
                @CameraScrollMovement.performed += instance.OnCameraScrollMovement;
                @CameraScrollMovement.canceled += instance.OnCameraScrollMovement;
                @OnMouseRightClick.started += instance.OnOnMouseRightClick;
                @OnMouseRightClick.performed += instance.OnOnMouseRightClick;
                @OnMouseRightClick.canceled += instance.OnOnMouseRightClick;
                @EnterTestMode.started += instance.OnEnterTestMode;
                @EnterTestMode.performed += instance.OnEnterTestMode;
                @EnterTestMode.canceled += instance.OnEnterTestMode;
                @InteractionKey.started += instance.OnInteractionKey;
                @InteractionKey.performed += instance.OnInteractionKey;
                @InteractionKey.canceled += instance.OnInteractionKey;
                @PlayerInfoKey.started += instance.OnPlayerInfoKey;
                @PlayerInfoKey.performed += instance.OnPlayerInfoKey;
                @PlayerInfoKey.canceled += instance.OnPlayerInfoKey;
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
        void OnOnMouseClick(InputAction.CallbackContext context);
        void OnOnMouseMove(InputAction.CallbackContext context);
        void OnCamera2DMovement(InputAction.CallbackContext context);
        void OnCameraScrollMovement(InputAction.CallbackContext context);
        void OnOnMouseRightClick(InputAction.CallbackContext context);
        void OnEnterTestMode(InputAction.CallbackContext context);
        void OnInteractionKey(InputAction.CallbackContext context);
        void OnPlayerInfoKey(InputAction.CallbackContext context);
    }
}
