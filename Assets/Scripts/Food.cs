using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Player attachedPlayer;

    public void Kill(GameObject specificFoodItem) {
        attachedPlayer = FindObjectOfType<Player>();
        attachedPlayer.PizzaShotClose(specificFoodItem);
    }

    public void selfDestroy() {
        Destroy(this.gameObject);
    }

    IEnumerator LifeEnd(float ageDelay) {
        yield return new WaitForSeconds(ageDelay);
        selfDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeEnd(5.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
