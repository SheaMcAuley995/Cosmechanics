using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutomaticScroll : MonoBehaviour, IUpdateSelectedHandler
{
	private static float SCROLL_MARGIN = 0.3f;

	private ScrollRect sr;

	public void Awake()
	{
		sr = this.gameObject.GetComponent<ScrollRect>();
	}

    public void OnUpdateSelected(BaseEventData eventData)
	{
		float contentHeight = sr.content.rect.height;
		float viewportHeight = sr.viewport.rect.height;

		float centerLine = eventData.selectedObject.transform.localPosition.y;
		float upperBound = centerLine + (eventData.selectedObject.GetComponent<RectTransform>().rect.height / 2f);
		float lowerBound = centerLine - (eventData.selectedObject.GetComponent<RectTransform>().rect.height / 2f);

		float lowerVisible = (contentHeight - viewportHeight) * sr.normalizedPosition.y - contentHeight;
		float upperVisible = lowerVisible + viewportHeight;

		float desiredLowerBound;
		if (upperBound > upperVisible)
		{
			desiredLowerBound = upperBound - viewportHeight + eventData.selectedObject.GetComponent<RectTransform>().rect.height * SCROLL_MARGIN;
		}
		else if (lowerBound < lowerVisible)
		{ 
			desiredLowerBound = lowerBound - eventData.selectedObject.GetComponent<RectTransform>().rect.height * SCROLL_MARGIN;
		}
		else
		{
			return;
		}

		float normalizedDesired = (desiredLowerBound + contentHeight) / (contentHeight - viewportHeight);
		sr.normalizedPosition = new Vector2(0f, Mathf.Clamp01(normalizedDesired));
	}
}
