namespace RSToolkit.VoxelEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using System;
    using UnityEngine;

    public class VoxelAnimator : MonoBehaviour {
        public Vector3 InitialPosition = new Vector3(0, 0, 0);
        public VoxelObj[] Frames;
        public int ActiveFrame { get; private set; }
        int m_LastFrame = -1;
        [SerializeField] private float m_fps = 5;
        public bool Loop = true;
        public bool WaitUntilAnimEnd = false;
        public bool IsAnimEnd { get; private set; }
        bool m_IsAnimEnding = false;

        // Use this for initialization
        void Start () {
            HideAllFrames();
            m_LastFrame = -1;
            ActiveFrame = 0;
            SetFps(m_fps);
            IsAnimEnd = false;
            m_IsAnimEnding = false;
        }

        public void SetToInitialPosition()
        {
            this.transform.localPosition = InitialPosition;
            this.transform.localEulerAngles = Vector3.zero;
        }

        public void SetFps(float fps)
        {
            CancelInvoke("GoToNextFrame");
            this.m_fps = fps;
            var invokeRate = 1f / (float)fps;
            InvokeRepeating("GoToNextFrame", 0, invokeRate);
        }

        void HideAllFrames()
        {
            for (int i = 0; i < Frames.Length; i++)
            {
                Frames[i].gameObject.SetActive(false);
            }
        }

        [SerializeField]private bool m_isAnimating = false;
        public bool IsAnimating {
            get
            {
                return m_isAnimating;
            }
            private set
            {
                m_isAnimating = value;
            }
        }

        public void Animate()
        {
            if (IsAnimating)
            {
                return;
            }

            IsAnimEnd = false;
            IsAnimating = true;
            m_IsAnimEnding = false;
            m_LastFrame = -1;
            SetFrame();

        }

        public void CloseAnimation()
        {
            ActiveFrame = 0;
            m_LastFrame = -1;
            m_isAnimating = false;
            this.gameObject.SetActive(false);
            HideAllFrames();
        }

        private void Awake()
        {
        }

        private float nextActionTime = 0.0f;
    
        // Update is called once per frame
        void Update () {
            if (!IsAnimating)
            {
                return;
            }
        }

        public void SetFrame()
        {
            for (int i = 0; i < Frames.Length; i++)
            {
                Frames[i].gameObject.SetActive(i == ActiveFrame);
            }
        }

        void GoToNextFrame()
        {
            if (IsAnimating)
            {
                if(m_IsAnimEnding){
                    m_IsAnimEnding = false;
                    IsAnimating = false;
                    IsAnimEnd = true;
                
                    //Debug.Log("Animation Ended");
                }
                if(m_LastFrame == 0 && !Loop){
                    return;
                }
                //Debug.LogFormat("Setting frame {0} on", ActiveFrame);
                SetFrame();
                ActiveFrame++;
                if (ActiveFrame >= Frames.Length)
                {
                    ActiveFrame = 0;
                    if (!Loop)
                    {
                        //StartCoroutine(AnimatingLastFrame());
                        m_IsAnimEnding = true;
                    }
                }
                
                m_LastFrame = ActiveFrame;
            }
        }

        void OnValidate()
        {
            if(m_fps < 0f)
            {
                m_fps = 0.1f;
            }
            
        }

        /* 
            IEnumerator AnimatingLastFrame()
            {
                yield return new WaitForSeconds(1f / (float)fps);
                IsAnimating = false;
                IsAnimEnd = true;
            }
        */
    }
}