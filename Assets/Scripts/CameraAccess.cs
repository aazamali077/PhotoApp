using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAccess : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture frontcam;
    private Texture defaultbackground;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        defaultbackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No Camera");
            camAvailable = false;
            return;
        }

        for (int i =0;i<devices.Length;i++)
        {
            if (devices[i].isFrontFacing)
            {
                frontcam = new WebCamTexture(devices[i].name, Screen.width,Screen.height);
            }
        }

        if (frontcam == null)
        {
            Debug.Log("No Front Camera");
            return;
        }

        frontcam.Play();
        background.texture = frontcam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable)
            return;


        float ratio = (float)frontcam.width / (float)frontcam.height;
        fit.aspectRatio = ratio;

        float scaleY = frontcam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -frontcam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0f, 0f, orient);
    }
}
