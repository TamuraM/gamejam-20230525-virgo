using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaSlider : MonoBehaviour
{
    // Start is called before the first frame update
    Slider _staminaSlider;
    [SerializeField]PlayerController _playerController;
    void Start()
    {
       _staminaSlider = GetComponent<Slider>();
       _staminaSlider.maxValue = _playerController.MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        _staminaSlider.value = _playerController.Stamina;
    }
}
