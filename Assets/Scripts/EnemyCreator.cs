using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] 
    List<GameObject> enemyTypes;
    
    int enemyTypeChoice;

    GameObject enemy;
    GameObject freshMeat;
    List<GameObject> enemiesToDestroy = new List<GameObject>();

    Vector3 enemyPos;
    Quaternion enemyRot;
    float[] posLimits = new float[6];
    List<EnemyDirections> directions = new List<EnemyDirections>() { 
        EnemyDirections.Left, 
        EnemyDirections.Right,
        EnemyDirections.Forward,
        EnemyDirections.Back
    };

    void SetPosLimits(float x0, float x1, float y0, float y1, float z0, float z1) {
        posLimits[0] = x0;
        posLimits[1] = x1;
        posLimits[2] = y0;
        posLimits[3] = y1;
        posLimits[4] = z0;
        posLimits[5] = z1;
    }

    IEnumerator enemySpawn(float spawnTime, int howMuch) {
        for (int i=0; i<howMuch; i++) {
            freshMeat = CreateEnemy();
            enemiesToDestroy.Add(freshMeat);
        }
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(enemySpawn(spawnTime, howMuch));
    }

    IEnumerator enemyDestroyer(float destroyTime, int howMuch) {
        yield return new WaitForSeconds(destroyTime);
        for (int i=0; i<howMuch; i++) {
            DestroyEnemy();
        }
        StartCoroutine(enemyDestroyer(destroyTime, howMuch));
    }

    GameObject CreateEnemy() {
        EnemyDirections nextDirection = directions[Random.Range(0, directions.Count)];

        if (nextDirection == EnemyDirections.Left) {
            SetPosLimits(25, 40, 0, 0, 15, 35);
        }
        else if (nextDirection == EnemyDirections.Right) {
            SetPosLimits(-40, -25, 0, 0, -15, 35);
        }
        else if (nextDirection == EnemyDirections.Forward) {
            SetPosLimits(-15, 15, 0, 0, -17.5f, -17.5f);
        }
        else if (nextDirection == EnemyDirections.Back) {
            SetPosLimits(-15, 15, 0, 0, 15, 35);
        }
        else {
            SetPosLimits(0, 0, 0, 0, 0, 0);
        }

        enemy = enemyTypes[Random.Range(0, enemyTypes.Count)];
        enemyPos = createNewPos(
            Random.Range(posLimits[0], posLimits[1]), 
            Random.Range(posLimits[2], posLimits[3]), 
            Random.Range(posLimits[4], posLimits[5]));
        enemyRot = createNewRot(nextDirection);

        return Instantiate(enemy, enemyPos, enemyRot);
    }

    Vector3 createNewPos(float xPos, float yPos, float zPos) {
        return new Vector3(xPos, yPos, zPos);
    }

    Quaternion createNewRot(EnemyDirections direction) {
        if (direction == EnemyDirections.Left) {
            return Quaternion.Euler(0, -90, 0);
        }
        else if (direction == EnemyDirections.Right) {
            return Quaternion.Euler(0, 90, 0);
        }
        else if (direction == EnemyDirections.Forward) {
            return Quaternion.Euler(0, 0, 0);
        }
        else if (direction == EnemyDirections.Back) {
            return Quaternion.Euler(0, 180, 0);
        }

        return Quaternion.identity;
    }

    void DestroyEnemy() {
        if (enemiesToDestroy.Count > 0) {
            Destroy(enemiesToDestroy[0]);
            enemiesToDestroy.Remove(enemiesToDestroy[0]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(enemySpawn(5.0f, 3));
        StartCoroutine(enemyDestroyer(10.0f, 6));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
