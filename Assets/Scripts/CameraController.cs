using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Khai báo biến 
    [SerializeField, Range(0, 1)] float _speedCam = 1f;
    [SerializeField] Vector3 _offset;

    Transform _focusCam;
    Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _focusCam = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 tagetPos = _focusCam.position + _offset;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, tagetPos, ref _velocity, _speedCam);
    }
}

