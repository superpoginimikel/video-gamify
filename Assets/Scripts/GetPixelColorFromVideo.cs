using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GetPixelColorFromVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public PlayerController playerController;
    public Image colorChecker;

    private Texture2D videoTexture;
    private int startingPosYOffset = 150;

    void Start()
    {
        // Prepare the video player
        videoPlayer.playOnAwake = false;
        videoPlayer.loopPointReached += OnVideoEnd;

        // Set video texture to the RawImage
        videoTexture = new Texture2D((int)videoPlayer.width, (int)videoPlayer.height);

        // Load the video file
        videoPlayer.Prepare();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Video has ended, do something if needed
    }

    void Update()
    {
        // Check if video is ready to play
        if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }

        // Check if video frame is ready
        if (videoPlayer.texture != null && videoPlayer.isPlaying)
        {
            // Update the video texture
            RenderTexture renderTexture = videoPlayer.texture as RenderTexture;
            RenderTexture.active = renderTexture;
            videoTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            videoTexture.Apply();
        }

        GetPixelColor();
    }

    public void GetPixelColor()
    {
        if (videoTexture != null) {
            int videoOffsetX = (int)(videoTexture.width / 2);
            int videoOffsetY = (int)(videoTexture.height / 2);
            int xPosition = videoOffsetX + (int)playerController.playerTransform.localPosition.x;
            int yPosition = videoOffsetY + (int)playerController.playerTransform.localPosition.y - startingPosYOffset;

            // Check if the texture is not null and the position is within bounds
            if (videoTexture != null && xPosition >= 0 && xPosition < videoTexture.width && yPosition >= 0 && yPosition < videoTexture.height)
            {
                // Get the color of the specified pixel
                Color pixelColor = videoTexture.GetPixel(xPosition, yPosition);
                Debug.Log("Pixel Color at position (" + xPosition + ", " + yPosition + "): " + pixelColor);
                colorChecker.color = pixelColor;
            }
        }
    }
}
