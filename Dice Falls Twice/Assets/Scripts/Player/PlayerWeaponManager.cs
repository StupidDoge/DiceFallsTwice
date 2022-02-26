using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private Wand[] _wandArray;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private float _minWaitingTime, _maxWaitingTime;

    private GameObject _currentWand;
    private PlayerAttack _playerAttack;
    private int _indexOfWeapon, _previousIndex;
    private float _angleX = -40;
    private Vector3 _rotation;
    [SerializeField] private Animator _anim;

    void Start()
    {
        _rotation = new Vector3(_angleX, 0, 0);
        _indexOfWeapon = Random.Range(0, _wandArray.Length);
        _previousIndex = _indexOfWeapon;
        _currentWand = Instantiate(_wandArray[_indexOfWeapon].gameObject, _collider.transform.position, Quaternion.Euler(_angleX, 0, 0)) as GameObject;
        _currentWand.transform.SetParent(_collider.transform);

        _playerAttack = GetComponent<PlayerAttack>();
        _playerAttack.setWand(_currentWand.gameObject.GetComponent<Wand>());

        InvokeRepeating("ChangeWeapon", Random.Range(_minWaitingTime, _maxWaitingTime), Random.Range(_minWaitingTime, _maxWaitingTime));
    }

    private void ChangeWeapon()
    {
        Debug.Log("Weapon changed");
        Destroy(_currentWand.gameObject);
        _anim.SetTrigger("Swap");
        while (_previousIndex == _indexOfWeapon)
            _indexOfWeapon = Random.Range(0, _wandArray.Length);
        _previousIndex = _indexOfWeapon;

        _currentWand = Instantiate(_wandArray[_indexOfWeapon].gameObject, _collider.transform.position, _currentWand.gameObject.transform.rotation) as GameObject;
        _playerAttack.setWand(_currentWand.gameObject.GetComponent<Wand>());
        _currentWand.transform.SetParent(_collider.transform);
    }
}
