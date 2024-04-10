using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Scrollbar volumeScrollbar;

    void Start()
    {
        // ��������� ���������� ������� �� ��������� �������� ����������
        volumeScrollbar.onValueChanged.AddListener(HandleVolumeChange);
    }

    // ���� ����� ����� ���������� ������ ���, ����� ������������ �������� ��������� ��������
    private void HandleVolumeChange(float value)
    {
        // ����� value ����� � ��������� �� 0 �� 1
        AudioListener.volume = value;
    }
}
