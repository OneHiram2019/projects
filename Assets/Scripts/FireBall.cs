using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    private float _speed;
    private int _damage { get; set; }
    public int Damage => _damage;


    public void Push(Vector3 direction, int power, float speed)
    {
        _speed = speed;
        _rb.AddForce(direction* _speed);
        _damage = power;
    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
