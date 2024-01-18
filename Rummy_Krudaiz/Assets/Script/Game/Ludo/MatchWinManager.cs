using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchWinManager : MonoBehaviour
{
    public Text profileNameMain;
    public Text wonTitleMain;

    public Text winTxtTitle;
    public Image winImg;
    public Sprite congrulationSprite;
    public Sprite tryAgainSprite;

    public Text[] profileNameTxt;
    public Text[] scoreTxt;
    public Text[] winTxt;

    public GameObject[] rowObj;
    public Sprite[] profileSprite;

    public Image rankSide1;
    public Image rankSide2;

    public Sprite[] rankSideSprite1;
    public Sprite[] rankSideSprite2;

    string roomId = "";
    string gameName = "";
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.WinSound();

        if (SceneManager.GetActiveScene().name == "Ludo")
        {
            gameName = "Ludo";
            DataSetLudo();
        }

    }
    #region Ludo Win
    void DataSetLudo()
    {
        if (LudoManager.Instance.isOpenWin == true)
        {
            return;
        }
        LudoManager.Instance.isOpenWin = true;
        if (DataManager.Instance.isTwoPlayer)
        {
            for (int i = 0; i < rowObj.Length; i++)
            {
                if (i <= 1)
                {
                    rowObj[i].SetActive(true);
                }
                else
                {
                    rowObj[i].SetActive(false);
                }
            }
            int playerWin = 0;
            if (LudoManager.Instance.isOtherPlayLeft == false)
            {
                int no1 = LudoManager.Instance.playerScoreCnt1;
                int no2 = LudoManager.Instance.playerScoreCnt3;


                if (no1 == no2)
                {
                    playerWin = 1;
                }
                else if (no1 > no2)
                {
                    playerWin = 2;
                }
                else
                {
                    playerWin = 3;
                }
            }
            else if (LudoManager.Instance.isOtherPlayLeft == true)
            {
                playerWin = 2;
            }




            if (playerWin == 1 || playerWin == 2)
            {
                //rankTxtMain.transform.parent.transform.GetChild(4).GetComponent<Text>().text = "Congratulations";
                winTxtTitle.text = "You have won the game!";

                winImg.sprite = congrulationSprite;

                rankSide1.sprite = rankSideSprite1[0];
                rankSide2.sprite = rankSideSprite2[0];

                if (playerWin == 1)
                {
                    wonTitleMain.text = "YOU WON " + (DataManager.Instance.winAmount).ToString("F2") + " Coin";


                    //float adminCommision = ((DataManager.Instance.tourEntryMoney) * 2) - DataManager.Instance.winAmount;

                    float adminCommision = DataManager.Instance.adminPercentage;


                    // if (DataManager.Instance.tourEntryMoney == 0)
                    // {
                    //     adminCommision = 0;
                    // }


                    DataManager.Instance.AddAmount(((float)DataManager.Instance.winAmount), DataManager.Instance.gameId, "Ludo-Win-" + DataManager.Instance.gameId, "won", adminCommision, 1);

                    DataManager.Instance.SetWonMoneyGame(DataManager.Instance.GetWonMoneyGame() + DataManager.Instance.winAmount);
                }
                else
                {
                    wonTitleMain.text = "YOU WON " + DataManager.Instance.winAmount + " Coin";


                    //float adminCommision = ((DataManager.Instance.tourEntryMoney / 10) * 2) - DataManager.Instance.winAmount;
                    float adminCommision = DataManager.Instance.adminPercentage;
                    bool isTournamentFree = false;
                    if (DataManager.Instance.tourEntryMoney == 0)
                    {
                        isTournamentFree = true;
                    }
                    if (DataManager.Instance.playerData.membership == "free")
                    {
                        if (isTournamentFree)
                        {
                            adminCommision = 0;
                        }
                        DataManager.Instance.AddAmount(DataManager.Instance.winAmount, DataManager.Instance.gameId, "Win " + gameName + "-" + DataManager.Instance.gameId, "won", adminCommision, 2);

                        DataManager.Instance.SetWonMoneyGame(DataManager.Instance.GetWonMoneyGame() + DataManager.Instance.winAmount);
                    }
                    else
                    {

                        adminCommision = 0;
                        float winVIPAmount = 0;
                        if (isTournamentFree)
                        {
                            winVIPAmount = DataManager.Instance.winAmount / 10;
                        }
                        else
                        {
                            winVIPAmount = (DataManager.Instance.tourEntryMoney / 5);
                        }
                        DataManager.Instance.AddAmount(winVIPAmount, DataManager.Instance.gameId, "Ludo-Win-" + DataManager.Instance.gameId, "won", adminCommision, 2);

                        DataManager.Instance.SetWonMoneyGame(DataManager.Instance.GetWonMoneyGame() + DataManager.Instance.winAmount);
                    }
                }
            }
            else
            {
                //wonTitleMain.text = "YOU WON ₹ " + 0;
                winTxtTitle.text = "You have lost the game!";
                winImg.sprite = tryAgainSprite;

                rankSide1.sprite = rankSideSprite1[1];
                rankSide2.sprite = rankSideSprite2[1];
                wonTitleMain.text = "Let's try again";
            }



            if (DataManager.Instance.isTwoPlayer)
            {

                profileNameTxt[0].text = "1st";


                if (LudoManager.Instance.isOtherPlayLeft)
                {
                    profileNameTxt[1].text = "Left-";

                }
                else
                {
                    profileNameTxt[1].text = "2nd";
                }

            }

            if (playerWin == 1 || playerWin == 2)
            {
                int getIndex = 0;
                if (DataManager.Instance.playerNo == 3)
                {
                    getIndex = 1;
                }
                //profileImgMain.sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameMain.text = DataManager.Instance.joinPlayerDatas[getIndex].userName;
                //profileImg[0].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameTxt[0].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[getIndex].userName);
                scoreTxt[0].text = LudoManager.Instance.playerScoreCnt1.ToString();
                winTxt[0].text = DataManager.Instance.winAmount + " Coin";

                int secondIndex = 0;
                if (getIndex == 0)
                {
                    secondIndex = 1;
                }
                else if (getIndex == 1)
                {
                    secondIndex = 0;
                }
                //profileImg[1].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[secondIndex].avtar];
                profileNameTxt[1].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[secondIndex].userName);

                if (LudoManager.Instance.isOtherPlayLeft)
                {
                    scoreTxt[1].text = "";
                    winTxt[1].text = "Left";
                }
                else
                {
                    scoreTxt[1].text = LudoManager.Instance.playerScoreCnt3.ToString();
                    winTxt[1].text = 0 + " Coin";
                }



            }
            else if (playerWin == 3)
            {
                int getIndex = 0;
                if (DataManager.Instance.playerNo == 3)
                {
                    getIndex = 1;
                }

                //profileImgMain.sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameMain.text = DataManager.Instance.joinPlayerDatas[getIndex].userName;

                //profileImg[1].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameTxt[1].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[getIndex].userName);

                scoreTxt[1].text = LudoManager.Instance.playerScoreCnt1.ToString();
                winTxt[1].text = 0 + " Coin";

                int secondIndex = 0;
                if (getIndex == 0)
                {
                    secondIndex = 1;
                }
                else if (getIndex == 1)
                {
                    secondIndex = 0;
                }
                //profileImg[0].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[secondIndex].avtar];
                profileNameTxt[0].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[secondIndex].userName);

                scoreTxt[0].text = LudoManager.Instance.playerScoreCnt3.ToString();
                winTxt[0].text = DataManager.Instance.winAmount + " Coin";
            }
        }
        else
        {
            for (int i = 0; i < rowObj.Length; i++)
            {
                if (i <= 1)
                {
                    rowObj[i].SetActive(true);
                }
                else
                {
                    rowObj[i].SetActive(false);
                }
            }
            int playerWin = 0;
            if (LudoManager.Instance.isOtherPlayLeft == false)
            {
                int no1 = LudoManager.Instance.playerScoreCnt1;
                int no2 = LudoManager.Instance.playerScoreCnt2;
                int no3 = LudoManager.Instance.playerScoreCnt3;
                int no4 = LudoManager.Instance.playerScoreCnt4;


                if (no1 == no2)
                {
                    playerWin = 1;
                }
                else if (no1 > no2)
                {
                    playerWin = 2;
                }
                else if(no3 == no4)
                {
                    playerWin = 3;
                }
                else
                {
                    playerWin = 4;
                }
            }
            else if (LudoManager.Instance.isOtherPlayLeft == true)
            {
                playerWin = 2;
            }




            if (playerWin == 1 || playerWin == 2)
            {
                //rankTxtMain.transform.parent.transform.GetChild(4).GetComponent<Text>().text = "Congratulations";
                winTxtTitle.text = "You have won the game!";

                winImg.sprite = congrulationSprite;

                rankSide1.sprite = rankSideSprite1[0];
                rankSide2.sprite = rankSideSprite2[0];

                if (playerWin == 1)
                {
                    wonTitleMain.text = "YOU WON " + (DataManager.Instance.winAmount / 2).ToString("F2") + " Coin";


                    float adminCommision = ((DataManager.Instance.tourEntryMoney) * 2) - DataManager.Instance.winAmount;


                    if (DataManager.Instance.tourEntryMoney == 0)
                    {
                        adminCommision = 0;
                    }


                    DataManager.Instance.AddAmount(((float)DataManager.Instance.winAmount), DataManager.Instance.gameId, "Ludo-Win-" + DataManager.Instance.gameId, "won", adminCommision, 1);

                    DataManager.Instance.SetWonMoneyGame(DataManager.Instance.GetWonMoneyGame() + DataManager.Instance.winAmount);
                }
                else
                {
                    wonTitleMain.text = "YOU WON " + DataManager.Instance.winAmount + " Coin";


                    float adminCommision = ((DataManager.Instance.tourEntryMoney / 10) * 2) - DataManager.Instance.winAmount;
                    bool isTournamentFree = false;
                    if (DataManager.Instance.tourEntryMoney == 0)
                    {
                        isTournamentFree = true;
                    }
                    if (DataManager.Instance.playerData.membership == "free")
                    {
                        if (isTournamentFree)
                        {
                            adminCommision = 0;
                        }
                        DataManager.Instance.AddAmount(DataManager.Instance.winAmount, DataManager.Instance.gameId, "Win " + gameName + "-" + DataManager.Instance.gameId, "won", adminCommision, 2);

                        DataManager.Instance.SetWonMoneyGame(DataManager.Instance.GetWonMoneyGame() + DataManager.Instance.winAmount);
                    }
                    else
                    {

                        adminCommision = 0;
                        float winVIPAmount = 0;
                        if (isTournamentFree)
                        {
                            winVIPAmount = DataManager.Instance.winAmount / 10;
                        }
                        else
                        {
                            winVIPAmount = (DataManager.Instance.tourEntryMoney / 5);
                        }
                        DataManager.Instance.AddAmount(winVIPAmount, DataManager.Instance.gameId, "Ludo-Win-" + DataManager.Instance.gameId, "won", adminCommision, 2);

                        DataManager.Instance.SetWonMoneyGame(DataManager.Instance.GetWonMoneyGame() + DataManager.Instance.winAmount);
                    }
                }
            }
            else
            {
                //wonTitleMain.text = "YOU WON ₹ " + 0;
                winTxtTitle.text = "You have lost the game!";
                winImg.sprite = tryAgainSprite;

                rankSide1.sprite = rankSideSprite1[1];
                rankSide2.sprite = rankSideSprite2[1];
                wonTitleMain.text = "Let's try again";
            }



            if (DataManager.Instance.isTwoPlayer)
            {

                profileNameTxt[0].text = "1st";


                if (LudoManager.Instance.isOtherPlayLeft)
                {
                    profileNameTxt[1].text = "Left-";

                }
                else
                {
                    profileNameTxt[1].text = "2nd";
                }

            }
            else
            {
                profileNameTxt[0].text = "1st";


                if (LudoManager.Instance.isOtherPlayLeft)
                {
                    profileNameTxt[1].text = "Left-";

                }
                else
                {
                    profileNameTxt[1].text = "2nd";
                }
            }

            if (playerWin == 1 || playerWin == 2)
            {
                int getIndex = 0;
                if (DataManager.Instance.playerNo == 3)
                {
                    getIndex = 1;
                }
                //profileImgMain.sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameMain.text = DataManager.Instance.joinPlayerDatas[getIndex].userName;
                //profileImg[0].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameTxt[0].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[getIndex].userName);
                scoreTxt[0].text = LudoManager.Instance.playerScoreCnt1.ToString();
                winTxt[0].text = DataManager.Instance.winAmount + " Coin";

                int secondIndex = 0;
                if (getIndex == 0)
                {
                    secondIndex = 1;
                }
                else if (getIndex == 1)
                {
                    secondIndex = 0;
                }
                //profileImg[1].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[secondIndex].avtar];
                profileNameTxt[1].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[secondIndex].userName);

                if (LudoManager.Instance.isOtherPlayLeft)
                {
                    scoreTxt[1].text = "";
                    winTxt[1].text = "Left";
                }
                else
                {
                    scoreTxt[1].text = LudoManager.Instance.playerScoreCnt3.ToString();
                    winTxt[1].text = 0 + " Coin";
                }



            }
            else if (playerWin == 3)
            {
                int getIndex = 0;
                if (DataManager.Instance.playerNo == 3)
                {
                    getIndex = 1;
                }

                //profileImgMain.sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameMain.text = DataManager.Instance.joinPlayerDatas[getIndex].userName;

                //profileImg[1].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameTxt[1].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[getIndex].userName);

                scoreTxt[1].text = LudoManager.Instance.playerScoreCnt1.ToString();
                winTxt[1].text = 0 + " Coin";

                int secondIndex = 0;
                if (getIndex == 0)
                {
                    secondIndex = 1;
                }
                else if (getIndex == 1)
                {
                    secondIndex = 0;
                }
                //profileImg[0].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[secondIndex].avtar];
                profileNameTxt[0].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[secondIndex].userName);

                scoreTxt[0].text = LudoManager.Instance.playerScoreCnt3.ToString();
                winTxt[0].text = DataManager.Instance.winAmount + " Coin";
            }
            else if(playerWin == 4)
            {
                int getIndex = 0;
                if (DataManager.Instance.playerNo == 3)
                {
                    getIndex = 1;
                }

                //profileImgMain.sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameMain.text = DataManager.Instance.joinPlayerDatas[getIndex].userName;

                //profileImg[1].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[getIndex].avtar];
                profileNameTxt[1].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[getIndex].userName);

                scoreTxt[1].text = LudoManager.Instance.playerScoreCnt1.ToString();
                winTxt[1].text = 0 + " Coin";

                int secondIndex = 0;
                if (getIndex == 0)
                {
                    secondIndex = 1;
                }
                else if (getIndex == 1)
                {
                    secondIndex = 0;
                }
                //profileImg[0].sprite = profileSprite[DataManager.Instance.joinPlayerDatas[secondIndex].avtar];
                profileNameTxt[0].text += UserNameStringManage(DataManager.Instance.joinPlayerDatas[secondIndex].userName);

                scoreTxt[0].text = LudoManager.Instance.playerScoreCnt3.ToString();
                winTxt[0].text = DataManager.Instance.winAmount + " Coin";
                
            }
        }

    }
    #endregion


    public string UserNameStringManage(string name)
    {
        if (name != null && name != "")
        {
            if (name.Length > 7)
            {
                name = name.Substring(0, 5) + "...";
            }
            else
            {
                name = name;
            }
        }
        return name;
    }
    public void HomeButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        DataReset();
        TestSocketIO.Instace.LeaveRoom();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Main");
        SoundManager.Instance.StartBackgroundMusic();
    }

    public void PayAgainButtonClick()
    {
        SoundManager.Instance.ButtonClick();
        DataReset();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("Main");
    }

    void DataReset()
    {
        DataManager.Instance.tournamentID = "";
        DataManager.Instance.tourEntryMoney = 0;
        DataManager.Instance.tourCommision = 0;
        DataManager.Instance.commisionAmount = 0;
        DataManager.Instance.orgIndexPlayer = 0;
        DataManager.Instance.joinPlayerDatas.Clear();
        TestSocketIO.Instace.roomid = "";
        TestSocketIO.Instace.userdata = "";
        TestSocketIO.Instace.playTime = 0;
        BotManager.Instance.isBotAvalible = false;
        BotManager.Instance.isConnectBot = false;
    }
}
