using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame

    private Button _buttonComponent;

    private void Start()
    {
        _buttonComponent = gameObject.GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _buttonComponent.onClick.Invoke();
        }
    }
}
