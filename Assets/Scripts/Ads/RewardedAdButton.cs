using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour
{
    public Button theButton;
    public RewardedAdManager ad;

    private void Update()
    {
        theButton.interactable = ad.hasToLoadButton;
    }
}