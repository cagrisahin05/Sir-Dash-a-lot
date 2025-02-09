using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float chunkSpeedDecrease = -2f;
    
    const string ANIM_TRIGGER_HIT = "Hit";
    float cooldownTimer = 0f;

    LevelGenerator levelGenerator; // LevelGenerator scriptine erişmek için tanımlama

    private void Start() 
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>(); 
    }

    private void Update() 
    {
        
        cooldownTimer += Time.deltaTime; 
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Collision Detected");

        if (cooldownTimer < collisionCooldown) return;
        if (other.gameObject.CompareTag("Obstacle"))
        {
            levelGenerator.ChangeChunkMoveSpeed(chunkSpeedDecrease); // chunkların hızını azalt
            animator.SetTrigger(ANIM_TRIGGER_HIT);
            cooldownTimer = 0f;
        }
        
    }
   
    

}
