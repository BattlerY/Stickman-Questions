using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddlesBook : MonoBehaviour
{
    [SerializeField] private Riddle[] _riddles;

    public Riddle GetRiddle(int number)
    {
        if (_riddles.Length > number)
            return _riddles[number];

        return _riddles[Random.Range(0, _riddles.Length)];
    }
}

[System.Serializable]
public class Riddle
{
   [SerializeField] private string _rightAnswer;
   [SerializeField] private string _wrongAnswer;
   [SerializeField] private Sprite _rebus;
   [SerializeField] private int _points;

    public string RightAnswer => _rightAnswer;
    public string WrongAnswer => _wrongAnswer;
    public Sprite Rebus => _rebus;
    public int Points => _points;
}