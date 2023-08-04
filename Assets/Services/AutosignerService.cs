using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class AutosignerService : MonoBehaviour
{
    // Update Models.id
    public IEnumerator GetUser(string Auth, System.Action<string, string> callback)
    {

		UnityWebRequest request = UnityWebRequest.Get($"{Credentials.autosignerUrl2}/api/v1/users/{Auth}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerTestnetBearer);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log(Auth);
			Debug.LogError(request.error, this);
			string username = null;
			callback.Invoke(username, Credentials.dbUrl_test);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			string username = jsonData["username"];
			Debug.Log(username);
			callback.Invoke(username, Credentials.dbUrl_test);
        }
    }

	public IEnumerator GetUserMainnet(string Auth, System.Action<string, string> callback)
    {

		UnityWebRequest request = UnityWebRequest.Get($"{Credentials.autosignerUrl1}/api/v1/users/{Auth}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerMainnetBearer);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log(Auth);
			Debug.LogError(request.error, this);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			string username = jsonData["username"];
			Debug.Log(username);
			callback.Invoke(username, Credentials.dbUrl_main);
        }
    }

    // Update Models.Hash
    // ClaimToken when hash is empty
    public IEnumerator DistributeToken(string auth, int amount, System.Action<string> callback){
		UnityWebRequest request = UnityWebRequest.Post($"{Credentials.autosignerUrl2}/api/v1/transactions/distribute?TokenId={Credentials.TokenId}&Amount={amount}&Auth={auth}", new WWWForm());
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerTestnetBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
			string hash = null;
			callback.Invoke(hash);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
            string hash = jsonData["viewModel"]["hash"];
            Debug.Log("Txn hash: " + jsonData);
			callback.Invoke(hash);
		}	
    }

	public IEnumerator DistributeTokenMainnet(string auth, int amount, System.Action<string> callback){
		UnityWebRequest request = UnityWebRequest.Post($"{Credentials.autosignerUrl1}/api/v1/transactions/distribute?TokenId={Credentials.mainnetTokenId}&Amount={amount}&Auth={auth}", new WWWForm());
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerMainnetBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
            string hash = jsonData["viewModel"]["hash"];
            Debug.Log("Txn hash: " + hash);
			callback.Invoke(hash);
		}	
    }

}
