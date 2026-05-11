using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoints(int points)
    {
        score += points;
        Debug.Log(score);
    }
}
