using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemFuel : MonoBehaviour
{
    /// <summary>
    /// Tutorial Article for the template of this code: https://gamedevacademy.org/educational-games-math-tutorial/ 
    /// </summary>
    public int stationId;  // identifier number for this fuel station
                        // called when something enters the fuel station's collider
    void OnTriggerEnter2D(Collider2D col)
    {
        // was it the player?
        if (col.CompareTag("Player"))
        {
            // tell the game manager that the player entered this station
            GameManager.instance.OnPlayerEnterFuelStation(stationId);
        }
    }
}
