using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HardLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject root;

    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image taskImage;

    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button imageButton;

    [SerializeField] private CanvasGroup copyToast;
    private Coroutine toastCoroutine;

    private Level currentLevel;
    private string clipboardData;

    public void Init(Level level)
    {
        currentLevel = level;
        currentLevel.OnRegenerated += HandleRegenerated;
    }

    public void Show()
    {
        if (currentLevel is IHardTaskProvider provider)
        {
            HardTask task = provider.GetHardTask();

            root.SetActive(true);

            descriptionText.text = task.Description;

            taskImage.sprite = task.Image;

            clipboardData = task.InputData;

            answerInput.text = "";
        }
        else
        {
            Debug.LogWarning("level is not a IHardTaskProvider");
        }
    }

    public void Hide()
    {
        if (currentLevel != null)
        {
            currentLevel.OnRegenerated -= HandleRegenerated;
        }

        root.SetActive(false);
    }

    private void Awake()
    {
        submitButton.onClick.AddListener(OnSubmit);
        skipButton.onClick.AddListener(OnSkip);
        imageButton.onClick.AddListener(OnImageClicked);
    }

    private void OnSubmit()
    {
        currentLevel.SubmitAnswer(answerInput.text);
    }

    private void OnSkip()
    {
        if (currentLevel is ISkippableLevel skippable)
        {
            skippable.SkipLevel();
        }
        else
        {
            Debug.Log("level is unskippable");
        }
    }

    private void OnImageClicked()
    {
        GUIUtility.systemCopyBuffer = clipboardData;

        if (toastCoroutine != null)
        {
            StopCoroutine(toastCoroutine);
        }

        toastCoroutine = StartCoroutine(
            ShowToastRoutine());
    }

    private IEnumerator ShowToastRoutine()
    {
        copyToast.gameObject.SetActive(true);

        copyToast.alpha = 1f;

        yield return new WaitForSeconds(1f);

        float t = 0;

        while (t < 0.5f)
        {
            t += Time.deltaTime;

            copyToast.alpha = Mathf.Lerp(1f, 0f, t / 0.5f);

            yield return null;
        }

        copyToast.gameObject.SetActive(false);
    }

    private void HandleRegenerated()
    {
        Show();
    }
}