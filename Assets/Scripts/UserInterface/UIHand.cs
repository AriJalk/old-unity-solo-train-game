using Engine;
using SoloTrainGame.GameLogic;
using UnityEngine;
using UnityEngine.UI;

public class UIHand : MonoBehaviour
{
    [SerializeField]
    private HorizontalLayoutGroup content;

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.PrefabManager.LoadAndRegisterPrefab<CardUIObject>(Engine.ResourceManagement.PrefabFolder.PREFAB_2D, "CardPrefab", 50);
        BuildTestHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BuildTestHand()
    {

        foreach (CardSO cardSO in ServiceLocator.ScriptableObjectManager.CardTypes)
        {
            CardInstance cardData = new CardInstance(cardSO);
            CardUIObject cardObject = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
            cardObject?.SetCard(cardData);
            cardObject?.transform.SetParent(content.transform);
        }
    }
}
