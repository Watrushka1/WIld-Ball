using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ����, �� ������� ����� ��������� ������ (�����)
    public Vector3 offset = new Vector3(0, 5, -10); // ��������� �������� ������
    public float smoothSpeed = 0.125f; // �������� ����������� ����������� ������

    public float rotationSpeed = 5f; // �������� �������� ������
    private float currentYaw = 0f; // ������� ���� �������� �� ��� Y (�����������)
    private float currentPitch = 20f; // ������� ���� �������� �� ��� X (���������)

    public float minPitch = -30f; // ����������� ���� �������� �� ��� X
    public float maxPitch = 60f; // ������������ ���� �������� �� ��� X

    public float zoomSpeed = 2f; // �������� ����
    public float minZoom = 5f; // ����������� ���������� ������ �� ����
    public float maxZoom = 15f; // ������������ ���������� ������ �� ����
    private float currentZoom = 10f; // ������� ���������� ������ �� ����

    public LayerMask obstructionMask; // ����(�) �����������, ������� ����� ����������� ������

    void Start()
    {
        // ������������� ������� ���������� ������ �� ������ ���������� ��������
        currentZoom = offset.magnitude;

        // �������������� ��������� ���� �������� �� ������ ���������� ��������
        Vector3 direction = offset.normalized;
        currentPitch = Mathf.Asin(direction.y) * Mathf.Rad2Deg;
        currentYaw = Mathf.Atan2(direction.x, -direction.z) * Mathf.Rad2Deg;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        // ��������� ���� � ������� �������� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // �������� ������ ��� ����������� ������ ������ ����
        if (Input.GetMouseButton(1)) // ������ ������ ���� ������������
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            currentYaw += mouseInputX * rotationSpeed;
            currentPitch -= mouseInputY * rotationSpeed; // ����������� ���������� �� ���������, ��� ������������� ����� ������ �����

            // ������������ ���� �������� �� ���������
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        }

        // ������������ �������� ������� ������ � ������ �������� � ����
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0, 0, -currentZoom);

        // ��������� ������� ����������� ����� ����� � �������� �������� ������
        RaycastHit hit;
        Vector3 directionToCamera = desiredPosition - target.position;
        float distance = currentZoom;

        if (Physics.Raycast(target.position, directionToCamera.normalized, out hit, currentZoom, obstructionMask))
        {
            // ���� ���������� �����������, ������������ ������� ������ ����� ������������
            distance = hit.distance - 0.5f; // ��������� ������� �� �����������
            distance = Mathf.Clamp(distance, minZoom, maxZoom);
            desiredPosition = target.position + rotation * new Vector3(0, 0, -distance);
        }

        // ������� ����������� ������ � �������� �������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // ������������� ������� ������, ����� ��� �������� �� ����
        transform.LookAt(target.position);
    }
}
