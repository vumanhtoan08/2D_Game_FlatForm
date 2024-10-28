using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Anim Manager for Player
public class AnimationState : MonoBehaviour
{
    //Khai báo biến 
    Movement _movementScript; // script Movement
    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _movementScript = GetComponent<Movement>();
    }

    void Update()
    {
        JumpAnim();
        RunAnim();
    }

    // Transform from idle to jump anim
    void JumpAnim()
    {
        if (_movementScript != null && _movementScript.IsJumping)
        {
            _anim.SetBool("jump", _movementScript.IsJumping);
        }
        else
        {
            _anim.SetBool("jump", _movementScript.IsJumping);
        }
    }

    // Transform from idle to run anim
    void RunAnim()
    {
        if (_movementScript != null && _movementScript.InputHorizontal != 0)
        {
            _anim.SetBool("run", true);
        }
        else
        {
            _anim.SetBool("run", false);
        }
    }
}
