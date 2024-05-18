using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEnemy : MonoBehaviour
{
    public List<Collider2D> detectedCollider = new List<Collider2D>();
    private Collider2D collider;

    private bool isPlayerDetected { get => detectedCollider.Count > 0; }

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollider.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollider.Remove(collision);
    }

    private void Update()
    {
        if (isPlayerDetected)
        {
            Debug.Log("Enemy Detected");
        }
    }
}
