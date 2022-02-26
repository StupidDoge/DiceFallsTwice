using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Wand _wand;

    public void setWand(Wand newWand)
    {
        _wand = newWand;
    }

    void Start()
    {
        _wand = GetComponentInChildren<Wand>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            _wand.Shoot();
    }
}
