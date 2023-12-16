using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarSwitch : MonoBehaviour
{
    public Transform[] Cars;
    public Transform MyCamera;

    private bool camAvailable;
    private WebCamTexture cameraTexture;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;
    public bool frontFacing;

    [SerializeField] private RawImage img = default;

    private WebCamTexture webCam;
    void Start()
    {
        // CarSwitch bileşenini al
        CarSwitch carSwitchComponent = GetComponent<CarSwitch>();

        // CarSwitch bileşeni null değilse, bir GameObject'e bağlıdır
        if (carSwitchComponent != null)
        {
            // CarSwitch bileşeni burada kullanılabilir
            // Örneğin, diğer kodları çağırabilir veya özelliklerine erişebilirsiniz
            carSwitchComponent.CurrentCarActive(0);
        }
        else
        {
            // CarSwitch bileşeni null ise, bir GameObject'e bağlı değildir
            Debug.LogError("CarSwitch component is not attached to a GameObject!");
        }
        // Assign the background variable, assuming it is set in the Unity Editor or instantiated somewhere else

        // Kamera butonunu burada çağırarak fotoğraf çekmesini sağla
        Button();
    }

    public void Button()
    {
        webCam = new WebCamTexture();

        if (!webCam.isPlaying) webCam.Play();
        img.texture = webCam;

        // Eklenen kısım: Texture2D'yi dosyaya kaydet
        SaveWebCamTextureToFile(webCam, "capturedPhoto.png");
    }

    void Update()
    {
        if (!camAvailable)
            return;

        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        fit.aspectRatio = ratio; // Set the aspect ratio

        float scaleY = cameraTexture.videoVerticallyMirrored ? -1f : 1f; // Find if the camera is mirrored or not
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f); // Swap the mirrored camera

        int orient = -cameraTexture.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    public void CurrentCarActive(int current)
    {
        int amount = 0;

        foreach (Transform Car in Cars)
        {
            if (current == amount)
            {
                MyCamera.GetComponent<VehicleCamera>().target = Car;

                MyCamera.GetComponent<VehicleCamera>().Switch = 0;
                MyCamera.GetComponent<VehicleCamera>().cameraSwitchView = Car.GetComponent<VehicleControl>().carSetting.cameraSwitchView;
                Car.GetComponent<VehicleControl>().activeControl = true;
            }
            else
            {
                Car.GetComponent<VehicleControl>().activeControl = false;
            }

            amount++;
        }
    }

    // Texture2D'yi dosyaya kaydetmek için yardımcı fonksiyon
    void SaveWebCamTextureToFile(WebCamTexture webCamTexture, string filePath)
    {
        Texture2D texture = new Texture2D(webCamTexture.width, webCamTexture.height);
        texture.SetPixels(webCamTexture.GetPixels());
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);

        Debug.Log("Photo saved to: " + filePath);
    }
}
