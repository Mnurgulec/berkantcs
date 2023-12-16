using UnityEngine;
using UnityEngine.UI;

public class FaceRecognition : MonoBehaviour
{
    public RawImage rawImage; // UI ��esi veya Renderer i�in referans
    public Texture2D referencePhoto; // Kar��la�t�r�lacak referans foto�raf

    private WebCamTexture webcamTexture;
    private Texture2D capturedPhoto;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (WebCamDevice device in devices)
        {
            Debug.Log("Camera available: " + device.name);
        }


        // Kamera kontrol�
        if (devices.Length > 0)
        {
            webcamTexture = new WebCamTexture(devices[0].name);
            webcamTexture.Play();
            rawImage.texture = webcamTexture; // Kamera ��k���n� g�ster

            capturedPhoto = new Texture2D(webcamTexture.width, webcamTexture.height);
        }
        else
        {
            Debug.LogError("No webcam found!");
        }
    }

    void Update()
    {
        // Kameradan foto�raf� �ek
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CapturePhoto();
            ComparePhotos();
        }
    }

    void CapturePhoto()
    {
        // Kameradan foto�raf� Texture2D'ye �ek
        capturedPhoto.SetPixels(webcamTexture.GetPixels());
        capturedPhoto.Apply();
    }

    void ComparePhotos()
    {
        // Kar��la�t�rma i�lemleri burada ger�ekle�tirilebilir
        float similarity = CompareTextures(capturedPhoto, referencePhoto);

        // Benzerlik oran�n� ekrana yazd�r
        Debug.Log("Similarity: " + similarity);
    }

    // �ki Texture2D'nin benzerli�ini kar��la�t�ran basit bir �rnek
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

        // Benzerlik oran�n� hesapla
        float similarity = (float)matchingPixels / pixels1.Length;

        return similarity;
    }

    void OnDisable()
    {
        // Kameray� kapat
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }
}
