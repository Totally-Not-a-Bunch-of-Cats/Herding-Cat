using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2021.3.2f1 
*/

//* Creates the Cat Buttons dynamically
public class CatChooseButtons : MonoBehaviour
{
    //Cat Prefab
    [SerializeField] private int childNum;
    // Start is called before the first frame update

    private void OnEnable()
    {
        childNum = transform.GetSiblingIndex();
        CreateCatButtons();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CreateCatButtons"> The current world that the player is going to </param>
    public void CreateCatButtons()
    {
        // Creating 5 buttons for the current world that the player has entered
        // Creates a button in the level select
        transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = GameManager.Instance._catInfoManager.Catlist[childNum].AnimationController;
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.Catlist[childNum].Acessory1;
        GameManager.Instance._catInfoManager.CurrentName = GameManager.Instance._catInfoManager.Catlist[childNum].nameofAcessory1;
        Debug.Log(transform.GetChild(0).GetChild(0));
        transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().offsetMax = -GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.GetAccessoryIndex()].MaxoffsetforExternalCatButton;
        transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().offsetMin = GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.GetAccessoryIndex()].MinoffsetforExternalCatButton;
        // Sets the text of the button to the respective level
        transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cat: " + (childNum + 1);
        Debug.Log(childNum);
    }
}
