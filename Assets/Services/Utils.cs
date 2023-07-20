using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// Tokenization Controller
public class Utils : MonoBehaviour
{
    string id;
    public GameObject ClaimButton;
    private AutosignerService asign;
    private DBService db;
    private Auth getAuth;

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
        // db = gameObject.AddComponent<DBService>();
        //Models.Auth = id;
    }
    public void UserLogin()
    {
        asign = gameObject.AddComponent<AutosignerService>();
        StartCoroutine(asign.GetUser(Models.Auth, OnUserReceived));
    }

    public void ClaimToken()
    {
        Debug.Log(Models.Hash);
        if(Models.Hash == null){
            asign = gameObject.AddComponent<AutosignerService>();
            StartCoroutine(asign.DistributeToken(Models.Auth, Models.Score, OnClaimReceived));
        }
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
            asign = gameObject.AddComponent<AutosignerService>();
            StartCoroutine(asign.DistributeTokenMainnet(Models.Auth, Models.Score, OnClaimReceived));
        }
        else
        {
            Models.Hash = result;
            db = gameObject.AddComponent<DBService>();
            StartCoroutine(db.PutTokensRequest(Credentials.dbUrl, Credentials.GameId, Models.UserId, Models.Score, result));
        }
    }

    private void OnLogin(string token)
    {
        Credentials.dbBearer = token;
        Debug.Log(Credentials.dbBearer);
        StartCoroutine(db.GetCheckClaimStatus(Credentials.dbUrl, Credentials.GameId, Models.UserId, Credentials.autosignerUrl2));
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
