using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    string AndroidAdUnit = "Interstitial_Android";
    string iOSAdUnit = "Interstitial_iOS";
    string AdUnitID;

    public void OnUnityAdsAdLoaded(string placementId)
    {
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        AdUnitID = AndroidAdUnit;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            AdUnitID = iOSAdUnit;
    }

    public void LoadAd()
    {
        Advertisement.Load(AdUnitID, this);
    }

    void ShowAd()
    {
        Advertisement.Show(AdUnitID, this);
    }
}
