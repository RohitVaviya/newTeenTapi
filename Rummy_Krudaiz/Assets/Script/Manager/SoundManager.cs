
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [Header("Background Music")]
    public AudioSource bgAudio;
    public AudioClip bgClip;


    [Header("Button Audio")]
    public AudioSource btnAudio;
    public AudioClip btnClip;

    [Header("Roll Dice")]
    public AudioSource rollDiceAudio;
    public AudioClip rollDiceClip;

    [Header("Tick Timer")]
    public AudioSource tickTimerAudio;
    public AudioClip tickTimerClip;

    [Header("Time Out")]
    public AudioSource timeOutAudio;
    public AudioClip timeOutClip;

    [Header("Token Home")]
    public AudioSource tokenHomeAudio;
    public AudioClip tokenHomeClip;


    [Header("Token Move")]
    public AudioSource tokenMoveAudio;
    public AudioClip tokenMoveClip;

    [Header("Token Kill")]
    public AudioSource tokenKillAudio;
    public AudioClip tokenKillClip;

    [Header("User Turn")]
    public AudioSource userTurnAudio;
    public AudioClip userTurnClip;

    [Header("Winning")]
    public AudioSource winAudio;
    public AudioClip winClip;


    [Header("Dragon Tiger-Andar Bahar-Roulette")]
    public AudioSource threeBetAudio;
    public AudioClip threeBetClip;

    public AudioSource dragonRoar;
    public AudioClip dragonRoarClip;
    
    public AudioSource tigerRoar;
    public AudioClip tigerRoarClip;
    
    public AudioSource dragonTigerBg;
    public AudioClip dragonTigerBgClip;

    [Header("Casino Win")]
    public AudioSource casinoWinAudio;
    public AudioClip casinoWinClip;

    [Header("Casino Turn")]
    public AudioSource casinoTurnAudio;
    public AudioClip casinoTurnClip;


    [Header("Casino Card Move")]
    public AudioSource casinoCardMoveAudio;
    public AudioClip casinoCardMoveClip;


    [Header("Casino Card Swipe")]
    public AudioSource casinoCardSwipeAudio;
    public AudioClip casinoCardSwipeClip;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }

    }

    private void Start()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            StartBackgroundMusic();
        }
        else if (DataManager.Instance.GetSound() == 1)
        {
            StopBackgroundMusic();
        }
    }
    //private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(DateTime.UtcNow);
            //DateTime ist = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            //DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            //print();
        }
    }


    public void StartBackgroundMusic()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            bgAudio.clip = bgClip;
            bgAudio.Play();
            bgAudio.volume = 1.0f;
        }
        else
        {
            bgAudio.Stop();
        }
    }
    public void StopBackgroundMusic()
    {
        bgAudio.volume = 0.05f;
        //bgAudio.Stop();
    }
    
    public void StopBackgroundMusicComplitly()
    {
        //bgAudio.volume = 0.05f;
        bgAudio.Stop();
    }
    
    public void DragonTigerBg()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            dragonTigerBg.clip = dragonTigerBgClip;
            dragonTigerBg.Play();
            dragonTigerBg.volume = 0.5f;
        }
    }
    
    public void DragonTigerBgStop()
    {
        dragonTigerBg.Stop();
    }

    public void ButtonClick()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            btnAudio.clip = btnClip;
            btnAudio.Play();
        }
    }

    public void RollDice_Start_Sound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            rollDiceAudio.clip = rollDiceClip;
            rollDiceAudio.Play();
        }
    }

    public void RollDice_Stop_Sound()
    {
        rollDiceAudio.Stop();
    }

    public void TickTimerSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            tickTimerAudio.clip = tickTimerClip;
            tickTimerAudio.Play();
        }
    }
    public void TickTimerStop()
    {
        tickTimerAudio.Stop();
    }

    public void TimeOutSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            timeOutAudio.clip = timeOutClip;
            timeOutAudio.Play();
        }
    }

    public void TokenHomeSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            tokenHomeAudio.clip = tokenHomeClip;
            tokenHomeAudio.Play();
        }
    }


    public void TokenMoveSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            tokenMoveAudio.clip = tokenMoveClip;
            tokenMoveAudio.Play();
        }
    }

    public void TokenKillSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            tokenKillAudio.clip = tokenKillClip;
            tokenKillAudio.Play();
        }
    }

    public void UserTurnSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            userTurnAudio.clip = userTurnClip;
            userTurnAudio.Play();
        }
    }

    public void WinSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            winAudio.clip = winClip;
            winAudio.Play();
        }
    }

    public void StopAllSound()
    {
        TickTimerStop();
        TickTimerStop();
    }


    #region Casino Game

    public void ThreeBetSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            threeBetAudio.clip = threeBetClip;
            threeBetAudio.Play();
        }
    }
    
    public void DragonWinSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            dragonRoar.clip = dragonRoarClip;
            dragonRoar.Play();
        }
    }
    
    public void TigerWinSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            tigerRoar.clip = tigerRoarClip;
            tigerRoar.Play();
        }
    }

    public void CasinoWinSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            casinoWinAudio.clip = casinoWinClip;
            casinoWinAudio.Play();
        }
    }
    public void CasinoTurnSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            casinoTurnAudio.clip = casinoTurnClip;
            casinoTurnAudio.Play();
        }
    }

    public void CasinoCardMoveSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            casinoCardMoveAudio.clip = casinoCardMoveClip;
            casinoCardMoveAudio.Play();
        }
    }

    public void CasinoCardSwipeSound()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            casinoCardSwipeAudio.clip = casinoCardSwipeClip;
            casinoCardSwipeAudio.Play();
        }
    }

    #endregion





}
