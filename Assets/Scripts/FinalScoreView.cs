using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreView : MonoBehaviour {

    void Start() {
        GetComponent<Text>().text = PlayerPrefs.GetInt(PredefinedStrings.PREF_SCORE).ToString();
    }
}
