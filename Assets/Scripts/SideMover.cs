using UnityEngine;

public class SideMover : MonoBehaviour
{
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _rotateSpeed; 
    [SerializeField] private float _rerotateSpeed;
    [SerializeField] private float _angleLimit;
    [SerializeField] private float _borderLimit;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _rotateTarget;

    public float MinAngle => 360 - _angleLimit;
    public float MaxAngle => _angleLimit;
    public float RightBorder =>   _borderLimit;
    public float LeftBorder =>  - _borderLimit;
    public bool IsOnRight => _rotateTarget.localEulerAngles.y > 0 && _rotateTarget.localEulerAngles.y < 180;

    private void Update()
    {
        if (_joystick.Horizontal != 0)
        {
            MoveToSide();
            Rotate();
        }
        else if(_rotateTarget.localEulerAngles.y != 0)
        {
            Rerotate();
        }
    }

    private void Rerotate()
    {
        Vector3 newAngles = _rotateTarget.localEulerAngles;

        if(IsOnRight)
            newAngles.y = Mathf.Clamp(newAngles.y - _rerotateSpeed * Time.deltaTime, 0, MaxAngle);
        else
            newAngles.y = Mathf.Clamp(newAngles.y + _rerotateSpeed * Time.deltaTime, MinAngle, 360);

        _rotateTarget.localEulerAngles = newAngles;
    }

    private void Rotate()
    {
         Vector3 newAngles = _rotateTarget.localEulerAngles;
         newAngles.y += _rotateSpeed * Time.deltaTime * _joystick.Horizontal;

         if (newAngles.y > MaxAngle && newAngles.y < MinAngle)
            newAngles.y = _joystick.Horizontal > 0 ? MaxAngle : MinAngle;
        
         _rotateTarget.transform.localEulerAngles = newAngles;
    }

    private void MoveToSide()
    {
       if(transform.localPosition.x <= RightBorder && transform.localPosition.x >= LeftBorder)
       {
           float offset = _joystick.Horizontal * _sideSpeed * Time.deltaTime;
           offset = Mathf.Clamp(offset, LeftBorder - transform.localPosition.x, RightBorder - transform.localPosition.x);
           transform.Translate(Vector3.right * offset, Space.World);
       }

       // transform.Translate(Vector3.right * _joystick.Horizontal * _sideSpeed * Time.deltaTime, Space.World);
       //
       // transform.localPosition = Mathf.Clamp()
    }
}
