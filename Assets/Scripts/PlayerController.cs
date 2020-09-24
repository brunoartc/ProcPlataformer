using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    public CharacterController2D.CharacterCollisionState2D flags;
    public float walkSpeed = 4.0f;     // Depois de incluido, alterar no Unity Editor
    public float jumpSpeed = 8.0f;     // Depois de incluido, alterar no Unity Editor
    public float gravity = -9.8f;       // Depois de incluido, alterar no Unity Editor

    const float orgSpeed = 2.2f;

    public bool isGrounded;     // Se está no chão
    public bool isJumping;      // Se está pulando

    private int playerScore = 0;

    private Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
    private CharacterController2D characterController;	//Componente do Char. Controller

    public void AddPoint()
    {
        playerScore += 1;
        if (playerScore > PlayerPrefs.GetInt("coins")) {
            PlayerPrefs.SetInt("coins", playerScore);
        }
        
    }

    public void RefreshSpeed()
    {
        walkSpeed = orgSpeed;
    }
    void Start()
    {
        RefreshSpeed();
        characterController = GetComponent<CharacterController2D>(); //identif. o componente
        RefreshSpeed();
    }

    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal"); // recupera valor dos controles
        moveDirection.x *= walkSpeed;

        if (isGrounded)
        {            // caso esteja no chão
            moveDirection.y = 0.0f;    // se no chão nem subir nem descer

            isJumping = false;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                isJumping = true;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;    // aplica a gravidade
        characterController.move(moveDirection * Time.deltaTime);   // move personagem	

        flags = characterController.collisionState;     // recupera flags
        isGrounded = flags.below;				// define flag de chão

    }
}
