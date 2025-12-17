using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkMenu : MonoBehaviour
{
    public TMP_InputField ipInput;
    public Button hostButton;
    public Button joinButton;

    public NetworkManager network;

    private void Start()
    {
        network = FindObjectOfType<NetworkManager>();
        ipInput.text = network.networkAddress; //  localhost par défaut

        hostButton.onClick.AddListener(Host);
        joinButton.onClick.AddListener(Join);
    }

    public void Host()
    {
        Debug.Log("Starting Host...");
        network.StartHost();
    }

    public void Join()
    {
        string ip = ipInput.text;

        if (!string.IsNullOrEmpty(ip))
        {
            network.networkAddress = ip;
        }

        Debug.Log("Connecting to " + network.networkAddress);
        network.StartClient();
    }
}
