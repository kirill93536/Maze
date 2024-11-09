using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject trap;

    public void DeactivateTrap() {
        if(trap == null) return;

        trap.SetActive(false);
    }
}
