using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerChargeBar visualCharges;

    int hearts = 3;
    int charges = 3;
    int score;
    TextMeshProUGUI textScore;
    List<HealthCare> healthImageList = new List<HealthCare>();

    public GameObject currentFood;

    void Awake() {

    }

    public void HeartSubtract() {
        if (hearts > 0) {
            hearts -= 1;
            UpdateHeartBillboard();
        }
    }

    public void HeartAdd() {
        if (hearts < healthImageList.Count) {
            hearts += 1;
            UpdateHeartBillboard();
        }
    }

    void UpdateHeartBillboard() {
        foreach (HealthCare healthImage in healthImageList) {
            healthImage.UpdateHeartCount(hearts);
        }
    }

    void Start()
    {
        var foundHCObjects = FindObjectsOfType<HealthCare>();
        foreach (HealthCare obj in foundHCObjects) {
            healthImageList.Add(obj);
        }
        textScore = FindObjectOfType<TextMeshProUGUI>();
        UpdateHeartBillboard();
        visualCharges.SetMaxCharges(charges);
    }

    void UpdateScoreBillboard() {
        if (textScore != null) {
            textScore.text = score + "";
        }
    }

    public void AddScore(int scoreAmount) {
        score += scoreAmount;
        UpdateScoreBillboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PizzaShotInit() {
        if (charges > 0) {
            Instantiate(currentFood, transform.position, transform.rotation);
            charges--;
            visualCharges.SetChargesAmount(charges);
        }
    }

    public void PizzaShotClose(GameObject pizzaGO) {
        Destroy(pizzaGO);
        charges++;
        visualCharges.SetChargesAmount(charges);
    }
}
