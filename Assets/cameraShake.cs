using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour {

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        Debug.Log("CALLED");
        float elaped = 0.0f;

        while (elaped < duration)
        {
            float x = Random.Range(-1, 1f) * magnitude;
            float y = Random.Range(-1, 1f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, originalPos.z);

            elaped += Time.deltaTime;

            yield return null;
        }
    }

}
