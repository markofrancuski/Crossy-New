using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiblePlayer : MonoBehaviour
{
    [SerializeField] private PlayerController player ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible() 
    {
        Debug.Log("Not visible");
        GameManager.instance.GameOver();
        if(!player.isDead) player.PlayDeathAnimation("car");
    }

}
