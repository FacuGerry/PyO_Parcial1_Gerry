using System.Collections;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif


public class LocalNotisManager : MonoBehaviour
{
#if UNITY_ANDROID
    private static string CHANNEL_ID = "notis01";

    private void Start()
    {
        //Creo los Notification Channels, una única vez.
        string NotiChannels_Created_Key = "NotiChannels_Created";
        if (!PlayerPrefs.HasKey(NotiChannels_Created_Key))
        {
            AndroidNotificationChannelGroup group = new AndroidNotificationChannelGroup()
            {
                Id = "Main",
                Name = "Main notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannelGroup(group);
            AndroidNotificationChannel channel = new AndroidNotificationChannel()
            {
                Id = CHANNEL_ID,
                Name = "Default Channel",
                Importance = Importance.Default,
                Description = "Generic notifications",
                Group = "Main",  // Tiene que ser el mismo Id del grupo que creé antes
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            StartCoroutine(RequestPermission());

            PlayerPrefs.SetString(NotiChannels_Created_Key, "y");
            PlayerPrefs.Save();
        }
        else
        {
            ScheduleNotis();
        }
    }

    private IEnumerator RequestPermission()
    {
        PermissionRequest request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return null;

        ScheduleNotis();
    }

    private void ScheduleNotis()
    {
        //Elimino las notis que había creado en la sesión anterior.
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        //Y las creo de nuevo:
        AndroidNotification notification3days = new AndroidNotification();
        notification3days.Title = "Ya pasaron 10 minutos";
        notification3days.Text = "Volve a jugar. Juego de Facundo Gerry";
        notification3days.FireTime = System.DateTime.Now.AddMinutes(10);

        AndroidNotificationCenter.SendNotification(notification3days, CHANNEL_ID);
    }
#endif
}