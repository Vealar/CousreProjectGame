using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlindnessEffect : MonoBehaviour
{
    public Light2D globalLight;
    

    public void ActivateBlindness(Light2D playerLight, float blindnessDuration)
    {
        StartCoroutine(BlindnessRoutine(playerLight, blindnessDuration));
    }

    private System.Collections.IEnumerator BlindnessRoutine(Light2D playerLight,float blindnessDuration)
    {
        Debug.Log(globalLight);
        if (globalLight != null)
           
            globalLight.intensity = 0.004f;

        if (playerLight != null)
            playerLight.enabled = true;

        yield return new WaitForSeconds(blindnessDuration);

        if (globalLight != null)
            globalLight.intensity = 1f;

        if (playerLight != null)
            playerLight.enabled = false;
    }
}