using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using SimpleJSON;

public class DBService : MonoBehaviour
{
    public IEnumerator PutLogin(string apiUrl, string gameId, string userId, string tokenId, System.Action<string> callback)
    {
		JSONObject json = new JSONObject();
		json.Add("tokenID", tokenId);
		json.Add("user", userId);
		string jsonBody = json.ToString();
		Debug.Log(jsonBody);

		UnityWebRequest request = UnityWebRequest.Put($"{apiUrl}/{gameId}/login", jsonBody);
		request.SetRequestHeader("Content-Type", "application/json");

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
            string token = jsonData["viewModel"]["token"];  
            callback.Invoke(token);
		}
    } 
    
    public IEnumerator GetUserGameId(string apiUrl, string gameId, string userId, System.Action<string> callback)
    {
		UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}/{gameId}/{userId}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.dbBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			callback.Invoke(request.downloadHandler.text);
		}
    }   

	public IEnumerator GetBearer(string apiUrl, string bearerId, System.Action<string> callback)
    {
		UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}/{bearerId}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.dbBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
            string bearer = jsonData["bearerToken"];  
            callback.Invoke(bearer);
		}
    }   

    public IEnumerator GetCheckClaimStatus(string apiUrl, string gameId, string userId, string autosignerUrl1, string autosignerUrl2)
    {
		UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}/{gameId}/{userId}/claimstatus?autosigner_url={autosignerUrl2}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.dbBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
			StartCoroutine(GetCheckClaimStatusMainnet(apiUrl, gameId, userId, autosignerUrl1));
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			string message = jsonData["message"];
			Debug.Log(jsonData);
		}
    }      

	public IEnumerator GetCheckClaimStatusMainnet(string apiUrl, string gameId, string userId, string autosignerUrl1)
    {
		UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}/{gameId}/{userId}/claimstatus?autosigner_url={autosignerUrl1}");
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.dbBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			string message = jsonData["message"];
			Debug.Log(jsonData);
		}
    } 

    public IEnumerator PutDailyScore(string apiUrl, string gameId, string userId, int score)
    {
		UnityWebRequest request = UnityWebRequest.Put($"{apiUrl}/{gameId}/{userId}/dailyscore?score={score}&limit=2000", "");
        // Convert the request body to a byte array
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.dbBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			Debug.Log("success");
		}
    } 

    public IEnumerator PutTokensRequest(string apiUrl, string gameId, string userId, int score, string hash)
    {
		UnityWebRequest request = UnityWebRequest.Put($"{apiUrl}/{gameId}/{userId}/tokensreq?hash={hash}", "");
        // Convert the request body to a byte array
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.dbBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			Debug.Log("success, hash = " + hash);
		}
    } 
	//{{api_url}}/{{gameId}}/{{user}}/tokensclaim/success
	public IEnumerator PutTokensSuccess(string apiUrl, string gameId, string userId, string status)
    {
		UnityWebRequest request = UnityWebRequest.Put($"{apiUrl}/{gameId}/{userId}/tokensclaim/{status}", "");
        // Convert the request body to a byte array
		request.SetRequestHeader("Authorization", "Bearer " + Credentials.dbBearer);

		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogError(request.error);
		}
		else
		{
			var jsonData = JSON.Parse(request.downloadHandler.text);
			var success = jsonData["success"];
			Debug.Log("success");
		}
    } 

}

