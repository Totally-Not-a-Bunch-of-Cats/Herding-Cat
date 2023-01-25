using UnityEngine;

public class Scale : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance._screenResizeManager.RescaleItem(gameObject);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance._screenResizeManager.RescaleItem(gameObject);
        }
    }
}
