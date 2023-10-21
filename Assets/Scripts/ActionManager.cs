using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public GameActions gameActions;
    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent<int> moveCheck;

    public UnityEvent kick;

    public UnityEvent meditate;

    public UnityEvent punch;
    // Start is called before the first frame update
    void Start()
    {
        gameActions = new GameActions();
        gameActions.gameplay.Enable();
        gameActions.gameplay.jump.performed += OnJumpAction;
        gameActions.gameplay.jumphold.performed += onJumpHoldAction;
        gameActions.gameplay.move.started += OnMoveAction;
        gameActions.gameplay.move.canceled += OnMoveAction;
        gameActions.gameplay.kick.performed += OnKickAction;
        gameActions.gameplay.punch.performed += OnPunchAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onJumpHoldAction(InputAction.CallbackContext context){ 
        if(context.performed)
        {   
            jumpHold.Invoke();
        }    
        
    }

    public void OnJumpAction(InputAction.CallbackContext context){
        if(context.performed)
        {
            jump.Invoke();
        }
        
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            int faceRight = context.ReadValue<float>() >0? 1:-1;
            moveCheck.Invoke(faceRight);
        }
        if(context.canceled){
            moveCheck.Invoke(0);

        }
    }
    
    public void OnKickAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            kick.Invoke();
        }
        
    }
    
    public void OnPunchAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            punch.Invoke();
        }
        
    }
}
