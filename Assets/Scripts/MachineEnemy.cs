using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class MachineEnemy : AbstractFiniteStateMachine
{
    public enum MyState
    {
        MOVEENEMY
    }
    private void Awake()
    {
        Init(MyState.MOVEENEMY,
            AbstractState.Create<StateAState, MyState>(MyState.MOVEENEMY, this)
        );
    }
    public class StateAState : AbstractState
    {
        public scr_gomba gombaScr;
        public override void OnEnter()
        {
            gombaScr = _parentStateMachine.GetComponent<scr_gomba>();     
        }
        public override void OnFixedUpdate()
        {
            gombaScr.MoveEnemy();
        }
    }
}
