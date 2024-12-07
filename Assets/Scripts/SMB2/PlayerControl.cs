using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
using UnityEngine.UIElements;

public class PlayerControl : AbstractFiniteStateMachine
{
    public MovementControl _movementControl;
    public JumpControl _jumpControl;

    public Rigidbody2D rb2d;
    public enum MyState
    {
        IDLE,
        WALKING,
        RUNNING,
        JUMPING,
        FALLING,
        SHOOTING,
        DEFEATING,
        POWERUP
    }
    private void Awake()
    {
        Init(MyState.IDLE,
            AbstractState.Create<IdleState, MyState>(MyState.IDLE, this),
            AbstractState.Create<WalkingState, MyState>(MyState.WALKING, this),
            AbstractState.Create<RunningState, MyState>(MyState.RUNNING, this),
            AbstractState.Create<JumpingState, MyState>(MyState.JUMPING, this),
            AbstractState.Create<FallingState, MyState>(MyState.FALLING, this),
            AbstractState.Create<ShootingState, MyState>(MyState.SHOOTING, this),
            AbstractState.Create<DefeatingState, MyState>(MyState.DEFEATING, this),
            AbstractState.Create<PowerupState, MyState>(MyState.POWERUP, this)
        );
    }
    public class IdleState : AbstractState
    {
        private PlayerControl _playerControl;
        private MovementControl _movementControl;
        public override void OnEnter()
        {
            _playerControl = _parentStateMachine.GetComponent<PlayerControl>();
            _movementControl = _playerControl._movementControl;
        }
        public override void OnUpdate()
        {
            _movementControl.Decelaration();
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                TransitionToState(MyState.WALKING);
            }
            if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetKeyDown(KeyCode.K))
            {
                TransitionToState(MyState.RUNNING);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                TransitionToState(MyState.JUMPING);
            }
        }
        public override void OnExit()
        {
        }
    }
    public class WalkingState : AbstractState
    {
        private PlayerControl _playerControl;
        private MovementControl _movementControl;
        private Rigidbody2D _rb2d;
        public override void OnEnter()
        {
            _playerControl = _parentStateMachine.GetComponent<PlayerControl>();
            _movementControl = _playerControl._movementControl;
            _rb2d = _playerControl.rb2d;
        }
        public override void OnUpdate()
        {
             _movementControl.axis = Input.GetAxisRaw("Horizontal");
             _movementControl.Walk();
             _rb2d.velocity = new Vector2(_movementControl.currentSpeed, _rb2d.velocity.y);

            if(Input.GetAxisRaw("Horizontal") == 0)
            {
                TransitionToState(MyState.IDLE);
            }
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKeyDown(KeyCode.K))
            {
                TransitionToState(MyState.RUNNING);
            }
            if(Input.GetKeyDown(KeyCode.J))
            {
                TransitionToState(MyState.JUMPING);
            }
        }
        public override void OnExit()
        {
        }
    }
    public class RunningState : AbstractState
    {
        private PlayerControl _playerControl;
        private MovementControl _movementControl;
        private Rigidbody2D _rb2d;
        public override void OnEnter()
        {
            _playerControl = _parentStateMachine.GetComponent<PlayerControl>();
            _movementControl = _playerControl._movementControl;
            _rb2d = _playerControl.rb2d;
        }
        public override void OnUpdate()
        {
            _movementControl.axis = Input.GetAxisRaw("Horizontal");
            _movementControl.Run();
            _rb2d.velocity = new Vector2(_movementControl.currentSpeed, _rb2d.velocity.y);

            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKeyUp(KeyCode.K))
            {
                TransitionToState(MyState.WALKING);
            }

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                TransitionToState(MyState.IDLE);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                TransitionToState(MyState.JUMPING);
            }
        }
        public override void OnExit()
        {
        }
    }
    public class JumpingState : AbstractState
    {
        private PlayerControl _playerControl;
        private JumpControl _jumpControl;
        private MovementControl _movementControl;
        private Rigidbody2D rb2d;
        public override void OnEnter()
        {
            _playerControl = _parentStateMachine.GetComponent<PlayerControl>();
            _jumpControl = _playerControl._jumpControl;
            _movementControl = _playerControl._movementControl;
            rb2d = _playerControl.rb2d;

            rb2d.velocity = new Vector2(_movementControl.currentSpeed, _jumpControl.jumpForce);
            _jumpControl.Jump();
        }
        public override void OnUpdate()
        {
            rb2d.velocity = new Vector2(_movementControl.currentSpeed, rb2d.velocity.y);
            _movementControl.axis = Input.GetAxisRaw("Horizontal");
            _movementControl.AirMovement();

            if(rb2d.velocity.y <= 0)
            {
                TransitionToState(MyState.FALLING);
            }
        }
        public override void OnExit()
        {
        }
    }
    public class FallingState : AbstractState
    {
        private PlayerControl _playerControl;
        float coolDownParaVolverAlIdlePorqueNoQuieroHacerUnRaycast;
        public override void OnEnter()
        {
            _playerControl = _parentStateMachine.GetComponent<PlayerControl>();
            coolDownParaVolverAlIdlePorqueNoQuieroHacerUnRaycast = 0;

            _playerControl._jumpControl.StopJump();
        }
        public override void OnUpdate()
        {
            //Aquí realmente deberia existir una logica con un raycast que si tocas el suelo es cuando vuelves al idle
            if(coolDownParaVolverAlIdlePorqueNoQuieroHacerUnRaycast > 0)
            {
                coolDownParaVolverAlIdlePorqueNoQuieroHacerUnRaycast -= 1 * Time.deltaTime;
            }
            else if(coolDownParaVolverAlIdlePorqueNoQuieroHacerUnRaycast <= 0)
            {
                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    TransitionToState(MyState.IDLE);
                }
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    TransitionToState(MyState.WALKING);
                }
                if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetKeyUp(KeyCode.K))
                {
                    TransitionToState(MyState.RUNNING);
                }
            }
        }
        public override void OnExit()
        {
        }
    }
    public class ShootingState : AbstractState
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
    public class DefeatingState : AbstractState
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
    public class PowerupState : AbstractState
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
