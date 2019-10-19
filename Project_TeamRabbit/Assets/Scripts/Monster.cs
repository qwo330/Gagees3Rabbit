using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Component")]
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
    State _state;
    [SerializeField] float _speed = 4f;

    [SerializeField] float _attackDist = 1.2f;
    float distance;

    [SerializeField] float _currHP;
    [SerializeField] float _maxHP = 1;

    private void Start()
    {
        Initialize();
    }
    
    private void Initialize()
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
        switch(next)
        {
            case State.Idle:

                break;
            case State.Move:
                StartCoroutine(IE_Move());
                break;
            case State.Attack:

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
        Vector2 dir;

        while(_state.Equals(State.Move))
        {
            if (!_groundCheck.isGround) StateMachine(State.Fall);

            yield return null;
        }

        StateMachine(State.Attack);
    }

    IEnumerator IE_Fall()
    {
        yield return new WaitUntil(() => _groundCheck.isGround);
        StateMachine(State.Move);
    }
}
