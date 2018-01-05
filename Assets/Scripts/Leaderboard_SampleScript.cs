using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Leaderboard_SampleScript : MonoBehaviour 
{
	public Text scoreInput, entryCount, outputData,moneyInput;
	public GameObject highScorePopup;

	void Awake () 
	{
		GameSparks.Api.Messages.NewHighScoreMessage.Listener += HighScoreMessageHandler; // assign the New High Score message
	}

	void HighScoreMessageHandler (GameSparks.Api.Messages.NewHighScoreMessage _message)
	{
		Debug.Log ("NEW HIGH SCORE \n "+_message.LeaderboardName);
		highScorePopup.GetComponent<Popup>().CallPopup(_message);
	}

	public void PostScoreBttn()
	{
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("SAMPLE_SCORE")
            .SetEventAttribute("MONEY", moneyInput.text)
            .Send((response) =>
            {

                if (!response.HasErrors)
                {
                    Debug.Log("Money Posted Sucessfully...");
                }
                else
                {
                    Debug.Log("Error Posting Money...");
                }
            });
        new GameSparks.Api.Requests.LogEventRequest()
           .SetEventKey("SAMPLE_SCORE")
           .SetEventAttribute("SCORE", scoreInput.text)
           .Send((response) =>
           {

               if (!response.HasErrors)
               {
                   Debug.Log("Score  Posted Sucessfully...");
               }
               else
               {
                   Debug.Log("Error Posting Score...");
                   Debug.LogError(response.Errors);
               }
           });

        GetLeaderboard();
	}
    public void OnToAuthentionClick()
    {
        Application.LoadLevel(0);
    }
	public void GetLeaderboard()
	{
		Debug.Log ("Fetching Leaderboard Data...");

        new GameSparks.Api.Requests.LeaderboardDataRequest()
            .SetLeaderboardShortCode("LEADERBOARD_SCORE")
			.SetEntryCount(int.Parse(entryCount.text)) // we need to parse this text input, since the entry count only takes long
			.Send ((response) => {

					if(!response.HasErrors)
					{
           
						Debug.Log("Found Leaderboard Data...");
						outputData.text = System.String.Empty; // first clear all the data from the output
						foreach(GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data) // iterate through the leaderboard data
						{
							int rank = (int)entry.Rank; // we can get the rank directly
							string playerName = entry.UserName;
							string score = entry.JSONData["SCORE"].ToString();
                        string money = entry.JSONData["MONEY"].ToString();
                        // we need to get the key, in order to get the score
							outputData.text += rank+"   Name: "+playerName+"  Score:"+score +"  Money: "+money+"\n"; // addd the score to the output text
						}
					}
					else
					{
						Debug.Log("Error Retrieving Leaderboard Data...");
					}

		});
	}
}
















