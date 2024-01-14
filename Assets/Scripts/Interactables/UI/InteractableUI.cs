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
            EventManager.StartListening(Const.GameEvents.PLAYER_PICKED_OBJECT, OnPlayerPickedObject);
            EventManager.StartListening(Const.GameEvents.PLAYER_DROPPED_OBJECT, OnPlayerDroppedObject);
        }
        private void OnDisable()
        {
            EventManager.StopListening(Const.GameEvents.PLAYER_ENTERED_RANGE, OnPlayerEnteredRange);
            EventManager.StopListening(Const.GameEvents.PLAYER_EXITED_RANGE, OnPlayerExitedRange);
            EventManager.StopListening(Const.GameEvents.PLAYER_PICKED_OBJECT, OnPlayerPickedObject);
            EventManager.StopListening(Const.GameEvents.PLAYER_DROPPED_OBJECT, OnPlayerDroppedObject);
        }
        private void OnPlayerEnteredRange(EventParam param)
        {
            if(param.paramObj.GetComponent<InteractableObject>() == interactableObject)
            {
                canvasGroup.alpha = 1;
            }
        }
        private void OnPlayerExitedRange(EventParam param)
        {
            if (param.paramObj.GetComponent<InteractableObject>() == interactableObject)
            {
                canvasGroup.alpha = 0;
            }
        }
        private void OnPlayerPickedObject(EventParam param)
        {
            if (param.paramObj.GetComponent<InteractableObject>() == interactableObject)
            {
                canvasGroup.alpha = 0;
            }
        }
        private void OnPlayerDroppedObject(EventParam param)
        {
            if (param.paramObj.GetComponent<InteractableObject>() == interactableObject)
            {
                canvasGroup.alpha = 1;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            interactableObject = GetComponentInParent<InteractableObject>();
            // Set Values
            interactableName.text = interactableObject.interactableName;
            interactText.text = interactableObject.interactString;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
