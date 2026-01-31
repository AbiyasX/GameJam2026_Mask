using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BushScript : MonoBehaviour
{
    private Material mat;
    private PlayerCamera cam;
    private float targetVisibility = 1;
    public float hideTime = 5;
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
            PlayerSystem ps = other.GetComponent<PlayerSystem>();
            StartCoroutine(PlayerHiding(ps));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisible(true);
            cam.zoomInCamera(false);
            other.GetComponent<PlayerSystem>().canPlayerSpotted = false;
        }
    }

    

    IEnumerator PlayerHiding(PlayerSystem ps)
    {
        yield return new WaitForSeconds(hideTime);
        Debug.Log("hidden");
        ps.canPlayerSpotted = false;
    }

}
