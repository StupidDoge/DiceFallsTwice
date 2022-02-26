
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    [SerializeField] private int _sizeDungeon;
    [SerializeField] private Room[] _roomPrefabs;
    [SerializeField] private Room _startingRoom;
    [SerializeField] private float _sizeRoom;
    [SerializeField] private LayerMask _layerRoom;
    private Room[,] _spawnedRoom;
    private int _valueRoom=0;
    private List<Room> _rooms = new List<Room>();
    private bool _isTrueRoom;
    private float _sizeBigRoom=63;
    private IEnumerator Start()
    {
        
        _spawnedRoom = new Room[_sizeDungeon, _sizeDungeon];
        _spawnedRoom[3, 3] = _startingRoom;
        _rooms.Add(_startingRoom);
        for (int i = 0; i < _sizeDungeon+1; i++)
        {
            yield return new WaitForSeconds(0.1f);
            PlaceOneRoom();
        }
    }

    private void PlaceOneRoom()
    {
        do
        {
            _isTrueRoom = false;
            bool UpCheck;
            bool DownCheck;
            bool LeftCheck;
            bool RightCheck;
            float radius;
            float tempRoomPos = _sizeRoom;
            float targetSize = _sizeRoom;
            HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
            Room newRoom = _roomPrefabs[Random.Range(0, _roomPrefabs.Length)];
            Debug.Log(newRoom.TypeRoom);
            if (_rooms[_valueRoom].TypeRoom == TypeRoom.L_type || _rooms[_valueRoom].TypeRoom == TypeRoom.x_type)
            {
                if (newRoom.TypeRoom == TypeRoom.L_type || newRoom.TypeRoom == TypeRoom.x_type)
                {
                    targetSize = _sizeBigRoom * 1.5f;
                    radius = 30;

                }
                else
                {
                    targetSize = _sizeRoom * 2f;
                    radius = 16;
                }
            }
            else
            {
                if (newRoom.TypeRoom == TypeRoom.L_type || newRoom.TypeRoom == TypeRoom.x_type)
                {
                    targetSize = _sizeBigRoom;
                    radius = 30;
                }
                else
                {
                    targetSize = _sizeRoom + 2;
                    radius = 16;
                }

            }

            UpCheck = Physics.CheckSphere(_rooms[_valueRoom].transform.position + Vector3.forward * targetSize, radius, _layerRoom);
            DownCheck = Physics.CheckSphere(_rooms[_valueRoom].transform.position + Vector3.back * targetSize, radius, _layerRoom);
            LeftCheck = Physics.CheckSphere(_rooms[_valueRoom].transform.position + Vector3.left * targetSize, radius, _layerRoom);
            RightCheck = Physics.CheckSphere(_rooms[_valueRoom].transform.position + Vector3.right * targetSize, radius, _layerRoom);
            if (!RightCheck) vacantPlaces.Add(new Vector2Int(1, 0));
            if (!DownCheck) vacantPlaces.Add(new Vector2Int(0, -1));
            if (!LeftCheck) vacantPlaces.Add(new Vector2Int(-1, 0));
            if (!UpCheck) vacantPlaces.Add(new Vector2Int(0, 1));
            Debug.Log(vacantPlaces.Count);
            if (vacantPlaces.Count > 0)
            {

                _isTrueRoom = true;
                Room temp = Instantiate(newRoom);
                int index = Random.Range(0, vacantPlaces.Count);
                Vector2Int t_pos = vacantPlaces.ElementAt(index);
                Vector2 position = vacantPlaces.ElementAt(index);

                //newRoom.RotateRandomly();
                if (newRoom.TypeRoom == TypeRoom.L_type || newRoom.TypeRoom == TypeRoom.x_type)
                {
                    if (_rooms[_valueRoom].TypeRoom == TypeRoom.L_type || _rooms[_valueRoom].TypeRoom == TypeRoom.x_type)
                    {
                        Debug.Log(11);
                        position *= _sizeBigRoom * 1.5f;
                    }
                    else
                    {
                        Debug.Log(22);
                        position *= _sizeBigRoom;
                    }
                }
                else
                {
                    if (_rooms[_valueRoom].TypeRoom == TypeRoom.L_type || _rooms[_valueRoom].TypeRoom == TypeRoom.x_type)
                    {
                        Debug.Log(33);
                        position *= _sizeBigRoom;
                    }
                    else
                    {
                        Debug.Log(44);
                        position *= _sizeRoom;
                    }

                }
                temp.transform.position = new Vector3(_rooms[_valueRoom].transform.position.x + position.x, 0, _rooms[_valueRoom].transform.position.z + position.y);
                _rooms.Add(temp);
                //включение двери
                if (t_pos.x == 1)
                {
                    _rooms[_valueRoom].DoorR.gameObject.SetActive(true);
                    _rooms[_valueRoom + 1].DoorL.gameObject.SetActive(true);
                    Debug.Log("Спарво");
                }
                if (t_pos.x == -1)
                {
                    _rooms[_valueRoom].DoorL.gameObject.SetActive(true);
                    _rooms[_valueRoom + 1].DoorR.gameObject.SetActive(true);
                    Debug.Log("Слева");
                }
                if (t_pos.y == 1)
                {
                    _rooms[_valueRoom].DoorU.gameObject.SetActive(true);
                    _rooms[_valueRoom + 1].DoorD.gameObject.SetActive(true);
                    Debug.Log("Сверху");
                }
                if (t_pos.y == -1)
                {
                    _rooms[_valueRoom].DoorD.gameObject.SetActive(true);
                    _rooms[_valueRoom + 1].DoorU.gameObject.SetActive(true);
                    Debug.Log("Снизу");
                }

                _valueRoom++;
            }
        } while (!_isTrueRoom);
        

    }


    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(_startingRoom.transform.position + Vector3.forward  * _sizeRoom , new Vector3(_sizeRoom, _sizeRoom, _sizeRoom));
        //Gizmos.DrawCube(_startingRoom.transform.position  + Vector3.back  * _sizeRoom , new Vector3(_sizeRoom, _sizeRoom, _sizeRoom));
        //Gizmos.DrawCube(_startingRoom.transform.position + Vector3.left * _sizeRoom , new Vector3(_sizeRoom, _sizeRoom, _sizeRoom));
        //Gizmos.DrawCube(_startingRoom.transform.position  + Vector3.right * _sizeRoom , new Vector3(_sizeRoom, _sizeRoom, _sizeRoom));
        //Gizmos.DrawCube(_startingRoom.transform.position + Vector3.right * _sizeRoom*1.75f , new Vector3(_sizeBigRoom, _sizeBigRoom, _sizeBigRoom));
       //Gizmos.DrawCube(_startingRoom.transform.position + Vector3.right * _sizeBigRoom*1.08f, new Vector3(_sizeBigRoom, _sizeBigRoom, _sizeBigRoom));
       //Gizmos.DrawCube(_rooms[_valueRoom].transform.position + Vector3.right * _sizeBigRoom*1.08f, new Vector3(_sizeBigRoom, _sizeBigRoom, _sizeBigRoom));
      //** Gizmos.DrawCube(_startingRoom.transform.position + Vector3.right * _sizeRoom * 2f, new Vector3(_sizeBigRoom, _sizeBigRoom, _sizeBigRoom));
   

    }
}
