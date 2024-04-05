# NotYet



## Getting started

To make it easy for you to get started with GitLab, here's a list of recommended next steps.

Already a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## Testing Blockchain Game
To [test the game live](https://xarcade-gamer.proximaxtest.com/game-profile/C38D975E2587284D)
This game requires the deployment of gamesDB and autosigner. Please refer documentation to deploy autosigner locally.

```
cd existing_repo
git clone https://github.com/adilelli/demoClaimToken.git

Please go into models.cs and edit credentials. Replace all of the AUTOSIGNER with loccally deployed autosigner and locally run gamesDB to test.

public static string autosignerTestnetBearer {get;set;} = "<<BEARER_TOKEN>>"; //Please avoid hardcoding your bearer token
public static string autosignerMainnetBearer {get;set;} = "<<BEARER_TOKEN>>"; //Please avoid hardcoding your bearer token
public static string bearerId = "<<BEARER_ID>>"; //Ignore this
public static string autosignerUrl2 = "<<AUTOSIGNER_2>>"; //Just put your local autosigner url
public static string autosignerUrl1 = "<<AUTOSIGNER_1>>"; //Just put your local autosigner url like the autosignerUrl2 above
public static string TokenId {get;set;} = "<<TOKEN_ID>>"; //Refer documentation on creating token if you don't have one
public static string mainnetTokenId {get; set;} = "<<PRODUCTION_TOKEN_ID>>"; //Refer documentation on creating token if you don't have one
public static string GameId {get; set;} = "<<GAME_ID>>"; //Refer documentation on creating namespace if you don't have one

```

