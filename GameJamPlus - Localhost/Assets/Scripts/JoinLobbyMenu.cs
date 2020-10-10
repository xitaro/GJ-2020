using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    // Reference to the Network Manager
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    }

    // When press the Join Lobby Button
    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;
        
        // Set the networkAdress for the networkManager to be at this IP
        networkManager.networkAddress = ipAddress;
        // Start as a client
        networkManager.StartClient();

        joinButton.interactable = false;
    }

    // When successfully connected to the server based on this IP
    public void HandleClientConnected()
    {
        // Re-enable the join button - To when we go back to the menu
        joinButton.interactable = true;

        // Disable this object and the landingPage
        gameObject.SetActive(false);
        landingPanel.SetActive(false);
    }

    // If disconnect - Called when fail to connect
    public void HandleClientDisconnected()
    {
        // We turn on the joinButton
        joinButton.interactable = true;
    }
}
