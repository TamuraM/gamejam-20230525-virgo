using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UItext : MonoBehaviour
{
    [SerializeField] Text _coinText;
    [SerializeField] Text _timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _coinText.text = GameManager.instance.Coine.ToString();
        _timerText.text = GameManager.instance.Timar.ToString("f2");
    }
}
