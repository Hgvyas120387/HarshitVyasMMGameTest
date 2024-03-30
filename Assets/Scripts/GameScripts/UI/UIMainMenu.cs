using UnityEngine;
using cyberspeed.Services;
using TMPro;
using System;

namespace cyberspeed.MatchGame.UI
{
    
    public class UIMainMenu : MonoBehaviour
    {
        /// <summary>
        /// class to fill drop down data
        /// </summary>
        [Serializable]
        public class DropDownData
        {
            public int rowCount;
            public int columnCount;
            public string strSelection { get { return $"{rowCount} x {columnCount}"; } }
        }

        [SerializeField] private TMP_Dropdown dropDownGameModeSelection;
        [SerializeField] private DropDownData[] dropDownDatas;
        private int rows, columns;

        private void Start()
        {
            rows = dropDownDatas[0].rowCount;
            columns = dropDownDatas[0].columnCount;
            //first clear anything inside drop down
            dropDownGameModeSelection.options.Clear();
            //fill the data in drop down
            for(int i = 0; i < dropDownDatas.Length; i++)
            {
                dropDownGameModeSelection.options.Add(new TMP_Dropdown.OptionData(dropDownDatas[i].strSelection));
            }
        }
        //Called from editor
        public void OnBtnPlayClicked()
        {
            //set the game mode
            ServiceLocator.Singleton.Get<IGameModeService>().SetGameGrid(rows, columns);
            //change state to game play
            ServiceLocator.Singleton.Get<IFSMService>().ChangeState(States.GamePlay.ToString());
        }
        //Called from editor
        public void DropdownValueChanged(TMP_Dropdown change)
        {
            rows = dropDownDatas[change.value].rowCount;
            columns = dropDownDatas[change.value].columnCount;
            Debug.Log($"Drop down selection {change.options[change.value].text} Rows : {rows} columns : {columns}");
        }
    }
}