using HeneGames.BugController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //PlayerManager

    public enum State
    {
        idle,
        move,
        atk,
        dmg,
        dead,
        wait
    }
    public State state;
    public float moveSpd;
    [SerializeField] private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        TakeAction();
    }

    void TakeAction()
    {
        if (_animator == null) return;

        switch (state)
        {
            case State.idle:
                _animator.SetFloat("moveSpd", 0f);
                break;
            case State.move:
                _animator.SetFloat("moveSpd", moveSpd);
                break;
            case State.atk:
                _animator.SetTrigger("atk");
                state = State.wait;
                break;
            case State.dmg:
                _animator.SetTrigger("hit");
                break;
            case State.dead:
                _animator.SetTrigger("die");
                break;
        }
    }
}
