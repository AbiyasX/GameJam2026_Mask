using UnityEngine;

public class BushScript : MonoBehaviour
{
    private Material mat;
    private PlayerCamera cam;
    private float targetVisibility = 1;
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        cam = FindAnyObjectByType<PlayerCamera>();
    }
    
    void Update()
    {
        float current = mat.GetFloat("_Float");
        float newValue = Mathf.Lerp(current, targetVisibility, Time.deltaTime * 8f);
        mat.SetFloat("_Float", newValue);
    }

    public void SetVisible(bool visible)
    {
        targetVisibility = visible ? 1f : 0.3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisible(false);
            cam.zoomInCamera(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisible(true);
            cam.zoomInCamera(false);

        }
    }
}
