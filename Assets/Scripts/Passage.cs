using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Passage : MonoBehaviour
{
    [HideInInspector] [SerializeField] bool _isRight;
    [HideInInspector] [SerializeField] private int _points;
    [SerializeField] private TextMeshProUGUI _answer;
   
    public UnityAction<bool> OnAnswered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StickmanCollider>(out StickmanCollider stickman))
        {
            OnAnswered?.Invoke(_isRight);
            stickman.GetPoints(_points);
        }
    }

    public void Initiait(string answer, bool isRight, int points)
    {
        _answer.text = answer;
        _isRight = isRight;
        _points = points;
    }
}

