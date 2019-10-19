using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] PlayerController _target;
    [SerializeField] GroundCheck _groundCheck;

    enum State
    {
        Idle,
        Move,
        Attack,
        Dead,
        Fall
    }

    [Header("Parameters")]
    [SerializeField] State _state;
    [SerializeField] float _speed = 4f;

    [SerializeField] float _attackDist = 1.2f;
    float distance;

    [SerializeField] float _currHP;
    [SerializeField] float _maxHP = 1;

    private void Start()
    {
        _target = GameManager.Get.player;
        Initialize();
    }
    
    public void Initialize()
    {   
        _currHP = _maxHP;
        StateMachine(State.Move);
    }

    public void TakeDamage(float damage)
    {
        _currHP -= damage;
        
        if(_currHP <= 0)
        {
            StopAllCoroutines();
            StateMachine(State.Dead);
        }
    }

    void StateMachine(State next)
    {
        _state = next;
        switch(_state)
        {
            case State.Idle:

                break;
            case State.Move:
                StartCoroutine(IE_Move());
                break;
            case State.Attack:
                StartCoroutine(IE_Attack());
                break;
            case State.Dead:

                break;
            case State.Fall:
                StartCoroutine(IE_Fall());
                break;
        }
    }

    IEnumerator IE_Move()
    {
        distance = Vector2.Distance(transform.position, _target.transform.position);

        while(_state.Equals(State.Move))
        {
            if (!_groundCheck.isGround) StateMachine(State.Fall);
            if (distance <= _attackDist) StateMachine(State.Attack);

            Vector2 dir = _target.transform.position - transform.position;
            dir.Normalize();

            transform.Translate(dir * _speed * Time.deltaTime);
            
            distance = Vector2.Distance(transform.position, _target.transform.position);

            yield return null;
        }

        StateMachine(State.Attack);
    }

    IEnumerator IE_Attack()
    {
        while (_state.Equals(State.Attack))
        {
            if (distance > _attackDist) StateMachine(State.Move);
                 
            yield return new WaitForSeconds(1.5f);

            distance = Vector2.Distance(transform.position, _target.transform.position);
        }
    }

    IEnumerator IE_Fall()
    {
        while (_state.Equals(State.Fall))
        {
            if(_groundCheck.isGround) StateMachine(State.Move);
            yield return null;
        }
    }
}
