using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Scrollbar volumeScrollbar;

    void Start()
    {
        // Добавляем обработчик события на изменение значения скроллбара
        volumeScrollbar.onValueChanged.AddListener(HandleVolumeChange);
    }

    // Этот метод будет вызываться каждый раз, когда пользователь изменяет положение ползунка
    private void HandleVolumeChange(float value)
    {
        // Здесь value будет в диапазоне от 0 до 1
        AudioListener.volume = value;
    }
}
