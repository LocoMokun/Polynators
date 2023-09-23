using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    void Start()
    {
        if (player == null)
        {
            player = GetComponentInParent<PlayerController>();
        }
    }

    public void CallPlayerFlower()
    {
        player.ChangeFlower();
    }

}
