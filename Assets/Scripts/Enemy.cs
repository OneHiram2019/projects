using DG.Tweening;
using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _enemyHealth;
    [SerializeField]
    private float _enemySpeed;
    [SerializeField]
    private int _enemyPrice;
    [SerializeField]
    private SplineFollower _splineFollower;
    [SerializeField]
    private Animator _animator;

    private Action<int> _get_price;

    public void Init(SplineComputer spline, Action<int> action)
    {
        _splineFollower.spline = spline;
        _get_price = action;
        _splineFollower.followSpeed = _enemySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
           var fireball = collision.GetComponent<FireBall>();

            _enemyHealth -= fireball.Damage;

            if (_enemyHealth <= 0)
            {
                _animator.SetInteger("health",0);
                _get_price?.Invoke(_enemyPrice);
                _splineFollower.followSpeed = 0;
                Destroy(this.GetComponent<CapsuleCollider2D>());
                Destroy(this.gameObject, 1);
                this.GetComponent<SpriteRenderer>().DOFade(0,1f);
               
            }
        }
    }
   
    
}
