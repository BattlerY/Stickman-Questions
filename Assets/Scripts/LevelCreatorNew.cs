using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorNew : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private Gate _gatePrefab;
    [SerializeField] private FinishBlock _finishBlock;

    [Header("ScneObj")]
    [SerializeField] private Stickman _stickman;
    [SerializeField] private Canvas _worldCnavas;
    [SerializeField] private RiddlesBook _riddlesBook;

    [Header("Variables")]
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private int _blocksCount;
    [SerializeField] private int _gateCount;
    [SerializeField] private int _gateStartOffset;
    [SerializeField] private int _gateEndOffset;
    [SerializeField] private int _stickmanStartOffset;
    [SerializeField] private int _finishBlockCount;
    [SerializeField] private int _finishBlockLength;
    [SerializeField] private Color[] _finishBlockPalette;
    [SerializeField] private Material _spesialFinishBlockMat;

    [SerializeField] [HideInInspector] private List<Block> _blocks;
    [SerializeField] [HideInInspector] private List<Gate> _gates;
    [SerializeField] [HideInInspector] private List<FinishBlock> _finishBlocks;

    private const int _blocksInLine = 9;
    private Vector3 _curentPos;

    public float Width => _blockPrefab.EdgeLength * _blocksInLine;
    public float Length => _blockPrefab.EdgeLength * _blocksCount / _blocksInLine;

    private void OnValidate()
    {
        if (_blocksCount % _blocksInLine != 0)
            _blocksCount = _blocksCount / _blocksInLine * _blocksInLine;
    }

    [ContextMenu("Create")]
    private void Create()
    {
        CreateBlocks();
        CreateGates();
        CreateFinishPanel();
    }

    [ContextMenu("AjustStickamm")]
    private void AjustStickamm()
    {
        Vector3 newPosition = new Vector3((int)Width / 2, _startPos.y + _blockPrefab.EdgeLength / 2f, _startPos.z+ _stickmanStartOffset);
        _stickman.Initiait(Width, newPosition);
    }

    private void CreateBlocks()
    {
        if (_blocks == null)
            _blocks = new List<Block>();

        if (_blocks.Count > 0)
            DestroyBlocks();

        Block newBlock = null;
        _curentPos = _startPos;

        for (int i = 1; i <= _blocksCount; i++)
        {
            newBlock = Instantiate(_blockPrefab, _curentPos, Quaternion.identity, this.transform);
            _blocks.Add(newBlock);

            if (i % _blocksInLine == 0)
            {
                _curentPos.x = _startPos.x;
                _curentPos.z += _blockPrefab.EdgeLength;
            }
            else
            {
                _curentPos.x += _blockPrefab.EdgeLength;
            }  
        }
    }

    private void CreateGates()
    {
        Vector3 position = new Vector3();
        position.x = -_blockPrefab.HalfEdgeLength;
        position.y = _blockPrefab.HalfEdgeLength;

        float startZ = _startPos.z + _gateStartOffset;
        float endZ = _startPos.z + Length - _gateEndOffset;
        float lenght = endZ - startZ;
        float stepZ = lenght / (_gateCount-1);

        if (_gates == null)
            _gates = new List<Gate>();

        if (_gates.Count > 0)
            DestroyGates();

        Gate _curentGate;

        for (int i = 0; i < _gateCount; i++)
        {
            position.z = startZ + stepZ * i;
            _curentGate = Instantiate(_gatePrefab, position, Quaternion.identity, _worldCnavas.transform);
            _curentGate.SetRiddle(_riddlesBook.GetRiddle(i));
            _gates.Add(_curentGate);
        }
    }

    private void CreateFinishPanel()
    {
        if (_finishBlocks == null)
            _finishBlocks = new List<FinishBlock>();

        if (_finishBlocks.Count > 0)
            DestroyFinishBlocks();

        Vector3 newPos = new Vector3((int)Width / 2, _startPos.x,  Length- _blockPrefab.HalfEdgeLength + _finishBlockLength/2f);
        FinishBlock newBlock;
        Color color;

        for (int i = -1; i < _finishBlockCount; i++)
        {
            newBlock = Instantiate(_finishBlock, newPos, Quaternion.identity, _worldCnavas.transform);
            _finishBlocks.Add(newBlock);

            if (i == -1)
            {
                newBlock.Initiate(_spesialFinishBlockMat, Width, _finishBlockLength);
            }
            else
            {
                color = _finishBlockPalette.Length >= i ? _finishBlockPalette[i] : Color.white;
                newBlock.Initiate(color, Width, _finishBlockLength, i + 1);
            }

            newPos.z += newBlock.Length;
        }
    }

    [ContextMenu("DestroyBlocks")]
    private void DestroyBlocks()
    {
        for (int i = _blocks.Count - 1; i >= 0; i--)
        {
            if(_blocks[i] !=null && _blocks[i].gameObject!=null)
                DestroyImmediate(_blocks[i].gameObject);
            _blocks.RemoveAt(i);
        }
    }

    [ContextMenu("DestroyGates")]
    private void DestroyGates()
    {
        for (int i = _gates.Count - 1; i >= 0; i--)
        {
            if (_gates[i] != null && _gates[i].gameObject != null)
                DestroyImmediate(_gates[i].gameObject);
            _gates.RemoveAt(i);
        }
    }

    [ContextMenu("DestroyFinishBlocks")]
    private void DestroyFinishBlocks()
    {
        for (int i = _finishBlocks.Count - 1; i >= 0; i--)
        {
            if (_finishBlocks[i] != null && _finishBlocks[i].gameObject != null)
                DestroyImmediate(_finishBlocks[i].gameObject);
            _finishBlocks.RemoveAt(i);
        }
    }
}

