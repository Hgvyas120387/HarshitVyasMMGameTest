using UnityEngine;
using TMPro;

namespace cyberspeed.MatchGame
{
    public class UIFPS : MonoBehaviour
    {
        private int FrameCounter = 0;
        [SerializeField] private TextMeshProUGUI txtFPS = null;

        void Awake()
        {
            FrameCounter = 0;
            InvokeRepeating("ShowFPS", 0f, 1f);
        }

        // Update is called once per frame
        void Update()
        {
            FrameCounter++;
        }

        private void ShowFPS()
        {
            txtFPS.text = $"FPS : {FrameCounter}";
            FrameCounter = 0;
        }
    }
}