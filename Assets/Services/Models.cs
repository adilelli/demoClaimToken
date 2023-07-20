using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    public static string Auth {get;set;} = "5FC5B531D6D544FCEBE280E1FC74EAE9C23D3427A3E6F0CED2321280C95BE102BAD76EA4F5EFD89F";
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
    public static string GameId {get; set;} = "kk";
}

