using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProxy : MonoBehaviour
{
    private PlayerController playerController;
    private EnemyController enemyController;
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        enemyController = GetComponentInParent<EnemyController>();
    }

    public void AttackEnemy()
    {
        if (playerController != null)
        {
            playerController.AttackEnemy();
        }
        else if (enemyController != null)
        {
            enemyController.AttackEnemy();
        }
        else
        {
            Debug.LogWarning("Controller is null in Proxy");
        }
    }
}
