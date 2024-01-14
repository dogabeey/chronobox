using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Doga.SilentCity
{
    public class InteractableUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public TMP_Text interactableName;
        public TMP_Text interactText;

        InteractableObject interactableObject;

        private void OnEnable()
        {
            EventManager.StartListening(Const.GameEvents.PLAYER_ENTERED_RANGE, OnPlayerEnteredRange);
            EventManager.StartListening(Const.GameEvents.PLAYER_EXITED_RANGE, OnPlayerExitedRange);
        }
        private void OnDisable()
        {
            EventManager.StopListening(Const.GameEvents.PLAYER_ENTERED_RANGE, OnPlayerEnteredRange);
            EventManager.StopListening(Const.GameEvents.PLAYER_EXITED_RANGE, OnPlayerExitedRange);
        }
        private void OnPlayerEnteredRange(EventParam param)
        {
            canvasGroup.alpha = 1;
            interactableObject = param.paramObj.GetComponent<InteractableObject>();
            // Convert interactable's world position to screen position and set UI position.
            Vector3 screenPos = Camera.main.WorldToScreenPoint(interactableObject.transform.position);
            transform.position = screenPos;
            // Set Values
            interactableName.text = interactableObject.name;
            interactText.text = interactableObject.interactString;
        }
        private void OnPlayerExitedRange(EventParam param)
        {
            canvasGroup.alpha = 0;
            interactableObject = null;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
