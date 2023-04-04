using TMPro;
using UnityEngine;

namespace TS.View
{
    public class PlayerBalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceText;
        [SerializeField] private Transform _originalParent;

        public void UpdateView(int balance)
        {
            _balanceText.text = balance.ToString();
        }

        public void Relocate(Transform parent, Vector3 position)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }

        public void ResetLocation()
        {
            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}