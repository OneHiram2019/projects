using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPopup : MonoBehaviour
{
    [SerializeField]
    private GameCore _gameCore;
    private Vector3 _towerPointPosition;
    private TowerPoint _towerPoint;


    public void SetTower(int id)
    {
        _gameCore.InstantTower(_towerPointPosition, id);
        _towerPoint.NotEmpty();
        OnCloseClick();
    }

    public void SetTowerPointPosition(Vector3 pos, TowerPoint towerPoint)
    {
        _towerPointPosition = pos;
        _towerPoint = towerPoint;
    }

    public void OnCloseClick()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
