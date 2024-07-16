using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _rotationSpeed = 5;
    [SerializeField] private FixedJoystick fixJS;
    [SerializeField] private GameObject _camera;

    private float _cameraSpeedMultiplier = 30.2f; // Множитель скорости камеры
    private float _cameraReturnSpeed = 5.0f; // Скорость возвращения камеры к самолету
    private float _joystickReleaseThreshold = 0.1f; // Порог для отпускания джойстика
    private float _returnTolerance = 0.1f; // Погрешность для возвращения камеры
    private float _maxDistance = 4.0f; // Максимальное расстояние между камерой и самолетом
    private float _desiredDistance = 3.0f; // Желаемая дистанция между камерой и самолетом

    private Vector2 _lastDirection = Vector2.zero; // Последнее направление движения

    public void FixedUpdate()
    {
        Vector2 direction = Vector2.up * fixJS.Vertical + Vector2.right * fixJS.Horizontal;

        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();

        transform.Translate(direction * _speed * inputMagnitude * Time.deltaTime, Space.World);

        Vector3 cameraToPlane = transform.position - _camera.transform.position;
        cameraToPlane.z = 0; // Поддержание позиции по оси z

        if (inputMagnitude > _joystickReleaseThreshold)
        {
            if (cameraToPlane.magnitude < _maxDistance)
            {
                // Движение камеры немного быстрее, чем самолета
                Vector3 targetPosition = transform.position + (Vector3)(direction * _speed * _cameraSpeedMultiplier * inputMagnitude * Time.deltaTime);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);
            }
            else
            {
                if (direction != _lastDirection)
                {
                    // Камера плавно двигается за самолетом, независимо от расстояния
                    Vector3 targetPosition = transform.position + (Vector3)(direction * _speed * inputMagnitude * Time.deltaTime);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);

                    // После изменения направления корректирует скорость, чтобы вернуться к дистанции 2.0f
                    Vector3 desiredPosition = transform.position - (cameraToPlane.normalized * _desiredDistance);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, desiredPosition, 0.1f);
                }
                else
                {
                    // Камера движется с той же скоростью, что и самолет
                    Vector3 targetPosition = transform.position + (Vector3)(direction * _speed * inputMagnitude * Time.deltaTime);
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, 0.1f);
                }
            }
        }
        else
        {
            // Возвращение камеры к самолету при отпускании джойстика
            if (cameraToPlane.magnitude > _returnTolerance)
            {
                Vector3 desiredPosition = transform.position - new Vector3(0, 0, 10);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, desiredPosition, _cameraReturnSpeed * Time.deltaTime);
            }
        }

        // Поддержание позиции камеры по оси z на уровне -10
        Vector3 cameraPosition = _camera.transform.position;
        cameraPosition.z = -10;
        _camera.transform.position = cameraPosition;

        if (direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed);
        }

        // Обновление последнего направления движения
        _lastDirection = direction;
    }
}
