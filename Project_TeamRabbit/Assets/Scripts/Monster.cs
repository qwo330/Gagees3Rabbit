using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] PlayerController _target;
    [SerializeField] GroundCheck _groundCheck;
    [SerializeField] Animator _animator;

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
    float _attackAnim;    //애니메이션 공격 포인트
    float distance;

    [SerializeField] float _damage = 4;
    [SerializeField] float _currHP;
    [SerializeField] float _maxHP = 1;

    [Header("Option")]
    [SerializeField] bool isBoneThrower = false;    //원거리형이라면 true

    private void Start()
    {
        _target = GameManager.Get.player;
    }
    
    public void Initialize()
    {   
        _currHP = _maxHP;
        StateMachine(State.Move);

        if (isBoneThrower) _attackAnim = 0.67f;
        else _attackAnim = 0.467f;
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
                StopAllCoroutines();
                ObjectPool.Get.ReturnObject(gameObject);
                gameObject.SetActive(false);
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
        if (isBoneThrower) _animator.SetTrigger("ATTACK_THROW");
        else _animator.SetTrigger("ATTACK");

        while (_state.Equals(State.Attack))
        {
            if (distance > _attackDist) StateMachine(State.Move);
                 
            yield return new WaitForSeconds(_attackAnim);

            if (isBoneThrower)
            {
                GameObject bullet = ObjectPool.Get.GetObject("Zombie_Bone");
                bullet.transform.position = transform.position;

                Vector2 dir = _target.transform.position - transform.position;
                dir.Normalize();

                bullet.GetComponent<Rigidbody2D>().AddForce(dir * 100 * Time.deltaTime, ForceMode2D.Impulse);

            }
            else
            {
                distance = Vector2.Distance(transform.position, _target.transform.position);
                if (distance <= _attackDist) _target.TakeDamage(_damage);
            }

            if(isBoneThrower) yield return new WaitForSeconds(1 - _attackAnim);
            else yield return new WaitForSeconds(0.7f - _attackAnim);

            distance = Vector2.Distance(transform.position, _target.transform.position);
            if(distance > _attackDist) StateMachine(State.Move);
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
