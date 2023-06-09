using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClickableObject : MonoBehaviour
{
    public GameObject uiObject;
    private ARRaycastManager raycastManager;
    private ARTrackedObjectManager trackedObjectManager;
    private bool isObjectClicked = false;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        trackedObjectManager = FindObjectOfType<ARTrackedObjectManager>();
        uiObject.SetActive(false);
    }

    void Update()
    {
        rotateYoshi();
        if (trackedObjectManager == null)
            return;

        if (!isObjectClicked && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit[] hits = new RaycastHit[1];
                if (Physics.Raycast(ray, out hits[0]))
                {
                    if (hits[0].collider.gameObject == gameObject)
                    {
                        isObjectClicked = true;
                        uiObject.SetActive(true);
                        gameObject.SetActive(false);
                        Invoke("ShowObject", 5f);
                    }
                }
            }
        }
    }

    void ShowObject()
    {
        uiObject.SetActive(false);
        gameObject.SetActive(true);
        isObjectClicked = false;
    }

    void rotateYoshi()
    {
        this.transform.Rotate(0, 1, 0);
    }
}
