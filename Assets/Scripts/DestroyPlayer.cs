using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{

    [SerializeField] private string message;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerController>() != null && !GameManager.instance.IsGameOver)
        {
            collision.collider.GetComponent<PlayerController>().PlayDeathAnimation(message);
            GameManager.instance.GameOver();
            //Destroy(collision.gameObject);
        }
        //else return;
    }
}
