using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float backgroundSpeed = 0.5f;

    private Material backgroundTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundTexture = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        backgroundTexture.mainTextureOffset += Time.deltaTime * backgroundSpeed * Vector2.up;
    }
}
