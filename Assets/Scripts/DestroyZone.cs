using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    [SerializeField]
    private GameCore _gameCore;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject);
            _gameCore.HealthCount();
        }
    }
}
