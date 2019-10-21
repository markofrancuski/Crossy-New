using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothness;

    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private Vector3 initialPosition;

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
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);

            //transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, smoothness);

            transform.Translate(Vector3.up * Time.deltaTime * speed);
            //diff = player.transform.position.x - gameObject.transform.position.x;
            //Debug.Log(diff);
            //if (diff >= distance) gameObject.transform.position += new Vector3(diff, 0, 0);
            //else 
            //gameObject.transform.position = 
        }
    }

    void ResetObject()
    {
        gameObject.transform.position = initialPosition;
    }
}
