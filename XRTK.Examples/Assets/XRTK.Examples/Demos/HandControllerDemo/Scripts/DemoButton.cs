using UnityEngine;
using UnityEngine.UI;
using XRTK.EventDatum.Input;
using XRTK.Interfaces.InputSystem.Handlers;
using XRTK.SDK.Input.Handlers;

public class DemoButton : BaseInputHandler, IMixedRealityPointerHandler
{
    [SerializeField]
    private Button.ButtonClickedEvent onClick = null;

    [SerializeField]
    private Transform pressableTransform = null;

    [SerializeField]
    private Vector3 pressedOffset = new Vector3(0, 0, .005f);

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        onClick.Invoke();
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        pressableTransform.localPosition = pressedOffset;
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        pressableTransform.localPosition = Vector3.zero;
    }
}
