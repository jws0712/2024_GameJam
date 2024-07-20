using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("CameraSetting")]
    [SerializeField] private Transform targetTransform = null;
    [SerializeField] private float followSpeed = default;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, followSpeed * Time.deltaTime);
    }
}
