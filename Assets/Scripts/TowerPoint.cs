using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    [SerializeField]
    private SelectPopup _selectPopup;
    [SerializeField]
    private Transform _targetPos;

    private bool _empty = true;
    private GameState _gameState;

    private void OnMouseUp()
    {
       
        if (_gameState == GameState.Game && _empty)
        {
            Time.timeScale = 0;
            _selectPopup.gameObject.SetActive(true);
            _selectPopup.SetTowerPointPosition(_targetPos.position, this);
        }
    }
    public void NotEmpty()
    {
       _empty = false;
    }
    public void IsEmpty()
    {
        _empty = true;
    }
    public void StateGame(GameState state)
    {
        _gameState = state;
    }
}
