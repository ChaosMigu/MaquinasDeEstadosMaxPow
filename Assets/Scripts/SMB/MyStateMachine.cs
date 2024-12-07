using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
using UnityEditor.Experimental.GraphView;
public class MyStateMachine : AbstractFiniteStateMachine
{
    public enum MyState
    {
        IDLE,
        MOVE,
        JUMP
    }

    private void Awake()
    {
        Init(MyState.IDLE,
             AbstractState.Create<IdleState, MyState>(MyState.IDLE, this),
             AbstractState.Create<MoveState, MyState>(MyState.MOVE, this),
             AbstractState.Create<JumpState, MyState>(MyState.JUMP, this)
        );
    }

    public class IdleState : AbstractState
    {
        scr_controlPlayer controlPlayerScr;

        public override void OnEnter()
        {
            controlPlayerScr = _parentStateMachine.GetComponent<scr_controlPlayer>();
        }

        public override void OnUpdate()
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                TransitionToState(MyState.MOVE);
            }

            if (Input.GetKeyDown(KeyCode.Space) && controlPlayerScr.isGrounded)
            {
                TransitionToState(MyState.JUMP);
            }
        }

        public override void OnFixedUpdate()
        {
            controlPlayerScr.HorizontalMove();
        }
    }

    public class MoveState : AbstractState
    {
        scr_controlPlayer controlPlayerScr;

        public override void OnEnter()
        {
            controlPlayerScr = _parentStateMachine.GetComponent<scr_controlPlayer>();
        }

        public override void OnUpdate()
        {
            if (Input.GetAxis("Horizontal") == 0)
            {
                TransitionToState(MyState.IDLE);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TransitionToState(MyState.JUMP);
            }
        }

        public override void OnFixedUpdate()
        {
            controlPlayerScr.HorizontalMove();
        }
    }

    public class JumpState : AbstractState
    {
        private scr_controlPlayer controlPlayerScr;

        public override void OnEnter()
        {
            controlPlayerScr = _parentStateMachine.GetComponent<scr_controlPlayer>();
            controlPlayerScr.Jump();
        }

        public override void OnUpdate()
        {
            if (controlPlayerScr.isGrounded)
            {
                if (Input.GetAxis("Horizontal") == 0)
                {
                    TransitionToState(MyState.IDLE);
                }
                else
                {
                    TransitionToState(MyState.MOVE);
                }
            }
        }

        public override void OnFixedUpdate()
        {
            controlPlayerScr.HorizontalMove();
        }
    }

}
