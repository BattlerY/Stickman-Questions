using System.Collections;
using TMPro;
using UnityEngine;

public class AnswerEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wrongAnswer;
    [SerializeField] private TextMeshProUGUI _rightAnswer;
    [SerializeField] private float _time;
    [SerializeField] private float _upVelosity;
    [SerializeField] [Range(0, 1)] private float _fadingLimit;
    [SerializeField] [Range(0, 1)] private float _diminutionLimit;

    private Coroutine _effect;

    public void LaunchEffect(bool isAnswerRight, Transform startPoint)
    {
        TextMeshProUGUI answerText = isAnswerRight ? _rightAnswer : _wrongAnswer;
        answerText.transform.localPosition = startPoint.position;

        if (_effect != null)
            StopCoroutine(_effect);

        _effect = StartCoroutine(ModifyEffect(answerText));
    }

    private IEnumerator ModifyEffect(TextMeshProUGUI effect)
    {
        float passedTime = 0;
        Vector3 newPosition;
        Color newColor;
        float fading;
        float diminution;

        while (passedTime< _time)
        {
            passedTime += Time.deltaTime;

            newPosition = effect.transform.localPosition;
            newPosition.y += _upVelosity * Time.deltaTime;
            effect.transform.localPosition = newPosition;

            fading = Mathf.Lerp(1, _fadingLimit, passedTime / _time);
            newColor = effect.color;
            newColor.a = fading;
            effect.color = newColor;

            diminution = Mathf.Lerp(1, _diminutionLimit, passedTime / _time);
            effect.transform.localScale = new Vector3(diminution, diminution, diminution);

            yield return null;
        }
    }
}
