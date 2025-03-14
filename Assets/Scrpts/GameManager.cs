using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject thing1;
    [SerializeField] bool thing1Active;
    [SerializeField] private GameObject thing2;
    [SerializeField] bool thing2Active;
    [SerializeField] private GameObject thing3;
    [SerializeField] bool thing3Active;

    void Awake()
    {
        //ting 1
        if (thing1Active == true)
        {
            thing1.SetActive(true);
        }
        else
        {
            thing1.SetActive(false);
        }
        //thing 2
        if (thing1Active == true)
        {
            thing2.SetActive(true);
        }
        else
        {
            thing2.SetActive(false);
        }
        //Thing 3
        if (thing1Active == true)
        {
            thing3.SetActive(true);
        }
        else
        {
            thing3.SetActive(false);
        }
    }
}
