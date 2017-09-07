﻿using System.Collections;

        public delegate void PlaybackAction(VideoController controller);
        public event PlaybackAction OnPause;
        public event PlaybackAction OnPlay;

        public GameObject videoScreenObject;
        public int width;

        public  VideoPlayer videoPlayer;

        private void Setup()
        {
            if (videoPlayer == null) {
                videoPlayer = videoScreenObject.AddComponent<VideoPlayer>();

                if (audioSource == null) {
                    audioSource = videoScreenObject.AddComponent<AudioSource>();
                }

                audioSource.volume = 1;

                videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
                videoPlayer.isLooping = true;
            }
            else {
                Pause();
                videoPlayer.Stop();
            }
        }

            Setup();
            videoPlayer.url = file_url;
            //StartCoroutine("PlayVideo");
        }

        IEnumerator PlayVideo()
        {
            videoPlayer.Play();
            yield return null;
        }

        public void TogglePlayback()
        {
            if (videoPlayer.isPlaying){
                Pause();
            }
            else {
                Play();
            }
        }

        public void Pause()
        {
            videoPlayer.Pause();
            if (OnPause != null) OnPause(this);
        }

        public void Play()
        {
            videoPlayer.Play();
            if (OnPlay != null) OnPlay(this);
        }

        private void VideoPlayerStarted(VideoPlayer source)
        {
            if (OnVideoStart != null) {
                this.width  = videoPlayer.texture.width;
                this.height = videoPlayer.texture.height;
            }
        }

        private void OnDisable()
        {
            videoPlayer.prepareCompleted -= VideoPlayerStarted;
        }

        //private void EndReached(VideoPlayer vp)
        //{
        //    vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        //}