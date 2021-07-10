using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;


public class RowOperator : MonoBehaviour
{
    [SerializeField] private GameObject row2;
    [SerializeField] private GameObject row3;
    [Header("ObjectsContlor")] public UnityEvent disableEvent;
    [SerializeField] GameObject panel;


    int _numberOfSprites = 3;


    public void TurnOnRow()
    {
        switch (_numberOfSprites)
        {
            case 3:

                row2.SetActive(true);
                _numberOfSprites = 6;
                break;
            case 6:
                row3.SetActive(true);
                _numberOfSprites = 9;
                break;
            case 9:

                panel.GetComponent<Image>().DOFade(+100, 15f);
                disableEvent.Invoke();
                break;
        }
    }
}