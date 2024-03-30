using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using cyberspeed.Services;
using cyberspeed.Pooling;

namespace cyberspeed.MatchGame.UI
{
    public class UICard : MonoBehaviour
    {
        public int pIndex { get { return index; } }
        public bool pIsCardClosed { get { return imgCard.enabled == false; } }
        public bool pIsCardFaceDown { get { return isFacedDown; } }
        [SerializeField] private Image imgCard = null;
        [SerializeField] private Sprite[] spritesForCard = null;
        [SerializeField] private Sprite spriteForCardFaceDown = null;
        [SerializeField] private AudioClip audioCardFlip = null;
        [SerializeField] private AudioClip audioMatchSuccess = null;
        [SerializeField] private AudioClip audioMatchFailed = null;
        [SerializeField] private float rotationSpeed;
        
        private bool isFacedDown = true;
        private bool isCardSwapped = false;
        private int index;

        public void Reset()
        {
            index = 0;
            isFacedDown = true;
            isCardSwapped = false;
            imgCard.sprite = spriteForCardFaceDown;
            imgCard.enabled = true;
            ServiceLocator.Singleton.Get<IPoolService>().ReturnToPool(gameObject);
        }
        public void SetData(int index)
        {
            this.index = index;
        }
        //called from editor
        public void OnClicked()
        {
            if (enabled || isFacedDown == false)//to avoid accidental double click
                return;
            enabled = true;
            isCardSwapped = false;
            ServiceLocator.Singleton.Get<IAudioService>().PlayAudioOneShot(audioCardFlip);
        }

        public void HideCardForSavedGame()
        {
            imgCard.enabled = false;
        }

        public void MakeCardFaceUpForSavedGame()
        {
            imgCard.sprite = spritesForCard[index];
            isFacedDown = false;
            ServiceLocator.Singleton.Get<IGameModeService>().CardOpened(this);
        }

        public IEnumerator HideCard(float delay)
        {
            ServiceLocator.Singleton.Get<IAudioService>().PlayAudioOneShot(audioMatchSuccess);
            yield return new WaitForSecondsRealtime(delay);
            imgCard.enabled = false;
        }

        public IEnumerator MakeCardFaceDown(float delay)
        {
            ServiceLocator.Singleton.Get<IAudioService>().PlayAudioOneShot(audioMatchFailed);
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