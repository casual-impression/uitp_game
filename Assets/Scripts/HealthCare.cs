using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCare : MonoBehaviour
{
    [SerializeField, Range(1, 5)]
    private int healthID;
    Image attachedImage;

    void Awake()
    {
        attachedImage = GetComponent<Image>();        
    }

    public void UpdateHeartCount(int leftLives) {
        if (leftLives >= healthID) {
            attachedImage.gameObject.SetActive(true);
        }
        else {
            attachedImage.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
