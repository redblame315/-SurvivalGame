using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldTextManager : MonoBehaviour
{

    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Gold:" + GameManager.Instance.gold;
    }
}