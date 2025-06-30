using CardSystem;
using CommonEngine.Core;
using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Scene.Components.Cards;
using UnityEngine;

namespace PrototypeGame.Scene.State.Cards
{
	internal class SceneCardStateManipulator
	{
		private CommonServices _commonServices;
		private CardObjectServices _cardServices;

		private GameObject _cardPrefab;

		public SceneCardStateManipulator(CommonServices commonServices, CardObjectServices cardServices)
		{
			_commonServices = commonServices;
			_cardServices = cardServices;
			_cardPrefab = Resources.Load<GameObject>("Prefabs/PrototypeGame/ProtoCardPrefab");
		}

		public ProtoCardObject BuildCard(ProtoCardData protoCardData)
		{
			ProtoCardObject cardObject = GameObject.Instantiate(_cardPrefab).GetComponent<ProtoCardObject>();
			cardObject.guid = protoCardData.guid;
			cardObject.CardTitle.text = protoCardData.CardTitle;
			cardObject.MoneyLabel.text = protoCardData.MoneyValue + "$";
			cardObject.TransportPointsLabel.text = protoCardData.TransportPointsValue + "TP";
			cardObject.CardDescription.text = protoCardData.CardDescription;
			cardObject.CardServices = _cardServices;
			cardObject.CommonServices = _commonServices;

			return cardObject;
		}

		public void AddCardToHand(ProtoCardObject cardObject)
		{
			_cardServices.AddCard(cardObject);
		}

		public void RemoveCardFromHand(ProtoCardObject cardObject)
		{
			_cardServices.RemoveCard(cardObject);
		}
	}
}
