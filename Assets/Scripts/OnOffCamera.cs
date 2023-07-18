using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnOffCamera : MonoBehaviour
{
    public Button CameraToggle;
    public GameObject CameraOn, AgainPhoto;
    public RawImage TookPhoto;
    public TextMeshProUGUI Countdown;

    private Texture photo;
    private bool iscameraOn;
    private float timer, countdown;
    void Start()
    {
        countdown = 4f;
        iscameraOn = false;
        AgainPhoto.SetActive(false);
        CameraToggle.onClick.AddListener(() =>
        {
            CameraOn.SetActive(true);
            CameraToggle.gameObject.SetActive(false);
            iscameraOn=true;
        });
    }

    private void Update()
    {
        int down;
        timer = Time.deltaTime;
        if (iscameraOn)
        {
            countdown-=(float)timer;
            down = (int)countdown;
            Countdown.text = down.ToString();
            Countdown.gameObject.SetActive(true);

            if(countdown <= 0)
            {
                StartCoroutine(Screenshot());
                //Screenshot();
                Countdown.gameObject.SetActive(false);
                iscameraOn = false;
            }
        }
    }

    IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        photo =  ScreenCapture.CaptureScreenshotAsTexture();
        TookPhoto.texture = photo;
        TookPhoto.gameObject.SetActive(true);
        yield return null;
        TookPhoto.GetComponent<Animator>().Play(0);
        CameraOn.SetActive(false);
        yield return new WaitForSeconds(3f);
        AgainPhoto.SetActive(true);
        Debug.Log(TookPhoto);
    }


    public void TakePhoto()
    {
        SceneManager.LoadScene(0);
    }
}
