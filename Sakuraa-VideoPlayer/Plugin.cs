using System;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Sakuraa_VideoPlayer
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private VideoPlayer videoPlayer;
        private GameObject canvas;
        private float countdown = 60;

        void Start()
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }

        void Update()
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                playVideo();
                countdown = 60;
            }

        }

        void playVideo()
        {
            if (canvas != null)
            {
                videoPlayer.Stop();
                Destroy(videoPlayer);
                Destroy(canvas);
                canvas = null;
            }

            canvas = new GameObject("VidCanvas");
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);

            GameObject rawImageObject = new GameObject("RawImage");
            rawImageObject.transform.SetParent(canvas.transform);

            RenderTexture renderTexture = new RenderTexture(1080, 1920, 24, RenderTextureFormat.ARGB32);
            renderTexture.Create();
            rawImageObject.AddComponent<RawImage>().texture = renderTexture;
            rawImageObject.transform.localPosition = Vector3.zero;
            rawImageObject.transform.localScale = new Vector3(10.8f, 42.7f, 10);
            rawImageObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            videoPlayer = rawImageObject.AddComponent<VideoPlayer>();
            string videoUrl = "https://cdn.discordapp.com/attachments/1076682685172432986/1117619904913346640/gtc_noob_LOL_28FD17E.mp4";
            // this mod is not afiliated with gtc in any way, just used this cuz its a great edit. this mod was made for fun.
            videoPlayer.url = videoUrl;
            videoPlayer.targetTexture = renderTexture;
            videoPlayer.isLooping = false;
        }

        private void OnVideoEnd(VideoPlayer vp)
        {
            videoPlayer.Stop();
            Destroy(videoPlayer);
            Destroy(canvas);
            canvas = null;
        }
    }
}
