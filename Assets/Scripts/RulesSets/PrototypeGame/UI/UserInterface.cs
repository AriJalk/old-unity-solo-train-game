using CommonEngine.UI.Options;
using PrototypeGame.UI.Options;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PrototypeGame.UI
{
	internal class UserInterface : MonoBehaviour
	{
		[SerializeField]
		OptionPanel optionPanel;
		public void Update()
		{
			// Test option creation
			if (Keyboard.current.xKey.wasPressedThisFrame && !optionPanel.isActiveAndEnabled)
			{
				GameObject prefab = Resources.Load<GameObject>("Prefabs/PrototypeGame/UI/BuildingOption");
				List<OptionObject> list = new List<OptionObject>();
				BuildingOption option;

				for (int i = 0; i < 10; i++)
				{
					option = GameObject.Instantiate(prefab).GetComponent<BuildingOption>();
					option.Setup(Guid.NewGuid(), "Factory", isEnabled: true);
					list.Add(option);
				}

				for (int i = 0; i < 10; i++)
				{
					option = GameObject.Instantiate(prefab).GetComponent<BuildingOption>();
					option.guid = Guid.NewGuid();
					option.Setup(Guid.NewGuid(), "Station");
					list.Add(option);
				}
				optionPanel.OpenPanel(list);
				optionPanel.OptionSelectedEvent += OptionSelected;
			}
		}

		private void OptionSelected(Guid guid)
		{
			Debug.Log(guid);
			optionPanel.OptionSelectedEvent -= OptionSelected;
			optionPanel.ClosePanel();
		}
	}
}
