using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour, IColliderable
{
    public void OnCollide()
    {
        GameManager.instance.PlayerGoal();
    }
}
