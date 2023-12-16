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
            // Kamerayý baþlat
            StartCamera();
        }
        else
        {
            // Kamera izinleri verilmemiþse izin iste
            RequestCameraPermission();
        }
    }

    void RequestCameraPermission()
    {
        // Kamera izni iste
        Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // Ýzin istendiðinde çaðrýlacak bir fonksiyon belirle
        StartCoroutine(CheckPermissionCoroutine());
    }

    System.Collections.IEnumerator CheckPermissionCoroutine()
    {
        // Kullanýcý izin verene kadar bekle
        while (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            yield return null;
        }

        // Kullanýcý izin verdiyse kamerayý baþlat
        StartCamera();
    }

    void StartCamera()
    {
        // Burada kamerayý baþlatma veya diðer iþlemleri gerçekleþtirme kodunu ekleyebilirsiniz
        Debug.Log("Kamera izni alýndý, kamera baþlatýlýyor...");
    }
}