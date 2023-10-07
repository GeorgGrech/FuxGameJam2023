using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class PointerUI : MonoBehaviour
{
    public Vector3 pointingTo;

    Vector3 screenPos;
    Vector2 onScreenPos;
    float max;
    private Camera mainCamera;

    private RectTransform rectTransform;// Transform of entire object

    private Image arrowImg;
    private bool shown = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        arrowImg = GetComponentInChildren<Image>();

    }

    private void Update()
    {
        //bool isOffScreen;

        screenPos = mainCamera.WorldToViewportPoint(pointingTo); //get viewport positions

        if (shown)
        {
            if (screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1)
            {
                //Debug.Log("already on screen, don't bother with the rest!");
                //gameObject.SetActive(false);
                arrowImg.enabled = false;
            }
            else
            {
                arrowImg.enabled = true;

                float borderSize = 175f;
                //gameObject.SetActive(true);

                RotatePointerTowardsTargetPosition();
                onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
                max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
                onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping

                float clampedX = Mathf.Clamp(onScreenPos.x * Screen.width, 0 + borderSize, Screen.width - borderSize);
                float clampedY = Mathf.Clamp(onScreenPos.y * Screen.height, 0 + borderSize, Screen.height - borderSize);
                Vector3 adjustedPosition = new Vector3(clampedX, clampedY, 0);
                rectTransform.position = adjustedPosition;
            }
        }
    }

    private void RotatePointerTowardsTargetPosition()
    {
        Vector3 adustedScreenPos = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height, 0);
        rectTransform.LookAt(adustedScreenPos);

        Vector3 toPosition = adustedScreenPos;
        Vector3 fromPosition = rectTransform.position; 
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        /*
        Debug.Log("toPosition: " + toPosition);
        Debug.Log("fromPosition: " + fromPosition);
        */

        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void Show(bool enable)
    {
        arrowImg.enabled = enable;
        shown = enable;
    }
}
