using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    public static string Auth {get;set;} = "8723F3CE8EF35B83DC354FF3B7C84104A47A9FF91432646780F3A964AD54AC99976C544F8BC43346";
    // public static string Auth {get;set;}
    public static string UserId {get; set;}
    public static int TokensClaim {get;set;}
    public static int TokensReq {get;set;}
    public static string? Hash {get;set;}
    public static long HashTime {get;set;}
    public static int Score {get;set;} = 2;
}

public static class Credentials
{
    public static string autosignerBearer = "xar-K1yKcEICAWuh10ZylQOs-bearer-3AQ3fOPMueqBlJS7f7UH-admin-P1ESJbighL6htWzLvPwX";
    public static string dbBearer {get;set;}
    public static string autosignerUrl2 = "https://xar-autosigner-2.proximaxtest.com/api/v1";
    public static string autosignerUrl1 = "https://xar-autosigner.proximaxtest.com";
    // public static string dbUrl = "http://localhost:4000/api/v1";
    public static string dbUrl = "https://metx-games-api-1.vercel.app/api/v1";
    public static string TokenId {get;set;} = "0EC9B913C9580AAA";
    public static string mainnetTokenId {get; set;} = "6E732F998AD017C3";
    public static string GameId {get; set;} = "kk";
}

