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

}
