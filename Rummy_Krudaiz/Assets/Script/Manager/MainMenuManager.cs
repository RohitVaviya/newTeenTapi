using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using WebSocketSharp;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using WebSocketSharp;
using System;
using System.Linq;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{

    public static MainMenuManager Instance;

    [Header("--- Home ---")]
    public Image avatarImg;
    public Text userNameTxt;
    public Text userIdTxt;
    public Text coinTxt;
    public Text diamondTxt;

    [Header("--- Prefab ---")]
    public GameObject prefabParent;
    public GameObject editProfilePrefab;
    public GameObject settingPrefab;
    public GameObject withdrawPrefab;
    public GameObject shopScreenPrefab;
    public GameObject contactUsPrefab;
    public GameObject tourErrorPrefab;
    public GameObject ludoLoadingPrefab;
    public GameObject tranHistoryPrefab;
    public GameObject refferalPrefab;
    public GameObject withdrawErrorPrefab;
    public GameObject fullScreenAd;


    [Header("--- Tournament ---")]
    public GameObject tournamentTeenPattiPrefab;
    public GameObject tournamentLudoPrefab;
    public GameObject ludoSelectorPrefab;
    //public List<TournamentData> tournamentData = new List<TournamentData>();

    //public GameObject settingUpdateObj;
    public int minPlayerRequired = 5;

    [Header("---Screen---")]
    public List<GameObject> screenObj = new List<GameObject>();

    [Header("---Notification List Panel---")]
    public GameObject notificationListPanel;
    public GameObject notificationRedDot;

    [Header("---Withdraw---")]
    public bool isWithdraw;

    public List<NotiBarManage> notiBarManages = new List<NotiBarManage>();
    
    public int botPlayers;

    bool isPressJoin;

    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


    }
    // Start is called before the first frame update
    [Obsolete("Obsolete")]
    void Start()
    {
        //GetVersionUpdate();
        UpdateAllData();
        //GetTournament();
        //GetTran();
        //GetBanner();
        
        DataManager.Instance.GetTournament();
        
    }


    public void UpdateAllData()
    {
        Getdata();
        //Getnotification();
    }
    // Update is called once per frame
    private void Update()
    {
        if (DataManager.Instance.tournamentData != null || DataManager.Instance.tournamentData.Count != 0) return;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Splash");

    }

    #region Home

    public void ProfileButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        GenerateEditProfile();
    }

    public void SettingButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        GenerateSetting();
    }

    public void CustomerButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        Application.OpenURL("mailto: " + "support@rummykrudaiz.in" + " ? subject = " + "subject" + " & body = " + "body");
        //GenerateContactUs();
    }

    public void MailButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        GenerateNotification();
    }

    public void VIPButtonClick()
    {

    }
    
    public void GameButtonClick(int no)
    {
        SoundManager.Instance.ButtonClick();
        switch (no)
        {
            case 1:
            {
                // Roulette
                print("************************Button is clicked ***********************");
                DataManager.Instance.gameMode = GameType.Roulette;
                string getTour = IsAvaliableSingleTournament(GameType.Roulette);
                print("Tournament availabe " + DataManager.Instance.tournamentData.Count);
                if (!string.IsNullOrEmpty(getTour))
                {
                    DataManager.Instance.tournamentID = getTour;
                    TestSocketIO.Instace.RouletteJoinroom();
                }
                else
                {
                    GenerateTournamentError();
                }

                break;
            }
            case 2:
            {
                DataManager.Instance.gameMode = GameType.Andar_Bahar;
                // Andar Bahar
                string getTour = IsAvaliableSingleTournament(GameType.Andar_Bahar);
                if (!string.IsNullOrEmpty(getTour))
                {
                    DataManager.Instance.tournamentID = getTour;
                    TestSocketIO.Instace.AndarBaharJoinroom();
                }

                //SceneManager.LoadScene(DataManager.Instance.GetModeToSceneName(DataManager.Instance.gameMode));
                //string getTour = IsAvaliableSingleTournament(GameType.Roulette);
                //if (getTour != null && getTour.Length != 0)
                //{
                //    DataManager.Instance.tournamentID = getTour;
                //    TestSocketIO.Instace.RouletteJoinroom();
                //}
                //else
                //{
                //    GenerateTournamentError();
                //}
                break;
            }
            case 3:
            {
                // Dragon Tiger
                DataManager.Instance.gameMode = GameType.Dragon_Tiger;
                string getTour = IsAvaliableSingleTournament(GameType.Dragon_Tiger);
                if (!string.IsNullOrEmpty(getTour))
                {
                    DataManager.Instance.tournamentID = getTour;
                    TestSocketIO.Instace.RouletteJoinroom();
                }
                else
                {
                    GenerateTournamentError();
                }

                break;
            }
            case 4:
                //DataManager.Instance.gameMode = GameType.Ludo;

                Instantiate(ludoSelectorPrefab, prefabParent.transform);
                //TournamentPanel.Instance.gameType = GameType.Ludo;
                //TournamentPanel.Instance.GenerateTournament();


                //string getTour = IsAvaliableSingleTournament(GameType.Ludo);
                //if (getTour != null && getTour.Length != 0)
                //{
                //    DataManager.Instance.tournamentID = getTour;//AA

                //    Screen.orientation = ScreenOrientation.Portrait;
                //    TestSocketIO.Instace.playTime = 2f;
                //    DataManager.Instance.playerNo = 0;
                //    DataManager.Instance.diceManageCnt = 0;
                //    DataManager.Instance.tourEntryMoney = 0;
                //    DataManager.Instance.winAmount = 0;
                //    //DataManager.Instance.tourBon
                //    TestSocketIO.Instace.LudoJoinroom();
                //}
                break;
            case 5:
            {
                // Teen Patti
                DataManager.Instance.gameMode = GameType.Teen_Patti;
                string getTour = IsAvaliableSingleTournament(GameType.Teen_Patti);
                if (!string.IsNullOrEmpty(getTour))
                {
                    DataManager.Instance.tournamentID = getTour;
                    TestSocketIO.Instace.TeenPattiJoinroom();
                }
                else
                {
                    GenerateTournamentError();
                }
            
                //Instantiate(tournamentTeenPattiPrefab, prefabParent.transform);
                //TournamentPanel.Instance.gameType = GameType.Teen_Patti;
                //TournamentPanel.Instance.GenerateTournament();
                break;
            }
            case 6:
            {
                // Poker
                DataManager.Instance.gameMode = GameType.Poker;
                //SceneManager.LoadScene(DataManager.Instance.GetModeToSceneName(DataManager.Instance.gameMode));
                string getTour = IsAvaliableSingleTournament(GameType.Poker);
                if (!string.IsNullOrEmpty(getTour))
                {
                    DataManager.Instance.tournamentID = getTour;
                    TestSocketIO.Instace.TeenPattiJoinroom();
                }
                else
                {
                    GenerateTournamentError();
                }

                break;
            }
        }
    }


    string IsAvaliableSingleTournament(GameType modeType)
    {
        for (int i = 0; i < DataManager.Instance.tournamentData.Count; i++)
        {
            if (DataManager.Instance.tournamentData[i].modeType == modeType)
            {
                return DataManager.Instance.tournamentData[i]._id;
            }
        }
        return null;
    }


    public void ShopButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        GenerateShop();
    }

    public void SafeButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        print("Balance : " + float.Parse(DataManager.Instance.playerData.balance));
        if (float.Parse(DataManager.Instance.playerData.balance) < 100)
        {
            GenerateWithdrawErrorPanel();
        }
        else
        {
            GenerateWithdraw();
        }
    }

    public void RankButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        GenerateHistroy();
    }

    public void ShareButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        //string shareTxt = "Download Latest StarX apk from Link Here : \n\n" + DataManager.Instance.appUrl + " \n\nUse this referral code :" + DataManager.Instance.playerData.refer_code;

        //new NativeShare().Share(shareTxt);
        GenerateRefferalPanel();
    }

    public void NoticeButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        GenerateNotification();
    }

    public void CoinButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        GenerateShop();
    }




    #endregion

    #region Prefab

    public void GenerateSetting()
    {
        Instantiate(settingPrefab, prefabParent.transform);
    }

    public void GenerateWithdraw()
    {
        Instantiate(withdrawPrefab, prefabParent.transform);
    }

    public void GenerateEditProfile()
    {
        Instantiate(editProfilePrefab, prefabParent.transform);
    }
    public void GenerateShop()
    {
        Instantiate(shopScreenPrefab, prefabParent.transform);
    }

    public void GenerateNotification()
    {
        Instantiate(notificationListPanel, prefabParent.transform);
    }

    public void GenerateContactUs()
    {
        Instantiate(contactUsPrefab, prefabParent.transform);
    }

    public void GenerateTournamentError()
    {
        Instantiate(tourErrorPrefab, prefabParent.transform);
    }
    public void GenerateHistroy()
    {
        Instantiate(tranHistoryPrefab, prefabParent.transform);
    }

    public void GenerateLoadingPanel()
    {
        Instantiate(ludoLoadingPrefab, prefabParent.transform);

    }

    public void GenerateRefferalPanel()
    {
        Instantiate(refferalPrefab, prefabParent.transform);

    }

    public void GenerateWithdrawErrorPanel()
    {
        Instantiate(withdrawErrorPrefab, prefabParent.transform);

    }
    #endregion
    
    
    #region PopUp Banner

    public void GetBanner()
    {
        StartCoroutine(GetBanners());
    }
    IEnumerator GetBanners()
    {

        UnityWebRequest request = UnityWebRequest.Get(DataManager.Instance.url + "/api/v1/players/banners");
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        yield return request.SendWebRequest();

        int gnerateBannerHomeCnt = 0;
        if (request.error == null && !request.isNetworkError)
        {
            print(request.downloadHandler.text);

            JSONNode values = JSON.Parse(request.downloadHandler.text.ToString());
            JSONNode data = JSON.Parse(values["data"].ToString());

            if (data.Count == 0)
            {
                print("There are no full screen banners");
            }
            else
            {
                int no = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i]["status"] != "active" || data[i]["location"] != "HOME" ||
                        data[i]["bannerType"] != "banner") continue;
                    {
                        GameObject fullScreenObj = Instantiate(fullScreenAd, prefabParent.transform);
                        string url = data[i]["url"];
                        no++;
                        FullScreenAd fullscreen = fullScreenObj.GetComponent<FullScreenAd>();
                        fullScreenObj.AddComponent<Button>().onClick.AddListener(() => BannerClick(url));
                        StartCoroutine(GetImages(data[i]["imageUrl"].ToString().Trim('"'), fullscreen.bannerImage));
                    }

                }
            }
        }
        else
        {
            Logger.log.Log(request.error.ToString());
        }
    }

    void BannerClick(string bannerUrl)
    {
        print("Banner URL : " + bannerUrl);
        if (bannerUrl != "" && bannerUrl != null)
        {
            Application.OpenURL(bannerUrl);
        }
    }
    
    IEnumerator GetImages(string URl, Image image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(URl);
        yield return request.SendWebRequest();

        if (request.error == null)
        {
            var texture = DownloadHandlerTexture.GetContent(request);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            if (image != null)
            {
                image.sprite = Sprite.Create(texture, rect, new Vector2(0, 0));
            }
        }
    }

    #endregion

    //#region API Calling

    #region Profile Data
    public void Getdata()
    {
        StartCoroutine(GetPlayerdata());
    }


    IEnumerator GetPlayerdata()
    {
        UnityWebRequest request = UnityWebRequest.Get(DataManager.Instance.url + "/api/v1/players/profile");
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        yield return request.SendWebRequest();

        if (request.error == null && !request.isNetworkError)
        {
            print("Data:" + request.downloadHandler.text);
            JSONNode values = JSON.Parse(request.downloadHandler.text.ToString());
            JSONNode data = JSON.Parse(values["data"].ToString());
            if (values["success"] == false)
            {
                DataManager.Instance.SetLoginValue("N");
                SceneManager.LoadScene("Splash");
                yield break;
            }
            Setplayerdata(data, true);
        }
        else
        {
            Logger.log.Log(request.error.ToString());
        }

    }

    public void SavePlayerProfile()
    {
        StartCoroutine(Profiledatasave());
    }

    IEnumerator Profiledatasave()
    {
        WWWForm form = new WWWForm();
        form.AddField("firstName", DataManager.Instance.playerData.firstName);
        form.AddField("lastName", DataManager.Instance.playerData.lastName);
        form.AddField("gender", DataManager.Instance.playerData.gender);
        form.AddField("email", DataManager.Instance.playerData.email);
        form.AddField("state", DataManager.Instance.playerData.state);
        form.AddField("panNumber", DataManager.Instance.playerData.panNumber);
        form.AddField("aadharNumber", DataManager.Instance.playerData.aadharNumber);
        form.AddField("dob", DataManager.Instance.playerData.dob);
        form.AddField("country", DataManager.Instance.playerData.country);
        UnityWebRequest request = UnityWebRequest.Post(DataManager.Instance.url + "/api/v1/players/profile", form);
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        //Datamanger.Intance.Avtar = Avtarint;
        yield return request.SendWebRequest();

        if (request.error == null && !request.isNetworkError)
        {
            JSONNode values = JSON.Parse(request.downloadHandler.text.ToString());
            print(request.downloadHandler.text);
            Logger.log.Log("Save Data", values.ToString());

            JSONNode datas = JSON.Parse(values["data"].ToString());
            //Debug.Log("User Data===:::" + datas.ToString());
            Setplayerdata(datas, false);
        }
        else
        {
            Logger.log.Log(request.error.ToString());
        }

    }

    public void Setplayerdata(JSONNode data, bool isGet)
    {
        Debug.Log("User Data===:::" + data.ToString());

        if (isGet)
        {
            if (data[nameof(DataManager.Instance.playerData.balance)] == "")
            {
                data[nameof(DataManager.Instance.playerData.balance)] = "";
            }
            DataManager.Instance.playerData.balance = ((float)data[nameof(DataManager.Instance.playerData.balance)]).ToString("F2");
            DataManager.Instance.playerData.kycStatus = data[nameof(DataManager.Instance.playerData.kycStatus)];
            if (data[nameof(DataManager.Instance.playerData.wonCount)] == "")
            {
                data[nameof(DataManager.Instance.playerData.wonCount)] = "";
            }
            DataManager.Instance.playerData.wonCount = data[nameof(DataManager.Instance.playerData.wonCount)];
            if (data[nameof(DataManager.Instance.playerData.joinCount)] == "")
            {
                data[nameof(DataManager.Instance.playerData.joinCount)] = "";
            }
            DataManager.Instance.playerData.joinCount = data[nameof(DataManager.Instance.playerData.joinCount)];
            DataManager.Instance.playerData.deposit = (data[nameof(DataManager.Instance.playerData.deposit)] * 10).ToString();
            DataManager.Instance.playerData.winings = (data[nameof(DataManager.Instance.playerData.winings)] * 10).ToString();
            DataManager.Instance.playerData.bonus = (data[nameof(DataManager.Instance.playerData.bonus)] * 10).ToString();
            DataManager.Instance.playerData._id = data[nameof(DataManager.Instance.playerData._id)];
            DataManager.Instance.playerData.phone = data[nameof(DataManager.Instance.playerData.phone)];
            DataManager.Instance.playerData.aadharNumber = data[nameof(DataManager.Instance.playerData.aadharNumber)];
            DataManager.Instance.playerData.refer_code = data[nameof(DataManager.Instance.playerData.refer_code)];
            DataManager.Instance.playerData.email = data[nameof(DataManager.Instance.playerData.email)];
            DataManager.Instance.playerData.firstName = data[nameof(DataManager.Instance.playerData.firstName)];
            DataManager.Instance.playerData.lastName = data[nameof(DataManager.Instance.playerData.lastName)];
            DataManager.Instance.playerData.gender = data[nameof(DataManager.Instance.playerData.gender)];
            DataManager.Instance.playerData.state = data[nameof(DataManager.Instance.playerData.state)];
            DataManager.Instance.playerData.createdAt = RemoveQuotes(data[nameof(DataManager.Instance.playerData.createdAt)].ToString());
            DataManager.Instance.playerData.countryCode = data[nameof(DataManager.Instance.playerData.countryCode)];

            string getName = data[nameof(DataManager.Instance.playerData.dob)];
            if (getName == "" || getName == null)
            {
                DataManager.Instance.playerData.dob = "none";
            }
            else
            {
                DataManager.Instance.playerData.dob = RemoveQuotes(data[nameof(DataManager.Instance.playerData.dob)]);
            }
            DataManager.Instance.playerData.panNumber = data[nameof(DataManager.Instance.playerData.panNumber)];
            DataManager.Instance.playerData.membership = "free";
            //DataManager.Instance.playerData.membership = data[nameof(DataManager.Instance.playerData.membership)];
            DataManager.Instance.playerData.avatar = DataManager.Instance.GetAvatarValue();
            DataManager.Instance.playerData.refer_count = data[nameof(DataManager.Instance.playerData.refer_count)];
            DataManager.Instance.playerData.refrer_level = data[nameof(DataManager.Instance.playerData.refrer_level)];
            DataManager.Instance.playerData.refrer_amount_total = data[nameof(DataManager.Instance.playerData.refrer_amount_total)];

            DataManager.Instance.playerData.refer_lvl1_count = data[nameof(DataManager.Instance.playerData.refer_lvl1_count)];
            DataManager.Instance.playerData.refer_vip_count = data[nameof(DataManager.Instance.playerData.refer_vip_count)];
            DataManager.Instance.playerData.refer_deposit_count = data[nameof(DataManager.Instance.playerData.refer_deposit_count)];
        }
        else
        {

            DataManager.Instance.playerData.email = data[nameof(DataManager.Instance.playerData.email)];
            DataManager.Instance.playerData.firstName = data[nameof(DataManager.Instance.playerData.firstName)];
            DataManager.Instance.playerData.lastName = data[nameof(DataManager.Instance.playerData.lastName)];
            DataManager.Instance.playerData.gender = data[nameof(DataManager.Instance.playerData.gender)];
            DataManager.Instance.playerData.panNumber = data[nameof(DataManager.Instance.playerData.panNumber)];
            DataManager.Instance.playerData.state = data[nameof(DataManager.Instance.playerData.state)];

            DataManager.Instance.playerData.aadharNumber = data[nameof(DataManager.Instance.playerData.aadharNumber)];
            DataManager.Instance.playerData.country = data[nameof(DataManager.Instance.playerData.country)];
            DataManager.Instance.playerData.dob = data[nameof(DataManager.Instance.playerData.dob)];
            DataManager.Instance.playerData.avatar = DataManager.Instance.GetAvatarValue();

            Getdata();

        }
        //print("Default Name : " + DataManager.Instance.GetDefaultPlayerName().Length);
        //print("Player Name : " + DataManager.Instance.playerData.firstName);
        coinTxt.text = DataManager.Instance.playerData.balance.ToString();
        if (DataManager.Instance.GetDefaultPlayerName().IsNullOrEmpty() && DataManager.Instance.playerData.firstName.IsNullOrEmpty())
        {
            print("Sub String : ");
            DataManager.Instance.SetDefaultPlayerName(DataManager.Instance.playerData.phone.Substring(0, 5));
            DataManager.Instance.playerData.firstName = DataManager.Instance.GetDefaultPlayerName();
        }
        else if (DataManager.Instance.playerData.firstName.IsNullOrEmpty())
        {
            DataManager.Instance.playerData.firstName = DataManager.Instance.GetDefaultPlayerName();
        }
        UserUpdateDisplayData();
        //TopBarDataSet();

    }

    #endregion

    #region Notification
    // public void Getnotification()
    // {
    //     StartCoroutine(GetNotifications());
    // }
    //
    // IEnumerator GetNotifications()
    // {
    //     UnityWebRequest request = UnityWebRequest.Get(DataManager.Instance.url + "/api/v1/notifications/player");
    //     request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
    //     yield return request.SendWebRequest();
    //
    //     if (request.error == null && !request.isNetworkError)
    //     {
    //         JSONNode value = JSON.Parse(request.downloadHandler.text.ToString());
    //         //print("Update Data : " + value.ToString())
    //         if (value.Count > 0)
    //         {
    //             notificationRedDot.SetActive(true);
    //         }
    //         else
    //         {
    //             notificationRedDot.SetActive(false);
    //         }
    //     }
    //
    // }


    /*
    #region Tournaments

    [Obsolete("Obsolete")]
    public void GetTournament()
    {
        StartCoroutine(GetTournaments());
    }
    [Obsolete("Obsolete")]
    IEnumerator GetTournaments()
    {
        //DataManager.Instance.tournamentData.Clear();
        UnityWebRequest request = UnityWebRequest.Get(DataManager.Instance.url + "/api/v1/players/tournaments");

        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        yield return request.SendWebRequest();

        if (request.error == null && !request.isNetworkError)
        {
            print("Tour Data : " + request.downloadHandler.text);

            JSONNode values = JSON.Parse(request.downloadHandler.text.ToString());
            JSONNode data = JSON.Parse(values["data"].ToString());

            for (int i = 0; i < data.Count; i++)
            {
                TournamentData t = new TournamentData
                {
                    bot = data[i]["bot"],
                    bonusAmountDeduction = data[i]["bonusAmountDeduction"],
                    active = data[i]["active"],
                    _id = data[i]["_id"],
                    name = data[i]["name"]
                };

                t.modeType = int.Parse(data[i]["mode"]) switch
                {
                    1 => GameType.Teen_Patti,
                    2 => GameType.Dragon_Tiger,
                    3 => GameType.Roulette,
                    4 => GameType.Poker,
                    5 => GameType.Andar_Bahar,
                    6 => GameType.Ludo,
                    _ => t.modeType
                };
                t.betAmount = data[i]["betAmount"];
                t.minBet = data[i]["minBet"];
                t.maxBet = data[i]["maxBet"];
                t.maxPayout = data[i]["maxPayout"];
                t.challLimit = data[i]["challLimit"];
                t.potLimit = data[i]["potLimit"];
                t.players = int.Parse(data[i]["players"]);
                t.winner = int.Parse(data[i]["winner"]);

                float winAmount = 0;
                for (int j = 0; j < data[i]["winnerRow"].Count; j++)
                {
                    t.winnerRow.Add(data[i]["winnerRow"][j]);
                    winAmount += data[i]["winnerRow"][j];
                }

                t.totalWinAmount = winAmount;


                t.time = float.Parse(data[i]["time"]);
                t.complexity = int.Parse(data[i]["complexity"]);
                t.interval = int.Parse(data[i]["interval"]);
                t._v = data[i]["__v"];
                t.createdAt = data[i]["createdAt"];
                t.updatedAt = data[i]["updatedAt"];
                if (t.active)
                {
                    DataManager.Instance.tournamentData.Add(t);
                }
            }

        }
        else
        {
            if (request.error != null) Logger.log.Log(request.error.ToString());
        }

    }

    #endregion
    */


    #region Transaction
    public void GetTran()
    {
        StartCoroutine(GetTrans());
    }

    IEnumerator GetTrans()
    {
        UnityWebRequest request = UnityWebRequest.Get(DataManager.Instance.url + "/api/v1/transactions/player");
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        yield return request.SendWebRequest();
        if (request.error == null && !request.isNetworkError)
        {
            JSONNode keys = JSON.Parse(request.downloadHandler.text.ToString());
            JSONNode data = JSON.Parse(keys["data"].ToString());

            JSONNode value = JSON.Parse(request.downloadHandler.text.ToString());
            if (value.Count > 0)
            {
                //isWithdraw = true;
            }
            else
            {
                isWithdraw = false;
            }

            for (int i = 0; i < value["data"].Count; i++)
            {

                //byte[] st = Encoding.ASCII.GetBytes(value["data"][i]["title"].ToString().Trim('"'));
                //string data = Encoding.UTF8.GetString(st);



                string paymentStatus = value["data"][i]["paymentStatus"];
                string logType = value["data"][i]["logType"];
                //string _id = value["data"][i]["_id"];
                //string amount = value["data"][i]["amount"];
                //string transactionType = value["data"][i]["transactionType"];
                //string note = value["data"][i]["note"];
                //string createdAt = value["data"][i]["createdAt"];



                if (logType == "withdraw" && paymentStatus == "PROCESSING")
                {
                    isWithdraw = true;
                }

            }
        }

        #endregion
    }

    #endregion

    #region Common

    public void UserUpdateDisplayData()
    {

        if (DataManager.Instance.playerData.firstName.IsNullOrEmpty())
        {
            userNameTxt.text = DataManager.Instance.GetDefaultPlayerName();
        }
        else
        {
            userNameTxt.text = DataManager.Instance.playerData.firstName;
        }
        if (DataManager.Instance.playerData.email.Length > 14)
        {
            userIdTxt.text = DataManager.Instance.playerData.email.Substring(0, 14) + "...";
        }
        else
        {
            userIdTxt.text = DataManager.Instance.playerData.email;
        }

        StartCoroutine(DataManager.Instance.GetImages(PlayerPrefs.GetString("ProfileURL"), avatarImg));
    }

    public string RemoveQuotes(string s)
    {
        string str = s;
        string newstr = str.Replace("\"", "");
        return newstr;
        
    }
    #endregion

    #region Load Bot

    public void CheckPlayers()
    {
        botPlayers = minPlayerRequired - DataManager.Instance.joinPlayerDatas.Count;
        if (DataManager.Instance.joinPlayerDatas.Count <= minPlayerRequired)
        {
            int[] avatars = Enumerable.Range(0, BotManager.Instance.botUser_Profile_URL.Count).ToArray();
            avatars.Shuffle();
            int[] randomAvatars = avatars.Take(botPlayers).ToArray();
        
            int[] names = Enumerable.Range(0, BotManager.Instance.botUserName.Count).ToArray();
            names.Shuffle();
            int[] randomNames = names.Take(botPlayers).ToArray();
            
            for (int i = 0; i < botPlayers; i++)
            {
                string avatar =
                    BotManager.Instance.botUser_Profile_URL[randomAvatars[i]];
                string botUserName =
                    BotManager.Instance.botUserName[randomNames[i]];
                string userId = DataManager.Instance.joinPlayerDatas[i].userId
                    .Substring(0, DataManager.Instance.joinPlayerDatas[i].userId.Length - 1) + "TeenPatti";
                DataManager.Instance.AddRoomUser(userId, botUserName,
                    DataManager.Instance.joinPlayerDatas[i].lobbyId,
                    10.ToString(), i , avatar);
            }
        }
    }
    

    #endregion

    #region Ludo Other Maintain


    public void OpenTournamentLoadScreen(Text t)
    {
        DataManager.Instance.SetPlayedGame(DataManager.Instance.GetPlayedGame() + 1);
        if (DataManager.Instance.isTwoPlayer)
        {
            print("Data Manager Join Player Count : " + DataManager.Instance.joinPlayerDatas.Count);
            if (DataManager.Instance.joinPlayerDatas.Count == 2)
            {
                StartCoroutine(LoadScene());
                //GenerateLoadingPanel();
            }
            else if (DataManager.Instance.joinPlayerDatas.Count == 1 &&
                     BotManager.Instance.isBotAvalible) // && DataManager.Instance.gameType!="Game")
            {
                //print("Enter The Bot Connect");
                //print("Enter The Condition");
                int playerNo = 3;


                string avatar =
                    BotManager.Instance.botUser_Profile_URL[
                        UnityEngine.Random.Range(0, BotManager.Instance.botUser_Profile_URL.Count)];
                string botUserName =
                    BotManager.Instance.botUserName[UnityEngine.Random.Range(0, BotManager.Instance.botUserName.Count)];
                string userId = DataManager.Instance.joinPlayerDatas[0].userId
                    .Substring(0, DataManager.Instance.joinPlayerDatas[0].userId.Length - 1) + "Ludo";
                DataManager.Instance.AddRoomUser(userId, botUserName, DataManager.Instance.joinPlayerDatas[0].lobbyId,
                    10.ToString(), playerNo, avatar);


                BotManager.Instance.isConnectBot = true;
                int rnoInd = UnityEngine.Random.Range(0, 2);
                //int rnoInd = 0;
                print("rnoInd : " + rnoInd);
                if (rnoInd == 0)
                {
                    DataManager.Instance.playerNo = 3;

                    JoinPlayerData joinplayerData1 = DataManager.Instance.joinPlayerDatas[0];
                    JoinPlayerData joinplayerData2 = DataManager.Instance.joinPlayerDatas[1];


                    string userId1 = joinplayerData1.userId;
                    string userName1 = joinplayerData1.userName;
                    string balance1 = joinplayerData1.balance;
                    string avtar1 = joinplayerData1.avtar;
                    //print("Join Player Data 1 : " + joinplayerData1.userName);
                    //print("Join Player Data 2 : " + joinplayerData2.userName);
                    DataManager.Instance.joinPlayerDatas[0].userId = joinplayerData2.userId;
                    DataManager.Instance.joinPlayerDatas[0].userName = joinplayerData2.userName;
                    DataManager.Instance.joinPlayerDatas[0].balance = joinplayerData2.balance;
                    DataManager.Instance.joinPlayerDatas[0].playerNo = 1;
                    DataManager.Instance.joinPlayerDatas[0].avtar = joinplayerData2.avtar;

                    DataManager.Instance.joinPlayerDatas[1].userId = userId1;
                    DataManager.Instance.joinPlayerDatas[1].userName = userName1;
                    DataManager.Instance.joinPlayerDatas[1].balance = balance1;
                    DataManager.Instance.joinPlayerDatas[1].playerNo = 3;
                    DataManager.Instance.joinPlayerDatas[1].avtar = avtar1;
                    BotManager.Instance.isConnectBot = true;
                    StartCoroutine(LoadScene());
                    //GenerateLoadingPanel();
                }
                else
                {
                    BotManager.Instance.isConnectBot = true;
                    //GenerateLoadingPanel();
                    StartCoroutine(LoadScene());
                }
            }
            else if (DataManager.Instance.joinPlayerDatas.Count == 1)
            {
                DataManager.Instance.tournamentID = "";
                DataManager.Instance.tourEntryMoney = 0;
                DataManager.Instance.tourCommision = 0;
                DataManager.Instance.commisionAmount = 0;
                DataManager.Instance.orgIndexPlayer = 0;
                DataManager.Instance.joinPlayerDatas.Clear();
                isPressJoin = false;

                t.text = "JOIN";
                TestSocketIO.Instace.roomid = "";
                TestSocketIO.Instace.userdata = "";
                TestSocketIO.Instace.playTime = 0;
                TestSocketIO.Instace.LeaveRoom();
                GenerateTournamentError();
            }

            //if (DataManager.Instance.joinPlayerDatas.Count == 2)
            //{
            //    GenerateLoadingPanel();
            //}
            //else if(DataManager.Instance.joinPlayerDatas.Count==1)
            //{
            //    DataManager.Instance.tournamentID = "";
            //    DataManager.Instance.tourEntryMoney = 0;
            //    DataManager.Instance.tourCommision = 0;
            //    DataManager.Instance.commisionAmount = 0;
            //    DataManager.Instance.orgIndexPlayer = 0;
            //    DataManager.Instance.joinPlayerDatas.Clear();
            //    t.text = "JOIN";
            //    isPressJoin = false;
            //    TestSocketIO.Instace.roomid = "";
            //    TestSocketIO.Instace.userdata = "";
            //    TestSocketIO.Instace.playTime = 0;
            //    Instantiate(tournamentErrorObj, parentObj.transform);

            //}
            //else if(DataManager.Instance.joinPlayerDatas.Count == 1)
            //{

            //}
        }
        else if (DataManager.Instance.isFourPlayer)
        {
            int maxPlayer = 4;
            int playerRequired = maxPlayer - DataManager.Instance.joinPlayerDatas.Count;
            print("Data Manager Join Player Count : " + DataManager.Instance.joinPlayerDatas.Count);
            
            if (DataManager.Instance.joinPlayerDatas.Count == 4)
            { 
                StartCoroutine(LoadScene());
               //GenerateLoadingPanel();
            }
            else if (DataManager.Instance.joinPlayerDatas.Count is 1 or 2 or 3 &&
                     BotManager.Instance.isBotAvalible) // && DataManager.Instance.gameType!="Game")
            {
                //print("Enter The Bot Connect");
                //print("Enter The Condition");
                // for assigning player number
                DataManager.Instance.playerNo = DataManager.Instance.joinPlayerDatas.Count switch
                {
                    2 => 3,
                    3 => 2,
                    1 => 1,
                    _ => DataManager.Instance.playerNo
                };

                for (int i = 0; i < playerRequired; i++)
                {
                    int playerNo = i + 2;
                    string avatar =
                        BotManager.Instance.botUser_Profile_URL[
                            UnityEngine.Random.Range(0, BotManager.Instance.botUser_Profile_URL.Count)];
                    string botUserName =
                        BotManager.Instance.botUserName[
                            UnityEngine.Random.Range(0, BotManager.Instance.botUserName.Count)];
                    string userId = DataManager.Instance.joinPlayerDatas[i].userId
                        .Substring(0, DataManager.Instance.joinPlayerDatas[i].userId.Length - 1) + "Ludo";
                    DataManager.Instance.AddRoomUser(userId, botUserName,
                        DataManager.Instance.joinPlayerDatas[i].lobbyId,
                        10.ToString(), playerNo, avatar);
                
                }
                

                BotManager.Instance.isConnectBot = true;
                //int rnoInd = UnityEngine.Random.Range(0, 2);
                int rnoInd = 0;
                print("rnoInd : " + rnoInd);
                if (rnoInd == 0)
                {
                    print("This is the assigned player number -> " + DataManager.Instance.playerNo);

                    JoinPlayerData joinplayerData1 = DataManager.Instance.joinPlayerDatas[0];
                    JoinPlayerData joinplayerData2 = DataManager.Instance.joinPlayerDatas[1];
                    JoinPlayerData joinplayerData3 = DataManager.Instance.joinPlayerDatas[2];
                    JoinPlayerData joinplayerData4 = DataManager.Instance.joinPlayerDatas[3];

                    string userId1 = joinplayerData1.userId;
                    string userName1 = joinplayerData1.userName;
                    string balance1 = joinplayerData1.balance;
                    string avtar1 = joinplayerData1.avtar;
                    //print("Join Player Data 1 : " + joinplayerData1.userName);
                    //print("Join Player Data 2 : " + joinplayerData2.userName);
                    DataManager.Instance.joinPlayerDatas[0].userId = userId1;
                    DataManager.Instance.joinPlayerDatas[0].userName = userName1;
                    DataManager.Instance.joinPlayerDatas[0].balance = balance1;
                    DataManager.Instance.joinPlayerDatas[0].playerNo = 1;
                    DataManager.Instance.joinPlayerDatas[0].avtar = avtar1;

                    DataManager.Instance.joinPlayerDatas[1].userId = joinplayerData2.userId;
                    DataManager.Instance.joinPlayerDatas[1].userName = joinplayerData2.userName;
                    DataManager.Instance.joinPlayerDatas[1].balance = joinplayerData2.balance;
                    DataManager.Instance.joinPlayerDatas[1].playerNo = 2;
                    DataManager.Instance.joinPlayerDatas[1].avtar = joinplayerData2.avtar;

                    DataManager.Instance.joinPlayerDatas[2].userId = joinplayerData3.userId;
                    DataManager.Instance.joinPlayerDatas[2].userName = joinplayerData3.userName;
                    DataManager.Instance.joinPlayerDatas[2].balance = joinplayerData3.balance;
                    DataManager.Instance.joinPlayerDatas[2].playerNo = 3;
                    DataManager.Instance.joinPlayerDatas[2].avtar = joinplayerData3.avtar;

                    DataManager.Instance.joinPlayerDatas[3].userId = joinplayerData4.userId;
                    DataManager.Instance.joinPlayerDatas[3].userName = joinplayerData4.userName;
                    DataManager.Instance.joinPlayerDatas[3].balance = joinplayerData4.balance;
                    DataManager.Instance.joinPlayerDatas[3].playerNo = 4;
                    DataManager.Instance.joinPlayerDatas[3].avtar = joinplayerData4.avtar;
                    BotManager.Instance.isConnectBot = true;
                    StartCoroutine(LoadScene());
                    //GenerateLoadingPanel();
                }
                else
                {
                    BotManager.Instance.isConnectBot = true;
                    StartCoroutine(LoadScene());
                    //GenerateLoadingPanel();
                }
            }
            else if (DataManager.Instance.joinPlayerDatas.Count == 1)
            {
                DataManager.Instance.tournamentID = "";
                DataManager.Instance.tourEntryMoney = 0;
                DataManager.Instance.tourCommision = 0;
                DataManager.Instance.commisionAmount = 0;
                DataManager.Instance.orgIndexPlayer = 0;
                DataManager.Instance.joinPlayerDatas.Clear();
                isPressJoin = false;

                t.text = "JOIN";
                TestSocketIO.Instace.roomid = "";
                TestSocketIO.Instace.userdata = "";
                TestSocketIO.Instace.playTime = 0;
                TestSocketIO.Instace.LeaveRoom();
                GenerateTournamentError();
            }
        }
    }

    public IEnumerator LoadScene()
    {

        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Ludo");

        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                //Destroy(obj);
                Screen.orientation = ScreenOrientation.Portrait;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }



    #endregion

}
[System.Serializable]
public class TournamentData
{
    public bool bot;
    public float bonusAmountDeduction;
    public bool active;
    public string _id;
    public string name;
    public GameType modeType;
    public float betAmount;
    public float minBet;
    public float maxBet;
    public float maxPayout;
    public float challLimit;
    public float potLimit;
    public int players;
    public int winner;
    public List<string> winnerRow = new List<string>();
    public float totalWinAmount;
    public float time;
    public int complexity;
    public int interval;
    public string _v;
    public string createdAt;
    public string updatedAt;
}
