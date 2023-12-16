using UnityEngine;
using UnityEngine.UI;

public class FaceRecognition : MonoBehaviour
{
    public RawImage rawImage; // UI öðesi veya Renderer için referans
    public Texture2D referencePhoto; // Karþýlaþtýrýlacak referans fotoðraf

    private WebCamTexture webcamTexture;
    private Texture2D capturedPhoto;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (WebCamDevice device in devices)
        {
            Debug.Log("Camera available: " + device.name);
        }


        // Kamera kontrolü
        if (devices.Length > 0)
        {
            webcamTexture = new WebCamTexture(devices[0].name);
            webcamTexture.Play();
            rawImage.texture = webcamTexture; // Kamera çýkýþýný göster

            capturedPhoto = new Texture2D(webcamTexture.width, webcamTexture.height);
        }
        else
        {
            Debug.LogError("No webcam found!");
        }
    }

    void Update()
    {
        // Kameradan fotoðrafý çek
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CapturePhoto();
            ComparePhotos();
        }
    }

    void CapturePhoto()
    {
        // Kameradan fotoðrafý Texture2D'ye çek
        capturedPhoto.SetPixels(webcamTexture.GetPixels());
        capturedPhoto.Apply();
    }

    void ComparePhotos()
    {
        // Karþýlaþtýrma iþlemleri burada gerçekleþtirilebilir
        float similarity = CompareTextures(capturedPhoto, referencePhoto);

        // Benzerlik oranýný ekrana yazdýr
        Debug.Log("Similarity: " + similarity);
    }

    // Ýki Texture2D'nin benzerliðini karþýlaþtýran basit bir örnek
    float CompareTextures(Texture2D texture1, Texture2D texture2)
    {
        Color[] pixels1 = texture1.GetPixels();
        Color[] pixels2 = texture2.GetPixels();

        int matchingPixels = 0;

        for (int i = 0; i < pixels1.Length; i++)
        {
            if (pixels1[i] == pixels2[i])
            {
                matchingPixels++;
            }
        }

        // Benzerlik oranýný hesapla
        float similarity = (float)matchingPixels / pixels1.Length;

        return similarity;
    }

    void OnDisable()
    {
        // Kamerayý kapat
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }
}
