using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _waitingTime;
    [SerializeField] private int _damage;

    private AudioManager _audioManager;
    private GameObject _manager;

    private float _lastTime=-100;
    [SerializeField] private bool _isBFG;
    private int _ammoCount;
    Bullet bullet;

    private void Awake()
    {
        _manager = GameObject.FindGameObjectWithTag("AudioManagerSpell");
        _audioManager = _manager.GetComponent<AudioManager>();

        if (_isBFG)
            _ammoCount = 1;
        else
            _ammoCount = 1000;
    }

    public virtual void Shoot()
    {
        if (Time.time>=_lastTime+_waitingTime && _ammoCount > 0)
        {
            _audioManager.Play("Spell");
            _lastTime = Time.time;
            bullet = Instantiate(_bullet, _bulletSpawn.transform.position, _bulletSpawn.transform.rotation);
            _ammoCount--;

            bullet.Damage = _damage;
        }
    }
}
