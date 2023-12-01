using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChargeBar : MonoBehaviour
{
    [SerializeField]
    Slider chargeSlider;

    public void SetChargesAmount(int chargeValue) {
        chargeSlider.value = chargeValue;
    }

    public void SetMaxCharges(int chargeValue) {
        chargeSlider.maxValue = chargeValue;
        chargeSlider.value = chargeValue;
    }
}
