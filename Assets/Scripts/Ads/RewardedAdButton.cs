using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour
{
    public Button theButton;
    public RewardedAdManager ad;

    private void Awake()
    {
#if UNITY_WEBGL
        theButton.gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
        theButton.interactable = ad.hasToLoadButton;
    }
}