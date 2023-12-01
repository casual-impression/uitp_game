using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCreator : MonoBehaviour
{
    [SerializeField]
    GameObject Bonus;
    GameObject NextCreatedBonus;
    List<GameObject> ExistingBonusList = new List<GameObject>();
    Coords posLimits;

    public struct Coords {
        public Coords(float x0, float x1, float y0, float y1, float z0, float z1) {
            Xmin = x0;
            Xmax = x1;
            Ymin = y0;
            Ymax = y1;
            Zmin = z0;
            Zmax = z1;
        }

        public float Xmin { get; }
        public float Xmax { get; }
        public float Ymin { get; }
        public float Ymax { get; }
        public float Zmin { get; }
        public float Zmax { get; }
    }

    GameObject CreateBonus() {
        float xPos = Random.Range(posLimits.Xmin, posLimits.Xmax);
        float yPos = Random.Range(posLimits.Ymin, posLimits.Ymax);
        float zPos = Random.Range(posLimits.Zmin, posLimits.Zmax);
        return Instantiate(Bonus, new Vector3(xPos, yPos, zPos), Quaternion.identity);
    }

    void DestroyBonus(GameObject ItemToDestroy) {
        Destroy(ItemToDestroy);
    }

    IEnumerator BonusSpawner(float timeDelay, int howMuch) {
        yield return new WaitForSeconds(timeDelay);
        for (int i=0; i<howMuch; i++) {
            NextCreatedBonus = CreateBonus();
            ExistingBonusList.Add(NextCreatedBonus);
        }
        StartCoroutine(BonusSpawner(timeDelay, howMuch));
    }

    IEnumerator BonusDestroyer(float timeDelay) {
        yield return new WaitForSeconds(timeDelay);
        if (ExistingBonusList.Count > 0) {
            DestroyBonus(ExistingBonusList[0]);
            ExistingBonusList.Remove(ExistingBonusList[0]);
        }
        StartCoroutine(BonusDestroyer(timeDelay));
    }


    // Start is called before the first frame update
    void Start()
    {
        posLimits = new Coords(-15, 15, 0, 0, -15, 30);
        StartCoroutine(BonusSpawner(5f, 2));
        StartCoroutine(BonusDestroyer(10f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
