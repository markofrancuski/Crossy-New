using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isJumping;

    [SerializeField] private GameObject dirtParticle;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private List<GameObject> DeathParticles;
    [SerializeField] private List<AudioClip> DeathSounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource deathAudioSource;

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

        transform.position = transform.position + positionToMove;

        audioSource.Play();

        mapGenerator.SpawnTerrain(false, transform.position);

    }

    [SerializeField] private TextMeshProUGUI scoreText;

    private float score;
    private float oneSecTimer = 1;

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

    [SerializeField] private Renderer playerRenderer;

    public bool isDead;
    public void PlayDeathAnimation(string message)
    {
        isDead = true;
        switch (message)
        {
            case "water":
                if(GameManager.instance.IsSoundEffectOn) deathAudioSource.PlayOneShot(DeathSounds[0]);
                GameObject go = Instantiate(DeathParticles[0]);
                go.transform.position = gameObject.transform.position + new Vector3(0, 0.2f, 0);
                break;
            case "car": if(GameManager.instance.IsSoundEffectOn) deathAudioSource.PlayOneShot(DeathSounds[1]); Instantiate(DeathParticles[1], gameObject.transform.position, Quaternion.identity);  break;

        }
        playerRenderer.enabled = false;
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

    }

}
