using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickmanCollider : MonoBehaviour
{
    public UnityAction<int> OnGetPoints;
    public UnityAction<int> OnCrossFinishBlock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FinishBlock>(out FinishBlock finishBlock))
        {
            OnCrossFinishBlock?.Invoke(finishBlock.BlockNumber);
            finishBlock.LaunchEffects();
        }
    }

    public void GetPoints(int points)=>
        OnGetPoints?.Invoke(points);
}
