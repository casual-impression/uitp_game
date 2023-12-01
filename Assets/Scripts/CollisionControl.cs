using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
        if (this.gameObject.tag == "Player") {
            if (col.gameObject.tag == "Enemy") {
                this.gameObject.GetComponent<Player>().HeartSubtract();
            }
            else if (col.gameObject.tag == "ExtraLife") {
                this.gameObject.GetComponent<Player>().HeartAdd();
                col.gameObject.GetComponent<Food>().selfDestroy();
            }
            else if (col.gameObject.tag == "Wall") {
                Wall givenWall = col.gameObject.GetComponent<Wall>();
                WallDirections givenDirection = givenWall.GetDirection();
                List<WallStates> givenStates = givenWall.GetAppliedStates();

                foreach (WallStates nextState in givenStates) {
                    if (nextState == WallStates.Clean) {
                        this.gameObject.GetComponent<PlayerController>().RespawnAtCheckpoint();
                    }
                    else if (nextState == WallStates.Stop) {
                        float xOffset = this.gameObject.transform.localScale.x * 0.5f;
                        float zOffset = this.gameObject.transform.localScale.z * 0.5f;
                        Vector3 pos = this.gameObject.transform.position;

                        if (givenDirection == WallDirections.Forward) {
                            this.gameObject.GetComponent<PlayerController>().RespawnAt(pos - Vector3.forward * zOffset);
                        }
                        else if (givenDirection == WallDirections.Right) {
                            this.gameObject.GetComponent<PlayerController>().RespawnAt(pos - Vector3.right * xOffset);
                        }
                        else if (givenDirection == WallDirections.Back) {
                            this.gameObject.GetComponent<PlayerController>().RespawnAt(pos - Vector3.back * zOffset);
                        }
                        else if (givenDirection == WallDirections.Left) {
                            this.gameObject.GetComponent<PlayerController>().RespawnAt(pos - Vector3.left * xOffset);
                        }
                    }
                }
            }
        }
        else if (this.gameObject.tag == "Wall") 
        {
            Wall givenWall = this.gameObject.GetComponent<Wall>();
            WallDirections givenDirection = givenWall.GetDirection();
            List<WallStates> givenStates = givenWall.GetAppliedStates();

            if (givenDirection == WallDirections.Unknown) {
                if (col.gameObject.tag == "Food") {
                    col.gameObject.GetComponent<Food>().Kill(col.gameObject);
                }
                else if (col.gameObject.tag == "Enemy") {
                    Destroy(col.gameObject);
                }
            }
        }
        else if (this.gameObject.tag == "Food") {
            if (col.gameObject.tag == "Enemy") {
                col.gameObject.GetComponent<Enemy>().DamageReceived(50);
                this.gameObject.GetComponent<Food>().Kill(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
