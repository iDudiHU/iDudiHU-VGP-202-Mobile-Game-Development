using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerAd : MonoBehaviour
{
    public Button hideBannerButton;

    BannerPosition bannerPos = BannerPosition.BOTTOM_CENTER;

    string AndroidAdUnit = "Banner_Android";
    string iOSAdUnit = "Banner_iOS";
    string AdUnitID;

    void Awake()
    {
        AdUnitID = AndroidAdUnit;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            AdUnitID = iOSAdUnit;

        if (hideBannerButton != null)
            hideBannerButton.interactable = false;

        Advertisement.Banner.SetPosition(bannerPos);
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(options);
    }

    void OnBannerLoaded()
    {
        hideBannerButton.onClick.AddListener(HideBannerAd);
        hideBannerButton.interactable = true;
        ShowBannerAd();
    }

    void OnBannerError(string msg)
    {
        Debug.Log(msg);
    }

    void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
    
    void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(AdUnitID, options);
    }

    void OnBannerHidden()
    {
        hideBannerButton.interactable = false;
    }

    void OnBannerShown()
    {
        hideBannerButton.interactable = true;
    }

    void OnBannerClicked()
    {

    }

    private void OnDestroy()
    {
        if (hideBannerButton != null)
            hideBannerButton.onClick.RemoveAllListeners();
    }
}
