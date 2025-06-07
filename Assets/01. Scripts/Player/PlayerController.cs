using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler InputHandler {get; private set;}
    public CharacterController Controller { get; private set; }

    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Controller = GetComponent<CharacterController>();
    }
   
}
