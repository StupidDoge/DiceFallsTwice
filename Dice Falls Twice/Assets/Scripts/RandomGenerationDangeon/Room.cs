using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TypeRoom 
{
   smallBox,
   L_type,
   x_type,
}
[System.Serializable]
public struct Wave 
{
    public EnemyAI[] enemy;
}
public class Room : MonoBehaviour
{
    [SerializeField] private TypeRoom _typeRoom;

    [SerializeField] Door _doorU;
    [SerializeField] Door _doorR;
    [SerializeField] Door _doorD;
    [SerializeField] Door _doorL;

    [SerializeField] private EnemyAI _boss;
    [SerializeField] private bool _roomIsClear = false;
    [SerializeField] private bool _isStartBatlle = false;
    [SerializeField] private Wave _currentWawe;
    [SerializeField] private Wave[] _wawes;
    [SerializeField] private GameObject[] _furniture;
    private int _valueEnemy;
    public UnityEvent unityEvent;
    public bool isendRoom;
    public bool RoomIsClear { get { return _roomIsClear; } }
    public TypeRoom TypeRoom
    {
        get { return _typeRoom; }
    }
    public Door DoorU
    {
        get { return _doorU; }
    }
    public Door DoorR
    {
        get { return _doorR; }
    }
    public Door DoorD
    {
        get { return _doorD; }
    }
    public Door DoorL
    {
        get { return _doorL; }
    }
    private void Start()
    {
        _doorU.Room = this;
        _doorR.Room = this;
        _doorD.Room = this;
        _doorL.Room = this;
    
        _currentWawe = _wawes[ Random.Range(0,_wawes.Length)];

        if (!isendRoom)
            _valueEnemy = _currentWawe.enemy.Length;
        else
            _valueEnemy = 1;
        _furniture[Random.Range(0, _furniture.Length)].SetActive(true);
        if (!isendRoom)
        {
            for (int i = 0; i < _currentWawe.enemy.Length; i++)
            {
                _currentWawe.enemy[i].gameObject.SetActive(true);
                _currentWawe.enemy[i]._death.AddListener(CheckCountEnemy);
            }
        }
        else 
        {
            _boss.gameObject.SetActive(true);
            _boss._death.AddListener(CheckCountEnemy);
        }
    }
    public void CheckCountEnemy() 
    {
        _valueEnemy--;
        if (_valueEnemy <= 0)
        {
            _roomIsClear = true;
        }
        if (isendRoom) 
        {
            unityEvent.Invoke();
        }
    }
    public void RotateRandomly() 
    {
        int count = Random.Range(0, 4);
        for (int i = 0; i < count; i++)
        {
            transform.Rotate(0, 90, 0);
            Door temp = _doorL;
            _doorL = _doorD;
            _doorD = _doorL;
            _doorR = _doorU;
            _doorU = temp;
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TopDownController>() && !_roomIsClear && !_isStartBatlle) 
        {
            _isStartBatlle = true;
        }
    }
}
