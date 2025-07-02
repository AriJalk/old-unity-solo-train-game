using CommonEngine.Core;
using CommonEngine.Helpers;
using CommonEngine.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace CommonEngine.UI.Options
{
    /// <summary>
    /// General purpose option selection panel
    /// </summary>
    public class OptionsPanel : MonoBehaviour
    {
        public event Action<Guid> OptionSelectedEvent;
        public event Action CancelEvent;

        [SerializeField]
        private CommonServices _commonServices;
        [SerializeField]
        private Button _cancelButton;
        [SerializeField]
        GridSizeScaler _gridSizeScaler;

        public Transform OptionsContainer;

        private ICollection<OptionObject> _options;

        private void OnOptionSelected(Guid guid)
        {
            OptionSelectedEvent?.Invoke(guid);
        }

        public void OpenPanel(IEnumerable<OptionObject> options)
        {
            ClosePanel();

            _options = new List<OptionObject>(options);

            foreach (OptionObject option in options)
            {
                option.SelectedEvent += OnOptionSelected;
                SceneHelpers.SetParentAndResetPosition(option.transform, OptionsContainer);
            }
            _commonServices.InputLock.AddLock(gameObject);
            gameObject.SetActive(true);
            _cancelButton.onClick.AddListener(OnCancelClicked);
            _gridSizeScaler.UpdateCellSize(options.Count());
        }

        public void ClosePanel()
        {
            if (_options != null)
            {
                foreach (OptionObject option in _options)
                {
                    option.SelectedEvent -= OnOptionSelected;
                    GameObject.Destroy(option.gameObject);
                }
                _options.Clear();
            }
            _commonServices.InputLock.RemoveLock(gameObject);
			gameObject.SetActive(false);
			_cancelButton.onClick.RemoveListener(OnCancelClicked);
		}

        private void OnCancelClicked()
        {
            CancelEvent?.Invoke();
        }
    }
}