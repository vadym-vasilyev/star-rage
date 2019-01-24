using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOwner : MonoBehaviour {

    [SerializeField] int health = 1;
  
    public delegate void HealthEnded();
    public event HealthEnded OnHealthEnded = delegate { };

    public void DecreaseHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            OnHealthEnded();
        }
    }
}
