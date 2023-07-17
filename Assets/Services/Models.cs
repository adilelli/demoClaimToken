using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    // public static string Auth {get;set;}= "D5DD97A23AB2D74A74E94A70A5DC9A132251BE7693558E7CAE9F4DCEFD232BBE326401C5E5E13196";
    public static string Auth {get;set;}
    public static string UserId {get; set;}
    public static int TokensClaim {get;set;}
    public static int TokensReq {get;set;}
    public static string? Hash {get;set;}
    public static long HashTime {get;set;}
    public static int Score {get;set;}
}

public static class Credentials
{
    public static string autosignerBearer = "xar-K1yKcEICAWuh10ZylQOs-bearer-3AQ3fOPMueqBlJS7f7UH-admin-P1ESJbighL6htWzLvPwX";
    public static string dbBearer {get;set;}
    public static string autosignerUrl = "https://xar-autosigner-2.proximaxtest.com/api/v1";
    public static string dbUrl = "http://localhost:4000/api/v1";
    public static string dbUrlCloud = "";
    public static string TokenId {get;set;} = "0EC9B913C9580AAA";
    public static string GameId {get; set;} = "F250E6C24E9B943E";
}

