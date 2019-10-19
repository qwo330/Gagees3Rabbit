using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        Attack,
        Dead
    }

    State _state;
    [SerializeField] float _speed = 4f;

    [SerializeField] float _attackDist = 1.2f;
    float distance;

    private void Start()
    {
        
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
        }
    }

    IEnumerator IE_Move()
    {
        Vector2 dir;

        while(distance >= _attackDist)
        {

            yield return null;
        }

        StateMachine(State.Attack);
    }


}
