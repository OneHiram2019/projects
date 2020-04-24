using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPopupSlot : MonoBehaviour
{
    
    [SerializeField]
    private SelectPopup _selectPopup;
    [SerializeField]
    private GameCore _totalCoinsCount;

    [SerializeField]
    private int _towerPrice;
    [SerializeField]
    private Text _towerPriceText;
    [SerializeField]
    private int _slotId;

    private void Start()
    {
        _towerPriceText.text = _towerPrice.ToString();
    }

    public void OnClick()
    {
       int total = _totalCoinsCount.TotalCoinsCount - _towerPrice;
        if (total >=0)
        {
            _selectPopup.SetTower(_slotId);
            _totalCoinsCount.CoinsCount(_towerPrice * -1);
        }   
    }
}
