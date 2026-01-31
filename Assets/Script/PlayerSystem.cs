using UnityEngine;
using UnityEngine.UI;
public class PlayerSystem : MonoBehaviour
{
    public float stamina;
    private float maxStamina = 100f;

    public float staminaDrainRate = 0.1f;
    public float staminaRegenRate = 5f;
    public float sprintDrainRate = 2f;

    public bool playerIsMasked = true;
    public bool isSprinting;

    public Slider staminaBar;

    private void Awake()
    {
        staminaBar.value = 0;
        stamina = maxStamina;
    }

    private void Update()
    {
        staminaDrain();
        staminaBarUpdate();
        Sprint();
        if (!playerIsMasked)
        {
            RegenerateStamina();
        }
    }

    public void staminaDrain()
    {
        if (!playerIsMasked) return;
        if (stamina <= 0f) return;
        stamina -= staminaDrainRate * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }
    public bool Sprint()
    {
        if (stamina <= 0f || !isSprinting) return false;
        Debug.Log("isSprinting");

        stamina -= sprintDrainRate * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        return true;
    }
    public void RegenerateStamina()
    {
        stamina += staminaRegenRate * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }

    void staminaBarUpdate()
    {

        staminaBar.value = Mathf.Lerp(staminaBar.value, stamina / maxStamina, Time.deltaTime * 8f);
    }
}
