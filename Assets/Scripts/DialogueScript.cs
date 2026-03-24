using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [Header("Connecters")]
    [SerializeField] PhysicsSystem_Script ps;
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameObject DialogueBox;
    [SerializeField] Image LensImage;

    [Header("DialogueList")]
    [SerializeField] string[] startDi;
    [SerializeField] string[] lose;
    [SerializeField] string[] win;
    [SerializeField] int[] StartDialogueLensSprite;
    [SerializeField] int[] WinDialogueLensSprite;
    [SerializeField] int[] LoseDialogueLensSprite;

    [Header("Vars")]
    [SerializeField] Sprite[] LensSprites;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject Lens;
    [SerializeField] bool isActive;
    [SerializeField] float timer = 0;
    [SerializeField] bool isStart = true;
    [SerializeField] bool debug = false;

    private int currentStartIndex = 0;
    private int currentWinIndex = 0;
    private int currentLoseIndex = 0;

    private bool isStartDialogueActive = false;
    private bool isWinDialogueActive = false;
    private bool isLoseDialogueActive = false;

    private bool hasWinDialoguePlayed = false;
    private bool hasLoseDialoguePlayed = false;

    void Start()
    {
        // 👇 автопоиск LevelManager (если не назначен)
        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        startDi[13] = "$name = $_POST['name'];\n\n" +
        "if ($name == \"\") {\n" +
        "    echo \"Имя не указано\";\n" +
        "} else {\n" +
        "    echo \"Привет, \" . $name;\n" +
        "}";

        DialogueBox.SetActive(false);

        if (LensImage == null)
        {
            LensImage = GetComponent<Image>();
            if (LensImage == null && Lens != null)
            {
                LensImage = Lens.GetComponent<Image>();
            }
        }

        if (debug)
        {
            isStart = false;
            Debug.Log("DialogueScript: Debug mode enabled");
        }
    }

    void Update()
    {
        if (debug) return;

        if (isStart && !isStartDialogueActive && !isWinDialogueActive && !isLoseDialogueActive)
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                isStart = false;
                StartStartDialogue();
                timer = 0;
            }
        }

        if (ps.isWin && !hasWinDialoguePlayed && !isWinDialogueActive)
        {
            hasWinDialoguePlayed = true;
            StartWinDialogue();
        }

        if (ps.isLose && !hasLoseDialoguePlayed && !isLoseDialogueActive)
        {
            hasLoseDialoguePlayed = true;
            StartLoseDialogue();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isStartDialogueActive)
                ContinueStartDialogue();
            else if (isWinDialogueActive)
                ContinueWinDialogue();
            else if (isLoseDialogueActive)
                ContinueLoseDialogue();
        }
    }

    void StartStartDialogue()
    {
        if (debug) return;

        currentStartIndex = 0;
        isStartDialogueActive = true;
        DialogueBox.SetActive(true);
        UpdateDialogueText(startDi, StartDialogueLensSprite, currentStartIndex);
    }

    void StartWinDialogue()
    {
        if (debug) return;

        currentWinIndex = 0;
        isWinDialogueActive = true;
        DialogueBox.SetActive(true);
        UpdateDialogueText(win, WinDialogueLensSprite, currentWinIndex);
    }

    void StartLoseDialogue()
    {
        if (debug) return;

        currentLoseIndex = 0;
        isLoseDialogueActive = true;
        DialogueBox.SetActive(true);
        UpdateDialogueText(lose, LoseDialogueLensSprite, currentLoseIndex);
    }

    void ContinueStartDialogue()
    {
        currentStartIndex++;

        if (currentStartIndex >= startDi.Length)
        {
            isStartDialogueActive = false;
            DialogueBox.SetActive(false);
            currentStartIndex = 0;
        }
        else
        {
            UpdateDialogueText(startDi, StartDialogueLensSprite, currentStartIndex);
        }
    }

    void ContinueWinDialogue()
    {
        currentWinIndex++;

        if (currentWinIndex >= win.Length)
        {
            isWinDialogueActive = false;
            DialogueBox.SetActive(false);
            currentWinIndex = 0;

            Debug.Log("WIN -> NEXT LEVEL");

            if (levelManager != null)
                levelManager.nextLevel();
            else
                Debug.LogError("LevelManager not found!");
        }
        else
        {
            UpdateDialogueText(win, WinDialogueLensSprite, currentWinIndex);
        }
    }

    void ContinueLoseDialogue()
    {
        currentLoseIndex++;

        if (currentLoseIndex >= lose.Length)
        {
            isLoseDialogueActive = false;
            DialogueBox.SetActive(false);
            currentLoseIndex = 0;

            Debug.Log("LOSE -> RESTART");

            if (levelManager != null)
                levelManager.restartLevel();
            else
                Debug.LogError("LevelManager not found!");
        }
        else
        {
            UpdateDialogueText(lose, LoseDialogueLensSprite, currentLoseIndex);
        }
    }

    void UpdateDialogueText(string[] dialogueArray, int[] spriteIndices, int index)
    {
        if (dialogueArray != null && index < dialogueArray.Length)
        {
            text.text = dialogueArray[index];

            if (spriteIndices != null && index < spriteIndices.Length)
            {
                int spriteIndex = spriteIndices[index];
                if (spriteIndex >= 0 && spriteIndex < LensSprites.Length && LensImage != null)
                {
                    LensImage.sprite = LensSprites[spriteIndex];
                }
            }
        }
    }

    public void ResetDialogues()
    {
        isStartDialogueActive = false;
        isWinDialogueActive = false;
        isLoseDialogueActive = false;
        hasWinDialoguePlayed = false;
        hasLoseDialoguePlayed = false;
        isStart = true;
        timer = 0;
        currentStartIndex = 0;
        currentWinIndex = 0;
        currentLoseIndex = 0;
        DialogueBox.SetActive(false);
    }

    public void SetDebugMode(bool enabled)
    {
        debug = enabled;

        if (debug)
        {
            isStartDialogueActive = false;
            isWinDialogueActive = false;
            isLoseDialogueActive = false;
            DialogueBox.SetActive(false);
        }
    }
}