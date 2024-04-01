using SoloTrainGame.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicUserInterface : MonoBehaviour
{
    public UIHand Hand;
    public UICardView CardView;

    static public bool IsMouseOver;

    static public bool IsUILocked;

    private CardInstance _selectedCard;

    // Start is called before the first frame update
    void Start()
    {
        Hand.CardClickedEvent.AddListener(CardClicked);
        CardView.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CardClicked(CardUIObject card)
    {
        if (card != null)
        {
            // TODO: States here
            _selectedCard = card.CardInstance;
            CardView.SetCard(card.CardInstance);
            CardView.gameObject.SetActive(true);
    }
    }


}
