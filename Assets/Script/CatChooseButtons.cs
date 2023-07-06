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
    [SerializeField] private Transform CatPrefab;
    [SerializeField] private GameObject Customizer;
    //[SerializeField] private List<GameObject> CatPrefabs;
    // Start is called before the first frame update

    private void Awake()
    {
        CreateCatButtons();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CreateCatButtons"> The current world that the player is going to </param>
    public void CreateCatButtons()
    {
        // Destorying previous buttons that will no longer be used
        foreach (Transform buttonTransform in this.transform)
        {
            Destroy(buttonTransform.gameObject);
        }

        // Checking to make sure that there is 10 levels to create and if not reducing the amount of levels created
        int AmountOfButtons = 5;

        // Creating 5 buttons for the current world that the player has entered
        for (int i = 0; i < AmountOfButtons; i++)
        {
            // Creates a button in the level select
            Transform CatButtonTransform = Instantiate(CatPrefab, this.transform);
            int CurrentButton = i;
            CatButtonTransform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.GetCatPrefab(CurrentButton).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            CatButtonTransform.GetChild(1).GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.GetCatPrefab(CurrentButton).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
            // Sets the text of the button to the respective level
            CatButtonTransform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cat: " + (i + 1);
            // Creates the action to go to the Customize Window
            CatButtonTransform.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance._catInfoManager.GoToCustomize(CurrentButton, Customizer));
        }
    }
}
