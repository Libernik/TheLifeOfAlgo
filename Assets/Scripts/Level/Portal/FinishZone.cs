using UnityEngine;
using System;

public class FinishZone : MonoBehaviour
{
    public event Action OnPlayerEntered;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        OnPlayerEntered?.Invoke();
    }
}