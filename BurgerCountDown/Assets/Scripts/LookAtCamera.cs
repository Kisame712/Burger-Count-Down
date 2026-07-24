using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private GameObject activeCamera;
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(activeCamera.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - activeCamera.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = activeCamera.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -activeCamera.transform.forward;
                break;
        }
    }

    public void SetAcitveCamera(GameObject activeCamera)
    {
        this.activeCamera = activeCamera;
    }
}
