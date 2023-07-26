using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;


// Tokenization Controller
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
        // db = gameObject.AddComponent<DBService>();
        // Models.Auth = id;
    }
    public void UserLogin()
    {
        asign = gameObject.AddComponent<AutosignerService>(); 
        Models.Score = 0;
        StartCoroutine(asign.GetUser(Models.Auth, OnUserReceived));
    }

    public void DecideClaim(){
        int toBeClaimed = Models.Score + Models.PrevScore;
        string _score = toBeClaimed.ToString();
        Points.text = _score;
        ConfirmClaim.SetActive(true);
        // ConfirmClaim.GetComponent<ConfirmClaim>().onClick.AddListener(DisableMenu);
    }

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

    public void Cancel()
    {
        ConfirmClaim.SetActive(false);
    }

    public void EndGameSession(string dbUrl, string gameId, string userId, int currentScore)
    {
        int _score = Models.Score;
        Models.Score = _score + currentScore;
        Debug.Log(Models.Score);
        db = gameObject.AddComponent<DBService>();
        StartCoroutine(db.PutDailyScore(dbUrl, gameId, userId, Models.Score));
    }

    private void OnUserReceived(string result)
    {
        if(result == null || result == "")
        {
            StartCoroutine(asign.GetUserMainnet(Models.Auth, OnUserReceived));
        }
        else
        {
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
        StartCoroutine(db.GetCheckClaimStatus(Credentials.dbUrl, Credentials.GameId, Models.UserId, Credentials.autosignerUrl2));
        StartCoroutine(db.GetUserGameId(Credentials.dbUrl, Credentials.GameId, Models.UserId, OnGettingUserInfo));
    }

    private void OnGettingUserInfo(string response)
    {
        var objectResponse = JSON.Parse(response);
		int prevScore = objectResponse["b_Score"]["d_TotalScore"];
        Models.PrevScore = prevScore;
        Debug.Log(prevScore);
    }

    private void DisableMenu()
    {
        ConfirmClaim.SetActive(false);
    }

    

    

    // private void OnHashReceived(string status)
    // {
    //     if(status == "true" )
    //     {
    //         int _claim = Models.TokensClaim;
    //         int _state = Models.TokensReq;
    //         Models.TokensClaim = _claim + _state;
    //         Models.TokensReq = 0;
    //     }else if(status == "false" && Models.HashTime - dateNow() > 3600)
    //     {
    //         int _score = Models.Score;
    //         int _state = Models.TokensReq;
    //         Models.Score = _state + _score;
    //         Models.TokensReq = 0;
    //     }
    //     //DB Post/Patch Claim, TokensReq, Score 
    // }

    // private static long dateNow (){
    //     DateTime currentDate = DateTime.UtcNow;
    //     DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    //     long epochTime = (long)(currentDate - epochStart).TotalSeconds;
    //     return epochTime;
    // }
}
