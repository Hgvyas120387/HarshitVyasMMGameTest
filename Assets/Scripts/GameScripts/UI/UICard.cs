using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using cyberspeed.Services;

namespace cyberspeed.MatchGame
{
    public class UICard : MonoBehaviour
    {
        public int pIndex { get { return index; } }
        [SerializeField] private Image imgCard = null;
        [SerializeField] private Sprite[] spritesForCard = null;
        [SerializeField] private Sprite spriteForCardFaceDown = null;
        [SerializeField] private float rotationSpeed;
        private bool isFacedDown = true;
        private bool isCardSwapped = false;
        private int index;

        public void SetData(int index)
        {
            this.index = index;
        }
        //called from editor
        public void OnClicked()
        {
            if (enabled)//to avoid accidental double click
                return;
            enabled = true;
            isCardSwapped = false;
        }

        public IEnumerator HideCard(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            imgCard.enabled = false;
        }

        public IEnumerator MakeCardFaceDown(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            isCardSwapped = false;
            enabled = true;
        }

        private void Awake()
        {
            enabled = false;
        }
        
        private void Update()
        {
            //reveal the card
            if (isFacedDown)
            {
                RotateCard(spritesForCard[index], false);
            }
            //hide the card
            else
            {
                RotateCard(spriteForCardFaceDown, true);
            }
        }

        private void RotateCard(Sprite targetSprite,bool targetFaceDown)
        {
            if (isCardSwapped == false)
            {
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                if (transform.rotation.eulerAngles.y > 90)
                {
                    imgCard.sprite = targetSprite;
                    isCardSwapped = true;
                }
            }
            else
            {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
                if (transform.rotation.eulerAngles.y < 0 || transform.rotation.eulerAngles.y > 300)
                {
                    enabled = false;
                    isFacedDown = targetFaceDown;
                    if (targetFaceDown == false)
                        ServiceLocator.Singleton.Get<IGameModeService>().CardOpened(this);
                    transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}