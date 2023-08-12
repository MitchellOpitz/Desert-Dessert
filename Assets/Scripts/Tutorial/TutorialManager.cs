using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Popup GameObjects")]
    [SerializeField] private GameObject gameDescriptionGameObject;
    [SerializeField] private GameObject drivingTestScene;
    [SerializeField] private GameObject drivingControlsDisplayGameObject;
    [SerializeField] private GameObject serveControlsDisplayGameObject;

    [Header("References")]
    [SerializeField] private Camera mainCamera;

    [Header("Descriptions")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI gameDescriptionText;
    [SerializeField] private TextMeshProUGUI drivingControlsDescriptionText;
    [SerializeField] private TextMeshProUGUI serveControlsDescriptionText;

    private CanvasGroup drivingControlsCanvasGroup;
    private CanvasGroup serveControlsCanvasGroup;

    public bool hasDrivingControlPopupBeenDisplayed;
    private bool hasServeControlPopupBeenDisplayed;

    private void Start()
    {
<<<<<<< Updated upstream
        gameDescriptionGameObject.SetActive(false);
        drivingControlsDisplayGameObject.SetActive(false);
        serveControlsDisplayGameObject.SetActive(false);
=======
        DisplayPopup(gameDescriptionGameObject); //do this when after transitioning scenes

        /*DisplayPopup(drivingControlsDisplayGameObject); //do this when closing game description popup
        hasDrivingControlPopupBeenDisplayed = true;

        DisplayPopup(serveControlsDisplayGameObject); //do this after serve scene transition
        hasServeControlPopupBeenDisplayed = true;
>>>>>>> Stashed changes

        drivingControlsCanvasGroup = drivingControlsDisplayGameObject.GetComponent<CanvasGroup>();
        serveControlsCanvasGroup = serveControlsDisplayGameObject.GetComponent<CanvasGroup>();*/
    }

    //change to events later on
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            DisplayPopup(gameDescriptionGameObject, gameDescriptionText);

        if (Input.GetKeyDown(KeyCode.L) && !hasDrivingControlPopupBeenDisplayed)
        {
            DisplayPopup(drivingControlsDisplayGameObject, drivingControlsDescriptionText);
            hasDrivingControlPopupBeenDisplayed = true;
        }

        if (Input.GetKeyDown(KeyCode.J) && !hasServeControlPopupBeenDisplayed)
        {
            DisplayPopup(serveControlsDisplayGameObject, serveControlsDescriptionText);
            hasServeControlPopupBeenDisplayed = true;
        }
            
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (hasDrivingControlPopupBeenDisplayed)
                StartCoroutine(FadeOutPopup(drivingControlsCanvasGroup));
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hasServeControlPopupBeenDisplayed)
                StartCoroutine(FadeOutPopup(serveControlsCanvasGroup));
        }
    }

<<<<<<< Updated upstream
    private void DisplayText(GameObject popup, string text)
    {
        if (popup != null)
            popup.GetComponentInChildren<TextMeshProUGUI>().text = text;
=======
    private void LateUpdate() {
        if(gameDescriptionGameObject.active == false && hasDrivingControlPopupBeenDisplayed == false) {
            DisplayPopup(drivingControlsDisplayGameObject);
            drivingTestScene.SetActive(true);
            mainCamera.GetComponent<CameraBasicFollow>().enabled = true;
            hasDrivingControlPopupBeenDisplayed = true;
        }
    }

    private void FadeOutDrivingTutorial()
    {
        if (tutorialPlayerInput.AllKeysPressed() && hasDrivingControlPopupBeenDisplayed) {
            drivingTestScene.SetActive(false);
            mainCamera.GetComponent<CameraBasicFollow>().enabled = false;
            StartCoroutine(FadeOutPopup(drivingControlsCanvasGroup, 4f));
        }
>>>>>>> Stashed changes
    }

    public void DisplayPopup(GameObject popup, TextMeshProUGUI popupText)
    {
        popup.SetActive(true);
        DisplayText(popup, popupText.text);
    }

    public void ClosePopup(GameObject popup) => popup.SetActive(false);

    private IEnumerator FadeOutPopup(CanvasGroup canvasGroup)
    {
        yield return new WaitForSeconds(2f);

        canvasGroup.alpha = 1f;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
