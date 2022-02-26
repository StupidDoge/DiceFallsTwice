using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField]
    private int MaxHp;
    [SerializeField]
    private Healthbar HealthBar;

    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private CharacterController Controller;
    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private float TimeDash;
    [SerializeField]
    private float SpeedDash;
    [SerializeField]
    private float TimeToResetDash;
    [SerializeField]
    private LayerMask _layer;
 
    public HealthSystem HealthSystem { get { return _healthSystem; } }
    public CharacterController CharacterController { get { return Controller; } }
    private HealthSystem _healthSystem;
    private float _lastTimeDash=-100;
    private bool _isDash;
    private Vector3 _dirDash;
    private Vector3 targetVector;
    [SerializeField] private Animator _anim;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }
    private void Start()
    {
        _healthSystem = new HealthSystem(MaxHp, gameObject);
        HealthBar.Setup(_healthSystem);

    }
    // Update is called once per frame
    void Update()
    {
       
        targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        RotateFromMouseVector();
        if (_input.isDash && !_isDash && Time.time>=_lastTimeDash+TimeToResetDash )
        {
             StartCoroutine(Dash());
        }
        if (!_isDash)
        {
            if (targetVector == Vector3.zero)
            {
                
                //_anim.SetBool("idle", true);
               // _anim.SetBool("move", false);
            }
            else
            {
               // _anim.SetBool("idle", false);
               // _anim.SetBool("move", true);
              
            } 
            MoveTowardTarget(targetVector, MovementSpeed);
            _dirDash = targetVector;
        }
        else 
        {
            MoveTowardTarget(_dirDash, SpeedDash);
        }
    }

    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f, _layer))
        {
            Vector3 target = hitInfo.point ;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector,float speed)
    {
        float t_speed = speed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        targetVector.Normalize();
        Controller.Move(targetVector * t_speed);
        //Vector3 targetPosition = transform.position + targetVector * t_speed;
       // transform.position = targetPosition;
        return targetVector;
    }

  
    private IEnumerator Dash()
    {
        _isDash = true;
        _lastTimeDash = Time.time;
        yield return new WaitForSeconds(TimeDash);
       _isDash = false;
      
    }
}
