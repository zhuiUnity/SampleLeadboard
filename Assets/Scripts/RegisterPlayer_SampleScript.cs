using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPlayer_SampleScript : MonoBehaviour {

	public Text displayNameInput, userNameInput, passwordInput; // these are set through the editor

    public GameObject success_text;
	public void RegisterPlayerBttn()
	{
		Debug.Log ("Registering Player...");
		new GameSparks.Api.Requests.RegistrationRequest ()
			.SetDisplayName (displayNameInput.text)
			.SetUserName (userNameInput.text)
			.SetPassword (passwordInput.text)
			.Send ((response) => {

					if(!response.HasErrors)
					{
						Debug.Log("Player Registered \n User Name: "+response.DisplayName);
                    StartCoroutine(SunccessRegister());
                        
					}
					else
					{
						Debug.Log("Error Registering Player... \n "+response.Errors.JSON.ToString());
					}

		});

	}
    IEnumerator SunccessRegister()
    {
        success_text.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        success_text.SetActive(false);
    }

}
