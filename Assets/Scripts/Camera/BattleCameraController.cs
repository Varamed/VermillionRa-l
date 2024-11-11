using System.Linq;
using UnityEngine;

public class BattleCameraController : MonoBehaviour
{
    public Transform player;
    public Transform cameraTransform;
    public float transitionSpeed = 2f;
    public float rotationSpeed = 50f;
    public float smoothReturnSpeed = 2f;
    public float upDownMovementSpeed = 0.2f;
    public float upDownAmplitude = 0.1f;

    private Transform[] enemies;
    private int currentEnemyIndex = 0;
    private Transform targetEnemy;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private bool isLookingAtEnemy = false;
    private bool isRotating = false;
    private bool isReturningToOriginal = false;

    private void Start()
    {
        originalCameraPosition = cameraTransform.position;
        originalCameraRotation = cameraTransform.rotation;
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.transform).ToArray();

        if (enemies.Length > 0)
        {
            targetEnemy = enemies[currentEnemyIndex];
            isLookingAtEnemy = true;
            isRotating = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeTarget(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeTarget(1);
        }

        if (isLookingAtEnemy && isRotating && targetEnemy != null)
        {
            RotateCameraTowardsEnemy();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCameraPosition();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isReturningToOriginal = false;
            isRotating = true;
        }

        if (isReturningToOriginal)
        {
            SmoothReturnToOriginalPosition();
        }
        else
        {
            MoveCameraSlightly();
        }
    }

    private void ChangeTarget(int direction)
    {
        if (enemies.Length == 0) return;

        currentEnemyIndex += direction;

        if (currentEnemyIndex < 0)
        {
            currentEnemyIndex = enemies.Length - 1;
        }
        else if (currentEnemyIndex >= enemies.Length)
        {
            currentEnemyIndex = 0;
        }

        targetEnemy = enemies[currentEnemyIndex];
        isLookingAtEnemy = true;
    }

    private void RotateCameraTowardsEnemy()
    {
        Vector3 directionToEnemy = targetEnemy.position - player.position;
        float angle = Mathf.Atan2(directionToEnemy.z, directionToEnemy.x) * Mathf.Rad2Deg;
        cameraTransform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
        cameraTransform.LookAt(targetEnemy);
    }

    private void ResetCameraPosition()
    {
        isReturningToOriginal = true;
        isRotating = false;
    }

    private void SmoothReturnToOriginalPosition()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, originalCameraPosition, smoothReturnSpeed * Time.deltaTime);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, originalCameraRotation, smoothReturnSpeed * Time.deltaTime);

        if (Vector3.Distance(cameraTransform.position, originalCameraPosition) < 0.1f)
        {
            cameraTransform.position = originalCameraPosition;
            cameraTransform.rotation = originalCameraRotation;
            isReturningToOriginal = false;
        }
    }

    private void MoveCameraSlightly()
    {
        float newY = originalCameraPosition.y + Mathf.Sin(Time.time * upDownMovementSpeed) * upDownAmplitude;
        cameraTransform.position = new Vector3(cameraTransform.position.x, newY, cameraTransform.position.z);
    }
}
