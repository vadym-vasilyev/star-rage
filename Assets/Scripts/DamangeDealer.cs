using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamangeDealer : MonoBehaviour {
 
    [SerializeField] private int damangeAmount;

    public int DamangeAmount  { 
        get { return damangeAmount; }
        set { damangeAmount = value; }
    }
  
}
