using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProxy : MonoBehaviour
{
    private PlayerController controller;
    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    public void AttackEnemy()
    {
        if (controller != null)
        {
            controller.AttackEnemy();
        }
        else
        {
            Debug.LogWarning("Controller is null in Proxy");
        }
    }
}
