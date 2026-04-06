using Meta.WitAi;
using Meta.WitAi.CallbackHandlers;
using Meta.WitAi.Requests;
using Oculus.Voice;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Android;
using System.Collections;

public class VoiceMANAGEMENT : MonoBehaviour
{
    [Header("App Voice Experience")]
    [SerializeField] private AppVoiceExperience voiceExperience;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI transcriptionText;

    [Header("Response Matcher")]
    [SerializeField] private WitResponseMatcher responseMatcher;

    [Header("Voice Events")]
    public UnityEvent onWakeWordDetected;
    public UnityEvent<string> onCompleteTranscription;

    private bool voiceCommandReady;

    private void Awake()
    {
        if (voiceExperience == null)
            voiceExperience = FindFirstObjectByType<AppVoiceExperience>();

        if (responseMatcher == null)
            responseMatcher = FindFirstObjectByType<WitResponseMatcher>();

#if UNITY_ANDROID && !UNITY_EDITOR
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }

    private void OnEnable()
    {
        if (voiceExperience == null) return;

        voiceExperience.VoiceEvents.OnRequestInitialized
            .AddListener(HandleRequestInitialized);

        voiceExperience.VoiceEvents.OnPartialTranscription
            .AddListener(HandlePartialTranscription);

        voiceExperience.VoiceEvents.OnFullTranscription
            .AddListener(HandleFullTranscription);
    }

    private void OnDisable()
    {
        if (voiceExperience == null) return;

        voiceExperience.VoiceEvents.OnRequestInitialized
            .RemoveListener(HandleRequestInitialized);

        voiceExperience.VoiceEvents.OnPartialTranscription
            .RemoveListener(HandlePartialTranscription);

        voiceExperience.VoiceEvents.OnFullTranscription
            .RemoveListener(HandleFullTranscription);
    }

    // AUTO‑START LISTENING 
    private IEnumerator Start()
    {
        // Give Unity + OS time to register microphone
        yield return new WaitForSeconds(0.5f);

        if (voiceExperience != null && !voiceExperience.Active)
        {
            voiceCommandReady = true;
            voiceExperience.Activate();
        }
    }

    private void HandleRequestInitialized(VoiceServiceRequest request)
    {
        Debug.Log("Voice listening started");
        onWakeWordDetected?.Invoke();
    }

    // LIVE TEXT WHILE SPEAKING
    private void HandlePartialTranscription(string text)
    {
        if (!voiceCommandReady) return;

        if (transcriptionText != null)
            transcriptionText.text = text;
    }

    // FINAL TEXT AFTER SPEECH
    private void HandleFullTranscription(string text)
    {
        if (!voiceCommandReady) return;

        Debug.Log("Final transcription: " + text);

        voiceCommandReady = false;

        if (transcriptionText != null)
            transcriptionText.text = text;

        onCompleteTranscription?.Invoke(text);
    }
}
