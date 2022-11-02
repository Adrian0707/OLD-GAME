using UnityEngine;

public class Transfer : MonoBehaviour
{
    public Vector3 cameraChange;
    public Vector3 playerChange;
    private CameraMovment cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            collision.transform.position += playerChange;
        }
    }
}
