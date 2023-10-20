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
        gameActions.gameplay.jump.performed += onJumpAction;
        gameActions.gameplay.jumphold.performed += onJumpHoldAction;
        gameActions.gameplay.move.started += onMoveAction;
        gameActions.gameplay.move.canceled += onMoveAction;
        gameActions.gameplay.kick.started += OnKickAction;
        gameActions.gameplay.kick.canceled += OnKickAction;
        gameActions.gameplay.kick.performed += OnKickAction;
        gameActions.gameplay.punch.performed += OnPunchAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onJumpHoldAction(InputAction.CallbackContext context){
        if(context.started)
            {
                // Debug.Log("Jumphold was started");
            }
        else if(context.performed)
        {   
            jumpHold.Invoke();
            // Debug.Log("Jumphold was pefermoed");
        }    
            
        // else if(context.canceled)
            // Debug.Log("Jumphold was cancelled");
    }

    public void onJumpAction(InputAction.CallbackContext context){
        if(context.started)
        {
            // Debug.Log("Jump was started");
        }
        else if(context.performed)
        {
            jump.Invoke();
            // Debug.Log("Jump was pefermoed");
        }
            
        // else if(context.canceled)
        //     Debug.Log("Jump was cancelled");
    }

    public void onMoveAction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // Debug.Log("move was started");
            int faceRight = context.ReadValue<float>() >0? 1:-1;
            moveCheck.Invoke(faceRight);
            // Debug.Log($"move amount {fa}");
        }
        if(context.canceled){
            Debug.Log("move stopped");
            moveCheck.Invoke(0);

        }
    }

    public void OnClickAction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            // Debug.Log("mouse click started");
        }    
        else if(context.performed)
        {
            // Debug.Log("mouse click performed");
        }

        // else if( context.canceled)
        //     Debug.Log("mouse click cancelled");

    }

    public void OnPointAction(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            
            Vector2 point = context.ReadValue<Vector2>();
            // Debug.Log($"mouse pos {point}");
        }


    }

    public void OnKickAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            Debug.Log("kick action");
            kick.Invoke();
        }
        
    }
    
    public void OnMedidateAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            Debug.Log("medidate action");
            meditate.Invoke();
        }
        
    }
    
    public void OnPunchAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            Debug.Log("punch action");
            punch.Invoke();
        }
        
    }
}
