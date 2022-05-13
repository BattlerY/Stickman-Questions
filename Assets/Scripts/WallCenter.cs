using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallCenter : MonoBehaviour
{
    public UnityAction Triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FinishBlock>(out FinishBlock finishBlock))
            Triggered?.Invoke();
    }
}
