using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHungerBar : MonoBehaviour
{
    [SerializeField]
    Slider HungerSlider;

    public void SetHungerAmount(int HungerValue) {
        HungerSlider.value = HungerValue;
    }

    public void SetMaxHunger(int HungerValue) {
        HungerSlider.maxValue = HungerValue;
        HungerSlider.value = HungerValue;
    }

    public void TurnOnVisual() {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
    }

    public void TurnOffVisual() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }
}
