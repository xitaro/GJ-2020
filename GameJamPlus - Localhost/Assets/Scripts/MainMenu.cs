using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;

    // When we want to host a lobby
    // When we press the host button
    public void HostLobby()
    {
        // Tell a network manager to start a host
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
    }
}
