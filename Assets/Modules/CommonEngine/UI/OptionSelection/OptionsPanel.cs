using CommonEngine.SceneServices;
using CommonEngine.UI.OptionSelection;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public event Action<Guid> OptionSelectedEvent;

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

        gameObject.SetActive(false);
    }
}
