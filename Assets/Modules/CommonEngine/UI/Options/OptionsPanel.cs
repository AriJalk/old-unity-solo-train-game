using CommonEngine.Core;
using CommonEngine.SceneServices;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace CommonEngine.UI.Options
{
    public class OptionPanel : MonoBehaviour
    {
        public event Action<Guid> OptionSelectedEvent;

        [SerializeField]
        private CommonServices _commonServices;

        public Transform OptionsContainer;

        private ICollection<OptionObject> _options;
        private Guid _selectedOption;

        private void OnOptionSelected(Guid guid)
        {
            _selectedOption = guid;
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
        }
    }
}