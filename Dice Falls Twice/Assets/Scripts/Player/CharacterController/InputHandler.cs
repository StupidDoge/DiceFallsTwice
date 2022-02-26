using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    InputHandler : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }

    public Vector3 MousePosition { get; private set; }
    public bool isDash { get; private set; }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        InputVector = new Vector2(h, v);
        isDash = Input.GetButton("Dash");
        MousePosition = Input.mousePosition;
    }
}
