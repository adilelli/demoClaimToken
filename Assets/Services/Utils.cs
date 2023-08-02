using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// All of the relevant functions for Unity Front End are Here
/// </summary>

public class Utils : MonoBehaviour
{
    string id;
    public GameObject ApplyClaim;
    public GameObject ClaimButton;
    public GameObject ConfirmClaim;

    private AutosignerService asign;
    private DBService db;
    private Auth getAuth;
    public TMP_Text Points;
    public TMP_Text Message;

    void Awake()
    {
        Destroy(asign);
        Destroy(getAuth);
        Destroy(asign);
        Destroy(db);
    }

    private void Start()
    {
        getAuth = gameObject.AddComponent<Auth>();
        id = getAuth.GetAuthFromWebGL();
        if (id != null){
            Models.Auth = id;
        }
    }

    /// <summary>
    /// Use this at either when press play or at the start of the game 
    /// </summary>

    public void UserLogin()
    {
        asign = gameObject.AddComponent<AutosignerService>(); 
        Models.Score = 0;
        StartCoroutine(asign.GetUser(Models.Auth, OnUserReceived));
    }

    /// <summary>
    /// Use this for pop out for before claim token
    /// </summary>

    public void DecideClaim(){
        int toBeClaimed = Models.Score + Models.PrevScore;
        string _score = toBeClaimed.ToString();
        Points.text = _score;
        ConfirmClaim.SetActive(true);
    }

    /// <summary>
    /// Use this to claim token after the pop up.
    /// </summary>

    public void ClaimToken()
    {
        ConfirmClaim.SetActive(false);
        Debug.Log(Models.Hash);
        if(Models.Hash == null){
            asign = gameObject.AddComponent<AutosignerService>();
            int toBeClaimed = Models.Score + Models.PrevScore;
            StartCoroutine(asign.DistributeToken(Models.Auth, toBeClaimed, OnClaimReceived));
        }
    }
    /// <summary>
    /// Use this at to cancel claim during pop up.
    /// </summary>

    public void Cancel()
    {
        ConfirmClaim.SetActive(false);
    }

    /// <summary>
    /// Use this at the end of game to record gameplay.
    /// </summary>

    public void EndGameSession(string dbUrl, string gameId, string userId, int currentScore)
    {
        int _score = Models.Score;
        Models.Score = _score + currentScore;
        Debug.Log(Models.Score);
        db = gameObject.AddComponent<DBService>();
        StartCoroutine(db.PutDailyScore(dbUrl, gameId, userId, Models.Score));
    }

    private void OnUserReceived(string result, string node)
    {
        if(result == null || result == "")
        {
            StartCoroutine(asign.GetUserMainnet(Models.Auth, OnUserReceived));
        }
        else
        {
            Credentials.dbUrl = node;
            Debug.Log(Credentials.dbUrl);
            Models.UserId = result;
            db = gameObject.AddComponent<DBService>();
            StartCoroutine(db.PutLogin(Credentials.dbUrl, Credentials.GameId, Models.UserId, Credentials.TokenId, OnLogin));
        }
        
        // StartCoroutine(autosigner.GetTransactionHash(OnHashReceived));
    }

    private void OnClaimReceived(string result)
    {
        if(result == null || result == "")
        {
            Message.text = "Error.";
            asign = gameObject.AddComponent<AutosignerService>();
            StartCoroutine(asign.DistributeTokenMainnet(Models.Auth, Models.Score, OnClaimReceived));
        }
        else
        {
            Models.Hash = result;
            Message.text = "Successs apply for claim. Scan QR to complete transaction. hash: " + result;
            db = gameObject.AddComponent<DBService>();
            StartCoroutine(db.PutTokensRequest(Credentials.dbUrl, Credentials.GameId, Models.UserId, Models.Score, result));
        }
    }

    private void OnLogin(string token)
    {
        Credentials.dbBearer = token;
        Debug.Log(Credentials.dbBearer);
        StartCoroutine(db.GetCheckClaimStatus(Credentials.dbUrl, Credentials.GameId, Models.UserId, Credentials.autosignerUrl1, Credentials.autosignerUrl2));
        StartCoroutine(db.GetUserGameId(Credentials.dbUrl, Credentials.GameId, Models.UserId, OnGettingUserInfo));
    }

    private void OnGettingUserInfo(string response)
    {
        var objectResponse = JSON.Parse(response);
		int prevScore = objectResponse["b_Score"]["d_TotalScore"];
        Models.PrevScore = prevScore;
        Debug.Log(prevScore);
    }

}
