using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour, IColliderable
{
    public void OnCollide()
    {
        GameManager.instance.AddCoine();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.AddCoine();
        Destroy(gameObject);
    }
}
