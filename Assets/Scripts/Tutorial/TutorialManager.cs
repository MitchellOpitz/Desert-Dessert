using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [Header("Popup GameObjects")]
    [SerializeField]
    private GameObject gameDescriptionGameObject;

    [SerializeField]
    private GameObject drivingTestScene;

    [SerializeField]
    private GameObject serveControlsDisplayGameObject;

    [Header("References")]
    [SerializeField]
    private Camera mainCamera;

    [Header("Descriptions")]
    [Space(10)]
    [SerializeField]
    private TextMeshProUGUI serveControlsDescriptionText;

    private CanvasGroup drivingControlsCanvasGroup;
    private CanvasGroup serveControlsCanvasGroup;

    public bool hasDrivingControlPopupBeenDisplayed;
    private bool hasServeControlPopupBeenDisplayed;

    [SerializeField]
    private TutorialPlayerInput tutorialPlayerInput;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DisplayPopup(gameDescriptionGameObject);

        DisplayPopup(gameDescriptionGameObject);
    }

    private void OnEnable()
    {
        tutorialPlayerInput.OnAllKeysPressed += FadeOutDrivingTutorial;
        tutorialPlayerInput.OnMouseButtonClicked += FadeOutServeTutorial;
    }

    private void OnDisable()
    {
        tutorialPlayerInput.OnAllKeysPressed -= FadeOutDrivingTutorial;
        tutorialPlayerInput.OnMouseButtonClicked -= FadeOutServeTutorial;
    }

    private void LateUpdate()
    {
        if (!gameDescriptionGameObject.activeSelf && !hasDrivingControlPopupBeenDisplayed)
        {
            drivingTestScene.SetActive(true);
            mainCamera.GetComponent<CameraBasicFollow>().enabled = true;
            hasDrivingControlPopupBeenDisplayed = true;
        }
    }

    private void FadeOutDrivingTutorial()
    {
        if (tutorialPlayerInput.AllKeysPressed() && hasDrivingControlPopupBeenDisplayed)
        {
            drivingTestScene.SetActive(false);
            mainCamera.GetComponent<CameraBasicFollow>().enabled = false;
            StartCoroutine(FadeOutPopup(drivingControlsCanvasGroup, 4f));
        }
    }

    private void FadeOutServeTutorial()
    {
        if (tutorialPlayerInput.MouseButtonClicked() && hasServeControlPopupBeenDisplayed)
            StartCoroutine(FadeOutPopup(serveControlsCanvasGroup, 4f));
    }

    public void DisplayPopup(GameObject popup) => popup.SetActive(true);

    public void ClosePopup(GameObject popup) => popup.SetActive(false);

    private IEnumerator FadeOutPopup(CanvasGroup canvasGroup, float secondsBeforeFadeStarts)
    {
        yield return new WaitForSeconds(secondsBeforeFadeStarts);

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
