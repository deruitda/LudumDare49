using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider Slider;

    public void SetHealthBarLevel(float health)
    {
        Slider.value = health;
    }
}
