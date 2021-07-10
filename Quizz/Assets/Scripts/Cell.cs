using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class Cell : MonoBehaviour
{
    public GameObject spawnPoint;

    private GameCore _gc;


    void Start()
    {
        transform.DOShakePosition(1.0f, strength: new Vector3(0, 1, 0), vibrato: 2, randomness: 0, snapping: false,
            fadeOut: true);
        _gc = FindObjectOfType<GameCore>();
         
    }


    private void OnMouseDown()
    {
        _gc.CheckMatched(this);
    }

    public Sprite GetSprite()
    {
        return spawnPoint.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetSprite(Sprite sprite)
    {
        spawnPoint.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}