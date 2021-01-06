using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class JumpToHyperSpace : MonoBehaviour
{
    public static JumpToHyperSpace instance;

    [Header("Camera Setup")]
    public GameObject mainCam;
    [SerializeField] Vector3 camStartPos;
    [SerializeField] Vector3 camEndPos;

    [Header("Win UI Setup")]
    public Animator winScreenAnimator;
    public ScoreDisplay scoreDisplay;

    [Header("Post Process Setup")]
    public PostProcessVolume volume;
    public AnimationCurve effectCurve;
    public AnimationCurve fadeBackCurve;
    Bloom bloomLayer = null;
    LensDistortion lensLayer = null;
    DepthOfField depthLayer = null;

    [Header("Post Process Settings")]
    [SerializeField] float bloomStartVal = 0.5f;
    [SerializeField] float bloomEndVal = 50f;
    [SerializeField] float lensDistStartVal = 0f;
    [SerializeField] float lensDistEndVal = -100f;
    [SerializeField] float depthStartVal = 50f;
    [SerializeField] float depthEndVal = 300f;

    [Header("Effect Duration Settings")]
    [SerializeField] float effectDurationTime = 3f;
    [SerializeField] float effectFadeTime = 0.5f;


    #region Singleton
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    void Start()
    {
        if (volume == null)
            volume = FindObjectOfType<PostProcessVolume>();

        if (mainCam == null)
            mainCam = Camera.main.gameObject;

        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out lensLayer);
        volume.profile.TryGetSettings(out depthLayer);

        camStartPos = mainCam.transform.position;

        // Default camera end position. Can be changed in-editor.
        camEndPos = new Vector3(camStartPos.x, camStartPos.y, camStartPos.z + 35f);
    }

    // This is for testing please remove when done testing thanks
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartCoroutine(HyperspaceJump());
    //    }
    //}

    public IEnumerator HyperspaceJump()
    {
        bloomLayer.enabled.value = true;
        lensLayer.enabled.value = true;
        depthLayer.enabled.value = true;

        bloomLayer.intensity.value = bloomStartVal;
        lensLayer.intensity.value = lensDistStartVal;
        depthLayer.focalLength.value = depthStartVal;

        float t = 0f;

        while (t < effectDurationTime)
        {
            float interpolation = effectCurve.Evaluate(t);

            bloomLayer.intensity.value = Mathf.Lerp(bloomStartVal, bloomEndVal, interpolation);
            lensLayer.intensity.value = Mathf.Lerp(lensDistStartVal, lensDistEndVal, interpolation);
            depthLayer.focalLength.value = Mathf.Lerp(depthStartVal, depthEndVal, interpolation);

            t += Time.unscaledDeltaTime;

            yield return 0;
        }
        mainCam.GetComponent<CameraMultiTarget>().enabled = false;

        StartCoroutine(ReverseEffect());

        yield break;
    }

    IEnumerator ReverseEffect()
    {
        float t = 0f;

        while (t < effectFadeTime)
        {
            float interpolation = fadeBackCurve.Evaluate(t);

            bloomLayer.intensity.value = Mathf.Lerp(bloomEndVal, bloomStartVal, interpolation);
            lensLayer.intensity.value = Mathf.Lerp(lensDistEndVal, lensDistStartVal, interpolation);
            depthLayer.focalLength.value = Mathf.Lerp(depthEndVal, depthStartVal, interpolation);

            mainCam.transform.position = Vector3.Lerp(camStartPos, camEndPos, interpolation);

            t += Time.unscaledDeltaTime;

            yield return 0;
        }

        bloomLayer.intensity.value = bloomStartVal;
        lensLayer.intensity.value = lensDistStartVal;
        depthLayer.focalLength.value = depthStartVal;

        StartCoroutine(RevealWinScreen());

        yield break;
    }

    IEnumerator RevealWinScreen()
    {
        yield return new WaitForSeconds(1f);

        winScreenAnimator.transform.parent.gameObject.SetActive(true);
        winScreenAnimator.SetTrigger("Win");

        yield return new WaitForSeconds(2f);

        //StartCoroutine(scoreDisplay.FillCogs());

        yield break;
    }
}
