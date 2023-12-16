using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPermission : MonoBehaviour
{
    void Start()
    {
        // Kamera izinlerini kontrol et
        CheckCameraPermission();
    }

    void CheckCameraPermission()
    {
        // Kamera izinleri verildiyse devam et
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            // Kameray� ba�lat
            StartCamera();
        }
        else
        {
            // Kamera izinleri verilmemi�se izin iste
            RequestCameraPermission();
        }
    }

    void RequestCameraPermission()
    {
        // Kamera izni iste
        Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // �zin istendi�inde �a�r�lacak bir fonksiyon belirle
        StartCoroutine(CheckPermissionCoroutine());
    }

    System.Collections.IEnumerator CheckPermissionCoroutine()
    {
        // Kullan�c� izin verene kadar bekle
        while (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            yield return null;
        }

        // Kullan�c� izin verdiyse kameray� ba�lat
        StartCamera();
    }

    void StartCamera()
    {
        // Burada kameray� ba�latma veya di�er i�lemleri ger�ekle�tirme kodunu ekleyebilirsiniz
        Debug.Log("Kamera izni al�nd�, kamera ba�lat�l�yor...");
    }
}