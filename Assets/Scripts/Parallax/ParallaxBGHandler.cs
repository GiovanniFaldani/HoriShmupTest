using UnityEngine;

public class ParallaxBGHandler : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] float speedMultiplier = 10f;
    [SerializeField] float parallaxAnimationSpeed = 10f;

    Vector3 cameraPosition;
    Vector3 previousFrameCameraPosition;

    private void Awake()
    {
        cameraPosition = Camera.main.transform.position;
    }

    // Start is called 1 frame after Awake
    void Start()
    {
        previousFrameCameraPosition = cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = Camera.main.transform.position;
        for (int i = 0; i < layers.Length; i++)
        {
            float distanceTravelled =  previousFrameCameraPosition.x - cameraPosition.x;
            Vector3 parallaxTarget = new Vector3(layers[i].position.x + distanceTravelled * i * speedMultiplier, this.transform.position.y, this.transform.position.z);

            layers[i].position = Vector3.Lerp(layers[i].position, parallaxTarget, parallaxAnimationSpeed * Time.deltaTime);
        }
        previousFrameCameraPosition = cameraPosition;
        
    }
}
