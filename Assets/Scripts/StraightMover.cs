using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMover : MonoBehaviour
{
    [SerializeField] private float _straightSpeed;

    private void Update()
    {
        Vector3 offset = Vector3.forward * _straightSpeed * Time.deltaTime;
        transform.Translate(offset, Space.World);
    }
}
