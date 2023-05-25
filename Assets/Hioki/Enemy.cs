using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IColliderable
{
    public void OnCollide()
    {
        GameManager.instance.Damage(1);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}
