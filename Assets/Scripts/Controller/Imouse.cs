using UnityEngine.InputSystem;

public interface Imouse
{
    public void Init();
    public void OnMouseDown(InputAction.CallbackContext context);
    public void OnMouseUp(InputAction.CallbackContext context);
    public void OnDrag(InputAction.CallbackContext context);
    public void OnMouseMove(InputAction.CallbackContext context);
    public void OnMouseHover(InputAction.CallbackContext context);
}
