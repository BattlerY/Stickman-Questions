using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private Image _rebusImage;
    [SerializeField] private Passage _rightPassage;
    [SerializeField] private Passage _leftPassage;
    [SerializeField] private Transform _answerPoint;
    [SerializeField] private float _fallVelocity;

    [HideInInspector][SerializeField] private AnswerEffect _answerEffect;
    [HideInInspector][SerializeField] private Riddle _riddle;

    private bool _canMove;

    private void OnEnable()
    {
        _rightPassage.OnAnswered += GetAnswer;
        _leftPassage.OnAnswered += GetAnswer;
    }

    private void OnDisable()
    {
        _rightPassage.OnAnswered -= GetAnswer;
        _leftPassage.OnAnswered -= GetAnswer;
    }

    private void Update()
    {
        if (_canMove)
            Move();
    }

    public void SetRiddle(Riddle riddle)
    {
        _riddle = riddle;
        _rebusImage.sprite = _riddle.Rebus;

        Passage truePassage = _rightPassage;
        Passage wrongPassage = _leftPassage;

        int rand = Random.Range(0, 2);

        if (rand == 0)
            (truePassage, wrongPassage) = (wrongPassage, truePassage);

        truePassage.Initiait(_riddle.RightAnswer, true, _riddle.Points);
        wrongPassage.Initiait(_riddle.WrongAnswer, false, -_riddle.Points);

        _answerEffect = FindObjectOfType<AnswerEffect>();
    }

    private void GetAnswer(bool answer)
    {
        _canMove = true;
        _answerEffect.LaunchEffect(answer, _answerPoint);
    }

    private void Move()
    {
        Vector3 offset = Vector3.down * _fallVelocity * Time.deltaTime;
        transform.Translate(offset, Space.World);
    }
}
