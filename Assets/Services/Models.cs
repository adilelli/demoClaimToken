using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    public static string Auth {get;set;} = "E6E1B1FB1D93BA7031D987361D7608866121332A273E7508294AC57DEE2E30DFE5230E9B35DEE855";
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
    public static string bearerId = "~ metx-secure";
    public static string dbBearer {get;set;}
    public static string autosignerUrl2 = "https://xar-autosigner-2.proximaxtest.com";
    public static string autosignerUrl1 = "https://xar-autosigner.proximaxtest.com";
    public static string dbUrl {get; set;}
    public static string dbUrl_local = "http://localhost:4000/api/v1";
    public static string dbUrl_test = "https://metx-games-api-demo.vercel.app/api/v1";
    public static string dbUrl_main = "https://metx-games-api-1.vercel.app/api/v1";
    public static string TokenId {get;set;} = "NYT";
    public static string mainnetTokenId {get; set;} = "NYT";
    public static string GameId {get; set;} = "BD00108F75C8CEF4";
    public static string testnetNode = "testnet";
    public static string mainnetNode = "mainnet";
}

