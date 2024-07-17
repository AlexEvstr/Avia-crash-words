using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _rotationSpeed = 5;
    [SerializeField] private FixedJoystick fixJS;
    [SerializeField] private GameObject _camera;

    private float _cameraSpeedMultiplier = 30.2f;
    private float _cameraReturnSpeed = 5.0f;
    private float _joystickReleaseThreshold = 0.1f;
    private float _returnTolerance = 0.1f;
    private float _maxDistance = 4.0f;
    private float _desiredDistance = 3.0f;

    private Vector2 _lastDirection = Vector2.zero;

    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * fixJS.Vertical + Vector2.right * fixJS.Horizontal;

        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();

        transform.Translate(direction * _speed * inputMagnitude * Time.deltaTime, Space.World);

        Vector3 cameraToPlane = transform.position - _camera.transform.position;
        cameraToPlane.z = 0;

        if (inputMagnitude > _joystickReleaseThreshold)
        {
            if (cameraToPlane.magnitude < _maxDistance)
            {
                Vector3 targetPosition = transform.position + (Vector3)(direction * _speed * _cameraSpeedMultiplier * inputMagnitude * Time.deltaTime);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);
            }
            else
            {
                if (direction != _lastDirection)
                {
                    Vector3 targetPosition = transform.position + (Vector3)(direction * _speed * inputMagnitude * Time.deltaTime);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);

                    Vector3 desiredPosition = transform.position - (cameraToPlane.normalized * _desiredDistance);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, desiredPosition, 0.1f);
                }
                else
                {
                    Vector3 targetPosition = transform.position + (Vector3)(direction * _speed * inputMagnitude * Time.deltaTime);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);
                }
            }
        }
        else
        {
            if (cameraToPlane.magnitude > _returnTolerance)
            {
                Vector3 desiredPosition = transform.position - new Vector3(0, 0, 10);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, desiredPosition, _cameraReturnSpeed * Time.deltaTime);
            }
        }
        Vector3 cameraPosition = _camera.transform.position;
        cameraPosition.z = -10;
        _camera.transform.position = cameraPosition;

        if (direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
        }

        _lastDirection = direction;
    }
}