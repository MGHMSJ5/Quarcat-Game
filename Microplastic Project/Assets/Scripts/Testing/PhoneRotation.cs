using UnityEngine;
using UnityEngine.UI;

public class PhoneRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Image _joystick;

    [Header("Portrait")]
    [SerializeField]
    private Vector3 _portraitPosition;
    [SerializeField]
    private float _portraitSize;

    [Header("Landscape")]
    [SerializeField]
    private Vector3 _landscapePosition;
    [SerializeField]
    private float _landscapeSize;

    private Vector2 portrair5 = new Vector2(0.5f, 0);

    // Update is called once per frame
    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown || Input.GetKeyDown(KeyCode.P))
        {
            _camera.orthographicSize = _portraitSize;

            //_joystick.rectTransform.anchorMin = portrair5;
            //_joystick.rectTransform.anchorMax = portrair5;
            //_joystick.rectTransform.anchoredPosition = _portraitPosition;
        }

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight || Input.GetKeyDown(KeyCode.L))
        {
            _camera.orthographicSize = _landscapeSize;

            //_joystick.rectTransform.anchorMin = Vector2.zero;
            //_joystick.rectTransform.anchorMax = Vector2.zero;
            //_joystick.rectTransform.anchoredPosition = _landscapePosition;
        }
    }

}
