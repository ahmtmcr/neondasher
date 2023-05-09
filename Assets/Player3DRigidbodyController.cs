using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;





public class Player3DRigidbodyController : MonoBehaviour
{
    
    
    
   private GameStateManager gameStateManager;
   private SoundController soundController;
    
    
    
    public bool teleportPressed = false;

    public TeleportBars teleportBars;
    
    
    
    
    
    
    
    public DashBar dashBar;
    public float maxDashDistance = 5f;
    public float dashReloadSpeed = 3.5f;
    private float dashValue = 0f;
    private float dashtimer = 5f;
    
    
    
    
    
    
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float GroundDistance = 0.5f;
    public LayerMask Ground;

    private Rigidbody _body;
    public Vector3 _inputs = Vector3.zero;
    public Vector3 _androidInputs = Vector3.zero;
    public bool _isGrounded = true;
    private Transform _groundChecker;

    private PlayerInput playerInput;
    

    void Start()
    {
        
       gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
       soundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
        
        
        dashBar.SetMaxDash(maxDashDistance);
        dashValue = maxDashDistance;
        
        
        
        
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);

        playerInput = GetComponent<PlayerInput>();

       
        
    }

   
    
    
    void Update()
    {
        
      
        
        ChangePositions();
        
        
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

            //Windows
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.z = Input.GetAxisRaw("Vertical");
        _inputs = _inputs.normalized;
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            soundController.PlayJumpAudio();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashValue * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
            _body.AddForce(dashVelocity, ForceMode.VelocityChange);
            soundController.PlayDashAudio();
            dashValue -= dashValue;
            dashtimer = dashValue;
        }
        
           //Anroid
        Vector2 readValue = playerInput.actions["Move"].ReadValue<Vector2>();
        _androidInputs.x = readValue.x;
        _androidInputs.z = readValue.y;
        if(_androidInputs != Vector3.zero)
            transform.forward = _androidInputs;
        
        
        if (playerInput.actions["Jump"].triggered && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            soundController.PlayJumpAudio();
        }

        if (playerInput.actions["Dash"].triggered)
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashValue * new Vector3((Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
            _body.AddForce(dashVelocity, ForceMode.VelocityChange);
            soundController.PlayDashAudio();
            dashValue -= dashValue;
            dashtimer = dashValue;
        }
       
        
    }

    void AndroidMovement()
    {
        _body.MovePosition(_body.position + _androidInputs * Speed * Time.fixedDeltaTime);
    }
    void WindowsMovement()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
    
    void FixedUpdate()
    {
       AndroidMovement();
       WindowsMovement();
       DashSetter();
       
        
    }
    void DashSetter()
    {
        if(dashtimer < 5)
        {
            dashValue = dashtimer;
            dashtimer += Time.deltaTime * dashReloadSpeed;
        }
       
        dashBar.SetDash(dashValue);

    }
    void ChangePositions()
    {
        
  
        
        
        if(!teleportPressed && teleportBars.canPress)
        {
            
            
            if(Input.GetKeyDown(KeyCode.J) || playerInput.actions["Teleport"].triggered)
            {
             
                teleportPressed = true;
                teleportBars.TeleportPressed();
                gameStateManager.SendMatchState(
                    3,
                    MatchDataJson.TeleportPlus(teleportBars.slider.value));
               
                
                
                
             }
        }
   
       

        
    }


   
   
}

