using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LeaderboardAPI : MonoBehaviour
{
    [Header("Supabase Settings")]
    [SerializeField] private string projectURL;
    [SerializeField] private string publishableKey;

    private string LeaderboardURL =>
        projectURL + "/rest/v1/leaderboard";

    private void Start()
    {
        GetLeaderboard();

        SubmitScore("TestPlayer", 100);
    }

    public void GetLeaderboard()
    {
        StartCoroutine(FetchLeaderboard());
    }

    private IEnumerator FetchLeaderboard()
    {
        using (UnityWebRequest request =
            UnityWebRequest.Get(LeaderboardURL + "?select=*&order=score.desc"))
        {
            request.SetRequestHeader("apikey", publishableKey);
            request.SetRequestHeader("Authorization", "Bearer " + publishableKey);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("LEADERBOARD DATA: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("LEADERBOARD ERROR: " + request.error);
                Debug.LogError(request.downloadHandler.text);
            }
        }
    }

    [System.Serializable]
    private class ScoreSubmission
    {
        public string p_player_name;
        public int p_score;
    }

    public void SubmitScore(string playerName, int score)
    {
        StartCoroutine(SendScore(playerName, score));
    }

    private IEnumerator SendScore(string playerName, int score)
    {
        ScoreSubmission data = new ScoreSubmission
        {
            p_player_name = playerName,
            p_score = score
        };

        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest request = new UnityWebRequest(
            projectURL + "/rest/v1/rpc/submit_leaderboard_score",
            "POST"))
        {
            byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("apikey", publishableKey);
            request.SetRequestHeader("Authorization", "Bearer " + publishableKey);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("SCORE SUBMITTED SUCCESSFULLY!");
            }
            else
            {
                Debug.LogError("SCORE SUBMISSION FAILED: " + request.error);
                Debug.LogError(request.downloadHandler.text);
            }
        }
    }
}