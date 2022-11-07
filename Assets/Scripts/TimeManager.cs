using UnityEngine;
using UnityEngine.UI;

namespace Exion.Handler
{
    public class TimeManager : MonoBehaviour
    {
        private int m_timeElapsed = 0;

        [SerializeField]
        private Light sun;

        public Image pauseIndicator;
        public Sprite pauseSprite;
        public Sprite playSprite;

        public string Time
        {
            get { return m_time; }
        }

        [SerializeField]
        private string m_time = "Morning";

        public bool pause = true;

        // Start is called before the first frame update
        private void Update()
        {
            if (Input.GetButtonDown("Jump")) pause = !pause;
            switch (m_time)
            {
                case "Morning":
                    sun.color = Color.Lerp(sun.color, Color.white, 0.1f);
                    break;

                case "Work":
                    sun.color = Color.Lerp(sun.color, Color.yellow, 0.1f);
                    break;

                case "Free Time":
                    sun.color = Color.Lerp(sun.color, Color.red, 0.1f);
                    break;

                case "Night":
                    sun.color = Color.Lerp(sun.color, Color.blue, 0.1f);
                    break;

                default:
                    break;
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!pause)
            {
                m_timeElapsed++;
                if (pauseIndicator.sprite == pauseSprite) pauseIndicator.sprite = playSprite;
                switch (m_time)
                {
                    case "Morning":
                        sun.color = Color.Lerp(sun.color, Color.white, 0.1f);
                        if (m_timeElapsed == 15)
                        {
                            m_timeElapsed = 0;
                            m_time = "Work";
                        }
                        break;

                    case "Work":
                        sun.color = Color.Lerp(sun.color, Color.yellow, 0.1f);
                        if (m_timeElapsed == 30)
                        {
                            m_timeElapsed = 0;
                            m_time = "End Work";
                        }
                        break;

                    case "End Work":
                        sun.color = Color.Lerp(sun.color, Color.yellow, 0.1f);
                        if (m_timeElapsed == 1)
                        {
                            m_timeElapsed = 0;
                            m_time = "Free Time";
                        }
                        break;

                    case "Free Time":
                        sun.color = Color.Lerp(sun.color, Color.red, 0.1f);
                        if (m_timeElapsed == 20)
                        {
                            m_timeElapsed = 0;
                            m_time = "End Free";
                        }
                        break;

                    case "End Free":
                        sun.color = Color.Lerp(sun.color, Color.magenta, 0.1f);
                        if (m_timeElapsed == 1)
                        {
                            m_timeElapsed = 0;
                            m_time = "Night";
                        }
                        break;

                    case "Night":
                        sun.color = Color.Lerp(sun.color, Color.blue, 0.1f);
                        if (m_timeElapsed == 10)
                        {
                            m_timeElapsed = 0;
                            m_time = "End Night";
                        }
                        break;

                    case "End Night":
                        if (m_timeElapsed == 1)
                        {
                            m_timeElapsed = 0;
                            m_time = "Morning";
                        }
                        break;

                    default:
                        break;
                }
            } else if (pauseIndicator.sprite == playSprite) pauseIndicator.sprite = pauseSprite;
        }
    }
}