using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class GomaControl : AbstractFiniteStateMachine
{
    public MovementControl _movementControl;
    public Rigidbody2D rb2d;
    public enum MyState
    {
        WALKING,
        DEFEATED,
        FALL
    }
    private void Awake()
    {
        Init(MyState.WALKING,
            AbstractState.Create<WalkingState, MyState>(MyState.WALKING, this),
            AbstractState.Create<DefeatedState, MyState>(MyState.DEFEATED, this),
            AbstractState.Create<FallState, MyState>(MyState.FALL, this)
        );
    }
    public class WalkingState : AbstractState
    {
        private GomaControl _gomaControl;
        private MovementControl _movementControl;
        private Rigidbody2D rb2d;
        public override void OnEnter()
        {
            _gomaControl = _parentStateMachine.GetComponent<GomaControl>();
            _movementControl = _gomaControl._movementControl;
            rb2d = _gomaControl.rb2d;
        }
        public override void OnUpdate()
        {
            _movementControl.axis = -1;
            _movementControl.Walk();
            rb2d.velocity = new Vector2(_movementControl.currentSpeed, rb2d.velocity.y);
        }
        public override void OnExit()
        {
        }
    }
    public class DefeatedState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
    public class FallState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
}
