﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pixelplacement;

public class PlayerController : MonoBehaviour
{
    private bool isJumping;

    public bool isDead;

    private float score;
    private float oneSecTimer = 1;

    [Header("Game Objects")] [SerializeField] private GameObject dirtParticle;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private List<GameObject> deathParticles;
    [SerializeField] private List<AudioClip> deathSounds;
 
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [Header("Components")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Animator animator;

    private void Start()
    {
        GameManager.instance.reset += ResetObject;        
    }

    private void Update()
    {
        scoreText.SetText(score.ToString());

        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {

            if (!Physics.Raycast(transform.position, Vector3.right, 1f, 1 << 8))
            {
                float zDiff = 0;
                // If its space off you can jump if you are at 0.3 you cannot jump
                if (transform.position.z % 1 != 0)
                {
                    zDiff = Mathf.Round(transform.position.z) - transform.position.z;
                }

                MovePlayer(new Vector3(1, 0, zDiff));
            }

        }

        if (Input.GetKeyDown(KeyCode.A) && !isJumping )
        {
            if (!Physics.Raycast(transform.position, Vector3.forward, 1f, 1 << 8))
            {
                MovePlayer(Vector3.forward);
            }

        }

        if (Input.GetKeyDown(KeyCode.D) && !isJumping)
        {
            if (!Physics.Raycast(transform.position, Vector3.back, 1f, 1 << 8)) MovePlayer(Vector3.back);
        }

        if (Input.GetKeyDown(KeyCode.S) && !isJumping)
        {
            if (!Physics.Raycast(transform.position, Vector3.left, 1f, 1 << 8)) MovePlayer(Vector3.left);
        }

    }
    public void FinishJump()
    {
        isJumping = false;
        Instantiate(dirtParticle, gameObject.transform.position + new Vector3(0, -0.45f, 0), Quaternion.identity);
    }

    private void MovePlayer(Vector3 positionToMove)
    {
        if (GameManager.instance.IsGameOver) return;

        if (!GameManager.instance.isStarted) GameManager.instance.isStarted = true;
        animator.SetTrigger("Jump");
        isJumping = true;

        Tween.Position(transform, transform.position, transform.position + positionToMove, 0.1f, 0);

        //transform.position = transform.position + positionToMove;

        audioSource.Play();

        mapGenerator.SpawnTerrain(false, transform.position);

    }

    private void UpdateScore()
    {
        scoreText.SetText(score.ToString());
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.collider.GetComponent<MovingObject>() != null)
        {
            if(collision.collider.GetComponent<MovingObject>().isLog)
            {
                transform.parent = collision.collider.transform;
            }
        }
        else { transform.parent = null; }
    }

    private void FixedUpdate()
    {
        if(!GameManager.instance.IsGameOver && GameManager.instance.isStarted)
        {
            oneSecTimer -= Time.deltaTime;
            if(oneSecTimer <= 0)
            {
                oneSecTimer = 1;
                score++;
                UpdateScore();
            }
        }
    }

    public void PlayDeathAnimation(string message)
    {
        isDead = true;
        switch (message)
        {
            case "water":
                if(GameManager.instance.IsSoundEffectOn) deathAudioSource.PlayOneShot(deathSounds[0]);
                GameObject go = Instantiate(deathParticles[0]);
                go.transform.position = gameObject.transform.position + new Vector3(0, 0.2f, 0);
                playerRenderer.enabled = false;
                break;
            case "car": 
            if(GameManager.instance.IsSoundEffectOn) deathAudioSource.PlayOneShot(deathSounds[1]); 
            //Instantiate(deathParticles[1], gameObject.transform.position, Quaternion.identity);  
            animator.SetBool("Squash", true);
            break;
        }
        
        //Instantiate();
    }
    private void ResetObject()
    {
        gameObject.transform.parent = null;
        gameObject.transform.position = new Vector3(0,1, 0);
        isJumping = false;
        score = 0;
        playerRenderer.enabled = true;
        isDead = false;
        animator.SetBool("Squash", false);

    }

}
