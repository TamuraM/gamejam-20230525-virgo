using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UItext : MonoBehaviour
{
    [SerializeField] Text _coinText;
    [SerializeField] Text _timerText;
    [SerializeField] Text _lifeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _coinText.text = GameManager.instance.Coin.ToString();
        _timerText.text = GameManager.instance.Timar.ToString("f2");
        _lifeText.text = GameManager.instance.Life.ToString();
    }
}
