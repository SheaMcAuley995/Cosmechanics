using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportShader : MonoBehaviour
{
    [Header("Effect Settings")]

    [Tooltip("Time in seconds the effect takes to complete")]
    public float duration = 0.75f;
    [Tooltip("How much do you want it to glow as the effect increases?")]
    [SerializeField] float maxIntensity = 10f;
    [Tooltip("How much do you want the model to stretch vertically as the effect increases?")]
    [SerializeField] float maxStretchiness = -7f;

    [Tooltip("The color of the effect")]
    [SerializeField] Color color = new Color(0.1098f, 0.3215f, 0.7490f, 1);

    [Tooltip("The curve applied to the materialization effect")]
    [SerializeField] AnimationCurve materializeCurve;
    [Tooltip("The curve applied to the teleport effect")]
    [SerializeField] AnimationCurve teleportCurve;

    Renderer[] renderers;

    float effectProgress;
    float intensity;
    float stretchiness;
    float t = 0.0f;

    Coroutine teleportRoutine = null;
    Coroutine materializeRoutine = null;

    void Start()
    {
        // FOR TESTING
        //TeleportEffect();
        //MaterializeEffect();
    }

    public void TeleportEffect()
    {
        if (materializeRoutine != null)
        {
            StopCoroutine(materializeRoutine);
        }

        renderers = gameObject.GetComponentsInChildren<Renderer>();
        // This has to be done because, for some reason, the 1st character model is 1 mesh (head + body together)
        // while the others are 2 meshes (head + body separate).
        if (renderers.Length == 1)
        {
            duration = 0.5f;
        }
        else
        {
            duration = 0.75f;
        }

        foreach (Renderer rend in renderers)
        {
            if (rend != null)
            {
                Material[] mats = rend.materials;
                foreach (Material mat in mats)
                {
                    if (mat.shader != Shader.Find("Custom/Teleport"))
                    {
                        mat.shader = Shader.Find("Custom/Teleport");
                    }
                }
            }
        }
        //color = renderers[0].material.GetColor("_Emission");

        teleportRoutine = StartCoroutine(Teleportation());
    }

    IEnumerator Teleportation()
    {
        while (true)
        {
            t += Time.deltaTime / duration;
            effectProgress = Mathf.Lerp(0.0f, 1.0f, teleportCurve.Evaluate(t));
            intensity = Mathf.Lerp(0f, maxIntensity * 2, teleportCurve.Evaluate(t));
            stretchiness = Mathf.Lerp(-1f, maxStretchiness, teleportCurve.Evaluate(t));

            foreach (Renderer rend in renderers)
            {
                if (rend != null)
                {
                    rend.material.SetFloat("_DissolveAmount", effectProgress);
                    rend.material.SetColor("_Emission", (color * intensity));
                    rend.material.SetFloat("_Stretchiness", stretchiness);
                }
            }

            if (effectProgress >= 1.0f)
            {
                t = 0.0f;
                StopCoroutine(teleportRoutine);

                // Just in case StopCoroutine doesn't work
                yield return null;
                yield break;
            }

            // Wait until the next frame and then repeat
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void MaterializeEffect()
    {
        if (teleportRoutine != null)
        {
            StopCoroutine(teleportRoutine);
        }

        renderers = gameObject.GetComponentsInChildren<Renderer>();
        // This has to be done because, for some reason, the 1st character model is 1 mesh (head + body together)
        // while the others are 2 meshes (head + body separate).
        if (renderers.Length == 1)
        {
            duration = 0.5f;
        }
        else
        {
            duration = 0.75f;
        }

        foreach (Renderer rend in renderers)
        {
            if (rend != null)
            {
                Material[] mats = rend.materials;
                foreach (Material mat in mats)
                {
                    if (mat.shader != Shader.Find("Custom/Teleport"))
                    {
                        mat.shader = Shader.Find("Custom/Teleport");
                    }
                }
            }
        }
        //color = renderers[0].material.GetColor("_Emission");

        materializeRoutine = StartCoroutine(Materialization());
    }

    IEnumerator Materialization()
    {
        while (true)
        {
            t += Time.deltaTime / duration;
            effectProgress = Mathf.Lerp(1.0f, 0.0f, materializeCurve.Evaluate(t));
            intensity = Mathf.Lerp(maxIntensity, 0f, materializeCurve.Evaluate(t));
            stretchiness = Mathf.Lerp(maxStretchiness, -1f, materializeCurve.Evaluate(t));

            foreach (Renderer rend in renderers)
            {
                if (rend != null)
                {
                    rend.material.SetFloat("_DissolveAmount", effectProgress);
                    rend.material.SetColor("_Emission", (color * intensity));
                    rend.material.SetFloat("_Stretchiness", stretchiness);
                }
            }

            if (effectProgress <= 0.0f)
            {
                foreach (Renderer rend in renderers)
                {
                    if (rend != null)
                    {
                        Material[] mats = rend.materials;
                        foreach (Material mat in mats)
                        {
                            mat.shader = Shader.Find("Autodesk Interactive");
                        }
                    }
                }

                t = 0.0f;

                StopCoroutine(materializeRoutine);

                // Just in case StopCoroutine doesn't work
                yield return null;
                yield break;
            }

            // Wait until the next frame and then repeat
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
