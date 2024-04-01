using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{

    // public static string Auth {get;set;}
    public static string UserId {get; set;}
    public static int TokensClaim {get;set;}
    public static int TokensReq {get;set;}
    public static string? Hash {get;set;}
    public static long HashTime {get;set;}
    public static int Score {get;set;}
    public static int PrevScore {get;set;}
}

public static class Credentials
{
    public static string autosignerTestnetBearer {get;set;}
    public static string autosignerMainnetBearer {get;set;}
    public static string bearerId = "<<BEARER_ID>>";
    public static string dbBearer {get;set;}
    public static string autosignerUrl2 = "<<AUTOSIGNER_2>>";
    public static string autosignerUrl1 = "<<AUTOSIGNER_1>>";
    public static string dbUrl {get; set;}
    public static string dbUrl_local = "<<LOCAL_DB>>";
    public static string dbUrl_test = "<<TEST_DB>>";
    public static string dbUrl_main = "<<PRODUCTION_DB>>";
    public static string TokenId {get;set;} = "<<TOKEN_ID>>";
    public static string mainnetTokenId {get; set;} = "<<PRODUCTION_TOKEN_ID>>";
    public static string GameId {get; set;} = "<<GAME_ID>>";
    public static string testnetNode = "testnet";
    public static string mainnetNode = "mainnet";
}

