using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SoloTrainGame.UI
{
    public class LevelEditorGUI : CoreGUI
    {
        public LevelEditorGUIEvents LevelEditorGUIEvents { get; private set; }

        private void Awake()
        {

        }
        // Start is called before the first frame update
        void Start()
        {
            
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            LevelEditorGUIEvents = new LevelEditorGUIEvents(this);
        }

        private void OnDestroy()
        {

        }

    }

}
