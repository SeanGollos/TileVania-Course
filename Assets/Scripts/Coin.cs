using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] int coinPoints = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetType().ToString().Equals("UnityEngine.CapsuleCollider2D")) //We add this since the player has two colliders associated with him
        {
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(coinPoints);
            Destroy(gameObject);
        }
    }
}
