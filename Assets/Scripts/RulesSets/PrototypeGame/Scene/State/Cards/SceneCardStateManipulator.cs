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
		private CardObjectServices _cardObjectServices;

		private GameObject _cardPrefab;

		public SceneCardStateManipulator(CommonServices commonServices, CardObjectServices cardServices)
		{
			_commonServices = commonServices;
			_cardObjectServices = cardServices;
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
			cardObject.CardServices = _cardObjectServices;
			cardObject.CommonServices = _commonServices;
			cardObject.IsAlwaysLastInHand = protoCardData.IsAlwaysLastInHand;

			return cardObject;
		}

		public void AddCardToHand(ProtoCardObject cardObject, bool fromUndo)
		{
			_cardObjectServices.AddCard(cardObject, fromUndo);
		}


		public void RemoveCardFromHand(ProtoCardObject cardObject, bool fromUndo)
		{
			_cardObjectServices.RemoveCard(cardObject, fromUndo);
		}

		public void ReorganizeCardsInHand()
		{
			_cardObjectServices.ReorganaizeHand();
		}
	}
}
