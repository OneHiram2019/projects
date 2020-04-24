using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private int _towerId;
    public int TowerId => _towerId;

    [SerializeField]
    private FireBall _fireballPrefab;
    [SerializeField]
    private int _fireBallPower = 1;
    [SerializeField]
    private float _fireBallSpeed = 1;

    [SerializeField]
    private GameObject _firePoint;
    [SerializeField]
    private int _targetAmount = 1;
    [SerializeField]
    private float _pushFireDelay = 1f;

    private Vector3 _direction;
    private List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        StartCoroutine(PushFire());
    }

    private void Update()
    {
        _enemies.RemoveAll(e=>e==null);
    }
    private IEnumerator PushFire () 
    {
        while (transform)
        {
            for (int i = 0; i < _targetAmount && i < _enemies.Count; i++)
            {
                var direction = _enemies[i].transform.position - _firePoint.transform.position;
                SetTarget(direction);
                var fireball = Instantiate(_fireballPrefab, _firePoint.transform.position, Quaternion.identity);
                fireball.Push(_direction, _fireBallPower, _fireBallSpeed);
            }
            yield return new WaitForSeconds(_pushFireDelay);
        }
    }

    private void SetTarget(Vector3 direction)
    {
        _direction = direction;
    }
    
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            _enemies.Add(enemy.GetComponent<Enemy>());
            var direction = enemy.transform.position - _firePoint.transform.position;
            SetTarget(direction);
        }
    }
   
    private void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            _enemies.Remove(enemy.GetComponent<Enemy>());
        }
    }
}
