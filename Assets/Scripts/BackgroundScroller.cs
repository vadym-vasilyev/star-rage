using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float backgroundSpeed = 0.5f;

    private Material backgroundTexture;

    void Start() {
        backgroundTexture = GetComponent<Renderer>().material;
    }

    void Update() {
        backgroundTexture.mainTextureOffset += Time.deltaTime * backgroundSpeed * Vector2.up;
    }
}
