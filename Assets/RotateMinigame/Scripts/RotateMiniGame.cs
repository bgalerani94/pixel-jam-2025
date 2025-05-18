using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RotateMinigame.Scripts
{
    public class RotateMiniGame : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float showAnimTime;
        [SerializeField] private float rotateAnimTime;

        [Header("Components")]
        [SerializeField] private Player.Scripts.Player player;
        [SerializeField] private GameObject gameHolder;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private List<Button> pieces;

        private readonly List<int> _pieceRotations = new() { 90, 180, 270 };

        private int _correctCount;

        private void Start()
        {
            pieces.ForEach(piece => piece.onClick.AddListener(() => OnPieceClicked(piece)));
        }

        public void OpenMiniGame()
        {
            player.CanMove = false;
            _correctCount = 0;
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0;
            gameHolder.SetActive(true);
            pieces.ForEach(piece =>
            {
                piece.transform.rotation =
                    Quaternion.Euler(0, 0, _pieceRotations[Random.Range(0, _pieceRotations.Count)]);
                piece.interactable = true;
                piece.image.color = Color.white;
            });

            canvasGroup.DOFade(1, showAnimTime).OnComplete(() => canvasGroup.interactable = true);
        }

        private void OnPieceClicked(Button piece)
        {
            StartCoroutine(RotatePiece(piece));
        }

        private void OnCompleted()
        {
            canvasGroup.DOFade(0, showAnimTime).OnComplete(() =>
            {
                player.CanMove = true;
                canvasGroup.interactable = false;
                gameHolder.SetActive(false);
            });
        }

        private IEnumerator RotatePiece(Button pieceButton)
        {
            var piece = pieceButton.GetComponent<RectTransform>();
            canvasGroup.interactable = false;
            var currentRotation = (piece.rotation.eulerAngles.z - 90) % 360;

            var anim = piece.transform.DOLocalRotate(new Vector3(0, 0, currentRotation), rotateAnimTime);
            yield return anim.WaitForCompletion();
            canvasGroup.interactable = true;

            if (piece.rotation.eulerAngles.z == 0)
            {
                pieceButton.image.DOColor(Color.gray, rotateAnimTime);
                pieceButton.interactable = false;
                _correctCount++;
            }

            if (_correctCount == pieces.Count)
            {
                OnCompleted();
            }
        }
    }
}