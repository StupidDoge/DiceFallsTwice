using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerWeaponManager>().gameObject;
    }

    [System.Obsolete]
    void Update()
    {
        if (_player.gameObject.activeInHierarchy == false)
            StartCoroutine(RestartScene());
    }

    [System.Obsolete]
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3f);
        if (Input.GetMouseButton(0))
            Application.LoadLevel(Application.loadedLevel);
    }
}
