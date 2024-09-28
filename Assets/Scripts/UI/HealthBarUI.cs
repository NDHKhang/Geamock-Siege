using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    float lockX = 45;
    float lockY = 0;
    float lockZ = 0;

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(lockX, lockY, lockZ);
    }
}
