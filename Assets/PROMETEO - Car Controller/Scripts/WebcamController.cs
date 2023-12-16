using UnityEngine;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    void Start()
    {
        // Get all available webcam devices
        WebCamDevice[] devices = WebCamTexture.devices;

        // Check if there is at least one webcam
        if (devices.Length > 0)
        {
            // Use the first webcam (you can modify this logic based on your requirements)
            webcamTexture = new WebCamTexture(devices[0].name);

            // Start the webcam
            webcamTexture.Play();

            // Apply the webcam texture to a material or UI element (e.g., RawImage)
            GetComponent<RawImage>().texture = webcamTexture;
        }
        else
        {
            Debug.LogError("No webcam found!");
        }
    }

    void Update()
    {
        // You can add additional logic or processing here if needed
    }

    void OnDisable()
    {
        // Stop the webcam when the script is disabled or the GameObject is destroyed
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }
}
