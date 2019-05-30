using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWarning : MonoBehaviour {

    public GameObject tutorialIcon;
    public Vector3 position;
    private void Start()
    {
        tutorialIcon = Instantiate(tutorialIcon);
    }

    // Update is called once per frame
    void Update () {
        tutorialIcon.transform.position = transform.position + position;
        //tutorialIcon.transform.eulerAngles = new Vector3(0, 180, 0);
	}

    private void OnDestroy()
    {
        Destroy(tutorialIcon);
    }
}
