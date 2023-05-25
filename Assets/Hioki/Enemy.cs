using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IColliderable
{
    public void OnCollide()
    {
        Destroy(gameObject);
    }
}
