using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickman : MonoBehaviour
{
    [Header("Children")]
    [SerializeField] private SideMover _sideMover;
    [SerializeField] private StraightMover _straightMover;
    [SerializeField] private BreakingWall _breakingWall;
    [SerializeField] private PainterWall _painterWall;
    [SerializeField] private DeactivateWall _deactivateWall;
    [SerializeField] private StickmanCollider _stickmanCollider;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _body;

    [Header("Variables")]
    [SerializeField] private float _xCameraAngleUp;
    [SerializeField] private float _yCameraAngleUp;
    [SerializeField] private float _cameraChangeTime;

    public float Width => transform.localScale.x;
    public int Score { get; private set;}

    private void Awake()=>
        Camera.main.transform.parent = _body.transform;

    private void OnEnable()
    {
        _stickmanCollider.OnGetPoints += GetPoints;
        _stickmanCollider.OnCrossFinishBlock += CrossFinishBlock;
    }

    private void OnDisable()
    {
        _stickmanCollider.OnGetPoints -= GetPoints;
        _stickmanCollider.OnCrossFinishBlock -= CrossFinishBlock;
    }

    public void Initiait(float width, Vector3 position)
    {
        transform.position = position;
        _breakingWall.SetSize(width);
        _painterWall.SetSize(width);
    }

    private void GetPoints(int points)
    {
        if(points>0)
            Score += points;

        _breakingWall.Move(Width * -points);
    }

    private void CrossFinishBlock(int blockNumber)
    {
        if (blockNumber == 0)
        {
            StartCoroutine(MoveSmoothly());
            _deactivateWall.gameObject.SetActive(false);
        }

        if (blockNumber == Score)
        {
            _straightMover.enabled = false;
            _sideMover.enabled = false;
            _animator.Play(StickmanAnimations.Idle);
        }
    }

    private IEnumerator MoveSmoothly()
    {
        float passedTime = 0;

        float start = Camera.main.transform.localPosition.y;
        float finish = Camera.main.transform.localPosition.y + _yCameraAngleUp;

        Vector3 newPosition = Camera.main.transform.localPosition;

        while (passedTime < _cameraChangeTime)
        {
            passedTime += Time.deltaTime;

            newPosition.y = Mathf.Lerp(start, finish, passedTime / _cameraChangeTime);
            Camera.main.transform.localPosition = newPosition;

            Camera.main.transform.Rotate(new Vector3(_xCameraAngleUp * Time.deltaTime / _cameraChangeTime, 0, 0));

            yield return null;
        }
    }
}
