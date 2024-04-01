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
        db = gameObject.AddComponent<DBService>();
        StartCoroutine(db.GetBearer(Credentials.dbUrl_test, Credentials.bearerId, OnTestnetBearer));
        StartCoroutine(db.GetBearer(Credentials.dbUrl_main, Credentials.bearerId, OnMainnetBearer));
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
        Message.text = "";
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
    /// Use this to directly claim token.
    /// </summary>
    public void ClaimTokenDirect()
    {
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
        // int _score = Models.Score;
        Models.Score = currentScore;
        Debug.Log(Models.Score);
        db = gameObject.AddComponent<DBService>();
        StartCoroutine(db.PutDailyScore(dbUrl, gameId, userId, Models.Score));
        asign = gameObject.AddComponent<AutosignerService>();
        StartCoroutine(asign.SubmitGameScore(Models.Auth, Models.Score));
        StartCoroutine(asign.SubmitGameScoreMainnet(Models.Auth, Models.Score));
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
            Message.text = "Loading";
            // Message.text = msg.ToString();
            asign = gameObject.AddComponent<AutosignerService>();
            StartCoroutine(asign.DistributeTokenMainnet(Models.Auth, Models.Score + Models.PrevScore, OnClaimReceived));
        }
        else if(result == "ERROR")
        {
            Message.text = "Error";
        }
        else
        {
            Models.Hash = result;
            Message.text = "Successfully claimed. Hash = " + Models.Hash;
            db = gameObject.AddComponent<DBService>();
            StartCoroutine(db.PutTokensRequest(Credentials.dbUrl, Credentials.GameId, Models.UserId, Models.Score + Models.PrevScore, result));
        }
    }

    private void OnLogin(string token)
    {
        if(Models.UserId == null){
            Message.text = "You're Not Logged In. Please Log In to save record and Claim Token";
        }
        Credentials.dbBearer = token;
        Debug.Log(Credentials.dbBearer);
        // StartCoroutine(db.GetCheckClaimStatus(Credentials.dbUrl, Credentials.GameId, Models.UserId, Credentials.autosignerUrl1, Credentials.autosignerUrl2));
        StartCoroutine(db.GetUserGameId(Credentials.dbUrl, Credentials.GameId, Models.UserId, OnGettingUserInfo));
    }

    private void OnGettingUserInfo(string response)
    {
        var objectResponse = JSON.Parse(response);
		int prevScore = objectResponse["b_Score"]["d_TotalScore"];
        Models.PrevScore = prevScore;
        Debug.Log(prevScore);
        string hash = objectResponse["c_TokensReq"]["b_TxnHash"];
        Models.Hash = hash;
        Debug.Log(hash);
        if(hash != null){
            StartCoroutine(asign.GetTransactionStatusTestnet(hash, OnTxnStatus));
            StartCoroutine(asign.GetTransactionStatusMainnet(hash, OnTxnStatus)); 
        }
        
    }

    private void OnTxnStatus(string response)
    {
        if(response == "False"){
            StartCoroutine(db.PutTokensSuccess(Credentials.dbUrl, Credentials.GameId, Models.UserId, "fail"));
        }else if(response == "True"){
            StartCoroutine(db.PutTokensSuccess(Credentials.dbUrl, Credentials.GameId, Models.UserId, "success"));
        }
        
    }
    private void OnTestnetBearer(string response)
    {
        // Debug.Log("Testnet Bearer: " + response);
        Credentials.autosignerTestnetBearer = response;
    }
    private void OnMainnetBearer(string response)
    {
        // Debug.Log("Mainnet Bearer: " + response);
        Credentials.autosignerMainnetBearer = response;
    }

}
