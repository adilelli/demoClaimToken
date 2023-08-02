using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    public static string Auth {get;set;} = "F71D074FDC80B5E01C5E2F873FA8732E1B725F7C6C70437412A113F74B4223D8CD40BE4F18BDBFE4";
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
    public static string autosignerBearer = "xar-K1yKcEICAWuh10ZylQOs-bearer-3AQ3fOPMueqBlJS7f7UH-admin-P1ESJbighL6htWzLvPwX";
    public static string dbBearer {get;set;}
    public static string autosignerUrl2 = "https://xar-autosigner-2.proximaxtest.com";
    public static string autosignerUrl1 = "https://xar-autosigner.proximaxtest.com";
    public static string dbUrl {get; set;}
    public static string dbUrl_local = "http://localhost:4000/api/v1";
    public static string dbUrl_test = "https://metx-games-api-demo.vercel.app/api/v1";
    public static string dbUrl_main = "https://metx-games-api-1.vercel.app/api/v1";
    public static string TokenId {get;set;} = "1D629F5F9BA9D7A5";
    public static string mainnetTokenId {get; set;} = "6E732F998AD017C3";
    public static string GameId {get; set;} = "F250E6C24E9B943E";
    public static string testnetNode = "testnet";
    public static string mainnetNode = "mainnet";
}

