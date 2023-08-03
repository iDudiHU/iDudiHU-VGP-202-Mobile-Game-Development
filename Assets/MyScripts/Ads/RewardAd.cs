using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    string AndroidAdUnit = "Rewarded_Android";
    string iOSAdUnit = "Rewarded_iOS";
    string AdUnitID;

    #region IUnityAdsLoadListener
    public void OnUnityAdsAdLoaded(string placementId)
    {
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region IUnityAdsShowListener
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Show Click");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Show Completed");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Show Failure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Show Start");
    }
    #endregion

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
