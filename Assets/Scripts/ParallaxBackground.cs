using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private new GameObject camera;

    [SerializeField] private float parallaxEffect;
    
    private float xPosition;
    private float length;
    
    void Start()
    {
        camera = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = camera.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = camera.transform.position.x * parallaxEffect;
        
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        } else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }
    }
}
