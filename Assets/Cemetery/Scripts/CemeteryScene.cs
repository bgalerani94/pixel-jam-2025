using System.Collections;
using DialogueSystem.Scripts;
using UnityEngine;

namespace Cemetery.Scripts
{
    public class CemeteryScene : MonoBehaviour
    {
        [SerializeField] private float timeToShowDialog;
        [SerializeField, TextArea] private string introText;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(timeToShowDialog);
            DialogueBox.Instance.ShowText(introText);
        }
    }
}