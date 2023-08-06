using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

[CustomEditor(typeof(VideoPlayer))]
public class VideoPreviewInSceneView : Editor
{
    private VideoPlayer videoPlayer;

    private void OnEnable()
    {
        videoPlayer = (VideoPlayer)target;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (videoPlayer != null && videoPlayer.clip != null)
        {
            double currentTime = Time.timeSinceLevelLoad % videoPlayer.clip.length;

            if (Application.isPlaying)
            {
                // Update the video player's time while in Play mode
                videoPlayer.time = currentTime;
            }
            else
            {
                // Draw a label with the current time in the Scene view
                Handles.Label(videoPlayer.transform.position, $"Video Time: {currentTime}");
            }
        }
    }
}