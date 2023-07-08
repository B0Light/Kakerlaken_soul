using HeneGames.BugController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum State
    {
        idle,
        move,
        sprint,
        atk,
        dmg,
        dead,
        wait,
        Cine
    }
    public State state;

    protected static PlayerManager s_Instance;
    public static PlayerManager instance { get { return s_Instance; } }

    public float moveSpd;
    public bool isDead = false;
    public bool setZero = false;
    [SerializeField] private Animator _animator;
    public AbilityBase[] m_Abilities;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        m_Abilities = GetComponents<AbilityBase>();
        s_Instance = this;
    }


    void Update()
    {
        if(setZero) return ; 
        TakeAction();
    }

    void TakeAction()
    {
        if (_animator == null) return;

        switch (state)
        {
            case State.Cine:
                _animator.SetFloat("moveSpd", 0f);
                break;
            case State.idle:
                _animator.SetFloat("moveSpd", 0f);
                break;
            case State.move:
                _animator.SetFloat("moveSpd", moveSpd);
                break;
            case State.sprint:
                _animator.SetFloat("moveSpd", moveSpd);
                break;
            case State.atk:
                _animator.SetTrigger("atk");
                state = State.wait;
                break;
            case State.dmg:
                _animator.SetTrigger("hit");
                state = State.wait;
                break;
            case State.dead:
                _animator.SetTrigger("die");
                setZero = true;
                break;
        }
    }
}
