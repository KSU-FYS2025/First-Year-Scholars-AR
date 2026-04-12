using Meta.WitAi;
using Meta.WitAi.CallbackHandlers;
using Oculus.Voice;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Android;

public class VoiceManager : MonoBehaviour
{/*
    [Header("Wit Configuration")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;
    [SerializeField] private WitResponseMatcher responseMatcher;
    [SerializeField] private TextMeshProUGUI transcriptionText;

    [Header("Voice Events")]
    [SerializeField] private UnityEvent wakeWordDetected;
    [SerializeField] private UnityEvent<string> completeTranscription;

    private bool _voiceCommandReady;

    [Header("WebSocket")]
    public WebSocketClient webSocketClient;

    private void Awake()
    {
        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(ReactivateVoice);
        appVoiceExperience.VoiceEvents.OnPartialTranscription.AddListener(OnPartialTranscription);
        appVoiceExperience.VoiceEvents.OnFullTranscription.AddListener(OnFullTranscription);

        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(responseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.AddListener(WakeWordDetected);
        }

        appVoiceExperience.Activate();
    }

    private void OnDestroy()
    {
        appVoiceExperience.VoiceEvents.OnRequestCompleted.RemoveListener(ReactivateVoice);
        appVoiceExperience.VoiceEvents.OnPartialTranscription.RemoveListener(OnPartialTranscription);
        appVoiceExperience.VoiceEvents.OnFullTranscription.RemoveListener(OnFullTranscription);

        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(responseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.RemoveListener(WakeWordDetected);
        }
    }

    private void ReactivateVoice() => appVoiceExperience.Activate();

    private void WakeWordDetected(string[] args)
    {
        _voiceCommandReady = true;
        wakeWordDetected?.Invoke();
    }

    // While the user is still speaking
    private void OnPartialTranscription(string transcription)
    {
        if (!_voiceCommandReady) return;
        transcriptionText.text = transcription;
    }

    // When the system determines the user has finished speaking
    private void OnFullTranscription(string transcription)
    {
        if (!_voiceCommandReady) return;
        _voiceCommandReady = false;
        completeTranscription?.Invoke(transcription);
        webSocketClient.SendText(transcriptionText.text);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TEST
            webSocketClient.SendText(transcriptionText.text);
            Debug.Log("Send");
            // TEST
        }
    }*/

    [SerializeField] private GameObject sttContentUI;
    [SerializeField] private GameObject contentUI;
    [SerializeField] private GameObject recordBtn;
    [SerializeField] private AppVoiceExperience appVoiceExperience;
    [SerializeField] private WitResponseMatcher responseMatcher;
    [SerializeField] private TextMeshProUGUI transcriptionText;
    [SerializeField] private TextMeshProUGUI backendText;
    [SerializeField] private TextMeshProUGUI sttInfoText;
    [SerializeField] private RealtimeQueryManager realtimeQueryManager;

    private bool listening = false;

    private void OnValidate()
    {
        if (!appVoiceExperience) appVoiceExperience = GetComponent<AppVoiceExperience>();
    }

    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    public void Activate()
    {
        if (!listening)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }

            appVoiceExperience.Activate();
            listening = true;
            recordBtn.GetComponent<Image>().color = Color.green;
        }
        else
        {
            appVoiceExperience.Deactivate();
            listening = false;
            recordBtn.GetComponent<Image>().color = Color.white;
        }
    }

    public void StopListening()
    {
        if (listening)
        {
            listening = false;
            recordBtn.GetComponent<Image>().color = Color.white;
        }
    }

    public void Navigate()
    {
        if (realtimeQueryManager.manualQuery != transcriptionText.text)
        {
            realtimeQueryManager.manualQuery = transcriptionText.text;
            realtimeQueryManager.SendManualQuery();
            //CloseSTT();
        }
    }

    public void OpenSTT()
    {
        sttContentUI.SetActive(true);
        contentUI.SetActive(false);
    }

    public void CloseSTT()
    {
        backendText.text = "";
        sttContentUI.SetActive(false);
        contentUI.SetActive(true);
    }

    // While the user is still speaking
    public void OnPartialTranscription(string transcription)
    {
        //if (!_voiceCommandReady) return;
        transcriptionText.text = transcription;
    }

    // When the system determines the user has finished speaking
    private void OnFullTranscription(string transcription)
    {
        //if (!_voiceCommandReady) return;
        //_voiceCommandReady = false;
        //completeTranscription?.Invoke(transcription);
        //webSocketClient.SendText(transcriptionText.text);
    }
}
