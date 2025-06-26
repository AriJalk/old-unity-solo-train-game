using CommonEngine.UI.OptionSelection;
using PrototypeGame.UI.Options;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.UI
{
	internal class UserInterface : MonoBehaviour
	{
		[SerializeField]
		OptionPanel optionPanel;
		public void Update()
		{
			GameObject prefab = Resources.Load<GameObject>("Prefabs/PrototypeGame/UI/BuildingOption");
			if (Keyboard.current.xKey.wasPressedThisFrame && !optionPanel.isActiveAndEnabled)
			{
				List<OptionObject> list = new List<OptionObject>();
				BuildingOption option;

				for (int i = 0; i < 3; i++)
				{
					option = GameObject.Instantiate(prefab).GetComponent<BuildingOption>();
					option.guid = Guid.NewGuid();
					option.Setup("Factory");
					list.Add(option);
				}

				for (int i = 0; i < 3; i++)
				{
					option = GameObject.Instantiate(prefab).GetComponent<BuildingOption>();
					option.guid = Guid.NewGuid();
					option.Setup("Station");
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
