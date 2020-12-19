using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    [Header("Scene Transition Setup")]
    public PostProcessVolume volume;
    public Image image;
    public AnimationCurve curve;

    [Header("Post Processing Variables")]
    [SerializeField] float chromAbStartValue = 0f;
    [SerializeField] float chromAbEndValue = 1f;
    ChromaticAberration chromAbLayer = null;

    [Header("Transition Settings")]
    public float sceneFadeTime = 1f;

    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    void Start()
    {
        if (volume == null)
            volume = FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out chromAbLayer);

        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void FadeToQuit()
    {
        StartCoroutine(FadeOutToQuit());
    }

    IEnumerator FadeIn()
    {
        chromAbLayer.enabled.value = true;
        chromAbLayer.intensity.value = chromAbEndValue;

        float t = sceneFadeTime;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;

            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);

            float interploation = curve.Evaluate(t);
            chromAbLayer.intensity.value = Mathf.Lerp(chromAbStartValue, chromAbEndValue, interploation / sceneFadeTime);

            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        chromAbLayer.enabled.value = true;
        chromAbLayer.intensity.value = chromAbStartValue;

        float t = 0f;

        while (t < sceneFadeTime)
        {
            t += Time.unscaledDeltaTime;

            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);

            float interploation = curve.Evaluate(t);
            chromAbLayer.intensity.value = Mathf.Lerp(chromAbStartValue, chromAbEndValue, interploation / sceneFadeTime);

            yield return 0;
        }

        //SceneManager.LoadScene(scene);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeOutToQuit()
    {
        float t = 0f;

        while (t < sceneFadeTime)
        {
            t += Time.unscaledDeltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        Application.Quit();
    }
}
