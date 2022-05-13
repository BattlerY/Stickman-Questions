using System.Collections;
using UnityEngine;

public class BreakingWall : Wall
{
    [SerializeField] private WallCenter _wallCenter;
    [SerializeField] private float _zLimit;
    [SerializeField] private float _moveTime;

    private float _zPosition;
    private Coroutine _smoothMove;

    private void OnEnable()
    {
        _zPosition = transform.localPosition.z;
        _wallCenter.Triggered += Deactive;
    }

    private void OnDisable()
    {
        _wallCenter.Triggered -= Deactive;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Block>(out Block block))
            block.Fall();
    }

    public void Move(float offset)
    {
        _zPosition = _zPosition + offset < _zLimit ? _zPosition + offset : _zLimit;

        if (_smoothMove != null)
            StopCoroutine(_smoothMove);

        _smoothMove = StartCoroutine(MoveSmoothly());
    }

    private IEnumerator MoveSmoothly()
    {
        float passedTime = 0;

        float start = transform.localPosition.z;
        float finish = _zPosition;

        Vector3 newPosition = transform.localPosition;

        while (passedTime < _moveTime)
        {
            passedTime += Time.deltaTime;
            newPosition.z = Mathf.Lerp(start, finish, passedTime / 2);
            transform.localPosition = newPosition;
            yield return null;
        }
    }

    private void Deactive() =>
        gameObject.SetActive(false);
}

