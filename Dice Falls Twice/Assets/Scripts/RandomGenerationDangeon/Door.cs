using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private Room _room;

    public Room Room { set { _room = value; } }
    private void OnTriggerEnter(Collider other)
    {
      

        if (other.TryGetComponent<TopDownController>(out TopDownController player) && _room.RoomIsClear)
        {
            player.CharacterController.enabled = false;
            player.transform.position = new Vector3(_exitPoint.position.x, player.transform.position.y, _exitPoint.position.z);
            Debug.Log(other.name);
            player.CharacterController.enabled = true;

        }
    }
}
