using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;


public class GameCore : MonoBehaviour
{
    [Header("LevelsControl")] public UnityEvent levelEvent;

    public List<Sprite> _sprites = new List<Sprite>();
    private readonly List<string> _namesOfObjects = new List<string>();
    [SerializeField] private Text textFind;
    [SerializeField] private List<Config> configs;
    [SerializeField] private GameObject panel;
     private Config _config;
    
    
    private Cell[] _cells;
    private int _stringIndex;
    private int _spriteIndex;

    private void Start()
    {
        panel.GetComponent<Image>().DOFade(-20,50f);
        
        
        _config = configs[GenerateIndex(2)];
        textFind.DOFade(100f, 200f);

        TakeConfigData();
        SetText();
        SetUpCells();
    }


    private void FindCells()
    {
        _cells = (FindObjectsOfType<Cell>());
    }

    private void TakeConfigData()
    {
        _namesOfObjects.Clear();
        _sprites.Clear();
        _namesOfObjects.AddRange(_config.GetNames());
        _sprites.AddRange(_config.GetSprites());
    }

    private void SetText()
    {
        _stringIndex = Random.Range(0, _namesOfObjects.Count);
        textFind.text = $"Find {_namesOfObjects[_stringIndex]}";
    }

    private void SetUpCells()
    {
        var isExistRightAnswer = false;
        var spritesIndexes = new List<int>();
        FindCells();

        foreach (var cell in _cells)
        {
            do
            {
                _spriteIndex = GenerateIndex(_sprites.Count);
            } while (spritesIndexes.Contains(_spriteIndex));

            spritesIndexes.Add(_spriteIndex);
            cell.SetSprite(_sprites[_spriteIndex]);
        }


        foreach (var cell in _cells)
        {
            if (cell.GetSprite().name == _namesOfObjects[_stringIndex])

            {
                isExistRightAnswer = true;
            }
        }

        if (isExistRightAnswer == false)
        {
            _cells[GenerateIndex(_cells.Length)].SetSprite(_sprites[_stringIndex]);
        }
    }

    private int GenerateIndex(int maxInt)
    {
        var index = Random.Range(0, maxInt);
        return index;
    }


    public void CheckMatched(Cell cell)
    {
        if (cell.GetSprite().name == _namesOfObjects[_stringIndex])
        {
            PlayVFX(cell);
            cell.transform.DOShakePosition(1.0f, strength: new Vector3(0, 1, 0), vibrato: 1, randomness: 0,
                snapping: false, fadeOut: true);
            _namesOfObjects.Remove(_namesOfObjects[_stringIndex]);
            _sprites.Remove(cell.GetSprite());
            SetText();
            if (_sprites.Count == _cells.Length)
            {
                TakeConfigData();
                SetText();
                levelEvent.Invoke();
                SetUpCells();
            }
            else
            {
                SetUpCells();
            }
        }
        else
        {
            cell.transform.DOShakePosition(2.0f, strength: new Vector3(2, 0, 0), vibrato: 3, randomness: 1,
                snapping: false, fadeOut: true);
        }
    }

    public void PlayVFX(Cell cell)
    {
        cell.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {
    }
}