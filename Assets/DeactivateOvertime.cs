using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOvertime : MonoBehaviour
{
    [SerializeField]private float timer;
    [SerializeField] private float tempTimer;

    private void Start()
    {
        tempTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        tempTimer -= Time.deltaTime;
        if (tempTimer <= 0) gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        tempTimer = timer;
    }
}
