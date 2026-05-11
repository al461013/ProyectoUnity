using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUI : MonoBehaviour
{
    private Label scoreLabel;

    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();

        var root = uiDocument.rootVisualElement;

        scoreLabel = root.Q<Label>("scoreLabel");

        Debug.Log(scoreLabel);

        if (scoreLabel == null)
        {
            Debug.LogError("No se encontró scoreLabel");
            return;
        }

        scoreLabel.text = "Score: 0";
    }

    private void Update()
    {
        Debug.Log("UI Update");

        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + ScoreManager.Instance.score;
        }
    }
}