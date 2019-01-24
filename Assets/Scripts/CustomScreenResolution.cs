using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Move to splash screen
public class CustomScreenResolution : MonoBehaviour
{
    private void Awake() {
        Screen.SetResolution(438, 700, FullScreenMode.Windowed);
    }
}
