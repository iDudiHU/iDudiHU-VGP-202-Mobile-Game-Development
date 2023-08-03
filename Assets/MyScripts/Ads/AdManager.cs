using System.IO;
using UnityEngine;
using UnityEngine.Advertisements;

[RequireComponent(typeof(RewardAd), typeof(InterstitialAd), typeof(BannerAd))]
public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public string AndroidGameID;
    public string iOSGameID;
    public bool TestMode;

    private InterstitialAd interstitialAd;
    public InterstitialAd InterstitialAd => interstitialAd;

    private RewardAd rewardAd;
    public RewardAd RewardAd => rewardAd;

    private BannerAd bannerAd;
    public BannerAd BannerAd => bannerAd;

    #region IUnityAdsInitializationListener
    public void OnInitializationComplete()
    {
        bannerAd.LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
    #endregion


    void Awake()
    {
        interstitialAd = GetComponent<InterstitialAd>();
        rewardAd = GetComponent<RewardAd>();
        bannerAd = GetComponent<BannerAd>();

        string gameID = AndroidGameID;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            gameID = iOSGameID;

        if (string.IsNullOrEmpty(gameID))
            throw new InvalidDataException("no game ID set, please ensure it is set properly");

        Advertisement.Initialize(gameID, TestMode, this);
    }
}
