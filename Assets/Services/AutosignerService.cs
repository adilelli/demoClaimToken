using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class AutosignerService : MonoBehaviour
{
    // Update Models.id
    public IEnumerator GetUser(string Auth, System.Action<string> callback)
    {

		UnityWebRequest request = UnityWebRequest.Get($"{Credentials.autosignerUrl2}/api/v1/users/{Auth}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerTestnetBearer);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log(Auth);
			Debug.LogError(request.error, this);
			string username = null;
			callback.Invoke(username);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			string username = jsonData["username"];
			Debug.Log(username);
			callback.Invoke(username);
        }
    }

	public IEnumerator GetUserMainnet(string Auth, System.Action<string> callback)
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
			callback.Invoke(username);
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
			callback.Invoke("ERROR");
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
            string hash = jsonData["viewModel"]["hash"];
            Debug.Log("Txn hash: " + hash);
			callback.Invoke(hash);
		}	
    }
// {{autosigner_url}}/api/v1/events/score?score=22&auth=5678F78D599ECB647E3D7C2834AE66444B6E51EFCF52359A686DE9FB2B237A8970BB7DE1AB927199&gameId=abcde
	public IEnumerator SubmitGameScore(string auth, int amount){
		UnityWebRequest request = UnityWebRequest.Post($"{Credentials.autosignerUrl2}/api/v1/events/score?score={amount}&auth={auth}&gameid={Credentials.GameId}", new WWWForm());
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerTestnetBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log(request.error);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
            string success = jsonData["success"];
            Debug.Log("Success: " + success);
		}	
    }
// https://xar-autosigner.proximaxtest.com/api/v1/events/score?score=0&auth=6D6ACF707F9999907E9CFC93AACCCE4B6473F2B87658348C922CC01F3F5D87CE8C7397E5A7DE7A69&gameId=notyet
	public IEnumerator SubmitGameScoreMainnet(string auth, int amount){
		UnityWebRequest request = UnityWebRequest.Post($"{Credentials.autosignerUrl1}/api/v1/events/score?score={amount}&auth={auth}&gameId={Credentials.GameId}", new WWWForm());
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerMainnetBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log(request.error);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
            string success = jsonData["success"];
            Debug.Log("Success: " + success);
		}	
    }
	// {{autosigner_url}}/api/v1/transactions/334AADA20DA1F28BFC23AA4708C0A8780D7A2BC7621ACCBD91CFC5385EDF9437
	public IEnumerator GetTransactionStatusMainnet(string hash, System.Action<string> callback)
    {
		Debug.Log("Mainnet Hash : " + hash);
		UnityWebRequest request = UnityWebRequest.Get($"{Credentials.autosignerUrl1}/api/v1/transactions/{hash}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerMainnetBearer);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error, this);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			string status = jsonData["success"];
			Debug.Log(status);
			callback.Invoke(status);
        }
    }

	public IEnumerator GetTransactionStatusTestnet(string hash, System.Action<string> callback)
    {
		Debug.Log("Testnet Hash : " + hash);
		UnityWebRequest request = UnityWebRequest.Get($"{Credentials.autosignerUrl2}/api/v1/transactions/{hash}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.autosignerTestnetBearer);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error, this);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			string status = jsonData["success"];
			Debug.Log(status);
			callback.Invoke(status);
        }
    }
}
