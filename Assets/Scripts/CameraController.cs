using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float speed;
    private Vector3 initialPosition;

    [SerializeField] private Transform center;

    private Vector3 nextPosition;

    float diff;

    private void Start()
    {
        initialPosition = transform.position;
        GameManager.instance.reset += ResetObject;
    }
    private void FixedUpdate()
    {
        if (player != null && GameManager.instance.isStarted && !GameManager.instance.IsGameOver)
        {
            diff = player.transform.position.x - center.position.x;

            if(diff  > 0)
            {
                Tween.Position(transform ,gameObject.transform.position, new Vector3(player.position.x, gameObject.transform.position.y, 0), 0.06f, 0f);
                //gameObject.transform.position = new Vector3(player.position.x, 10, 0);
            }
            nextPosition = gameObject.transform.position + Vector3.right;
            
            //new Vector3(gameObject.transform.position.x +1, gameObject.transform.position.y, 0 );// Vector3.right;

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextPosition, Time.deltaTime * speed);  

        }
    }

    void ResetObject()
    {
        gameObject.transform.position = initialPosition;
    }
}
