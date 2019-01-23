using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthOwner))]
//TODO: Separate script for collision processing?
public class EnemyShip : MonoBehaviour {

    private HealthOwner healthOwner;

    void Start() {
        healthOwner = GetComponent<HealthOwner>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        var damangeDealer = collision.gameObject.GetComponent<DamangeDealer>();
        if (damangeDealer) {
            healthOwner.DecreaseHealth(damangeDealer.DamangeAmount);
            Destroy(collision.gameObject);
        }
    }

}
