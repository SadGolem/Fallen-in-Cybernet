using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer; // ��������� ��� Audio Mixer ����� ����� Inspector
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider voiceVolumeSlider;
    private const float defaultVolume = 0.3f; // ������� ��������� ���������
    private string settingsFilePath;

    private void Awake()
    {
        settingsFilePath = Path.Combine(Application.persistentDataPath, "audioSettings.json");
        LoadSettings();
    }

    private void Start()
    {
        // ��������� ��������� �������� ��������� � ����������
        masterVolumeSlider.onValueChanged.AddListener(value => SetVolume("MasterVolume", value));
        musicVolumeSlider.onValueChanged.AddListener(value => SetVolume("MusicVolume", value));
        voiceVolumeSlider.onValueChanged.AddListener(value => SetVolume("VoiceVolume", value));
    }

    public void SetVolume(string parameterName, float value)
    {
        // ����������� �������� � �������� ���������
        audioMixer.SetFloat(parameterName, Mathf.Log10(value) * 20);
    }

    public void SaveSettings()
    {
        // �������� ������� � �����������
        AudioSettings settings = new AudioSettings
        {
            masterVolume = masterVolumeSlider.value,
            musicVolume = musicVolumeSlider.value,
            voiceVolume = voiceVolumeSlider.value
        };

        // ������������ �������� � JSON
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(settingsFilePath, json);
    }

    public void LoadSettings()
    {
        // �������� ��������, ���� ���� ����������
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            AudioSettings settings = JsonUtility.FromJson<AudioSettings>(json);

            masterVolumeSlider.value = settings.masterVolume;
            musicVolumeSlider.value = settings.musicVolume;
            voiceVolumeSlider.value = settings.voiceVolume;

            SetVolume("MasterVolume", settings.masterVolume);
            SetVolume("MusicVolume", settings.musicVolume);
            SetVolume("VoiceVolume", settings.voiceVolume);
        }
        else
        {
            // ��������� �������� �� ���������, ���� ���� �������� �� ������
            masterVolumeSlider.value = defaultVolume;
            musicVolumeSlider.value = defaultVolume;
            voiceVolumeSlider.value = defaultVolume;

            SaveSettings(); // ���������� ����������� ��������
        }
    }

    public void ResetToDefaults()
    {
        // ����� �������� �� �������� �� ���������
        masterVolumeSlider.value = defaultVolume;
        musicVolumeSlider.value = defaultVolume;
        voiceVolumeSlider.value = defaultVolume;

        SaveSettings(); // ���������� ����� ������
    }
}