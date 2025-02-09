using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    const string playerString = "Player";
    public int scoreValue = 0;

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime ,0);
    }

  private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerString))
        {
            gameObject.SetActive(false);
            ScoreManager.instance.AddScore(scoreValue);
        }
    }
    
}
