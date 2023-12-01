using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    WallDirections lookDirection = WallDirections.Unknown;
    // Start is called before the first frame update
    [SerializeField]
    List<WallStates> appliedStates;

    public WallDirections GetDirection() {
        return lookDirection;
    }

    public List<WallStates> GetAppliedStates() {
        return appliedStates;
    }
}
