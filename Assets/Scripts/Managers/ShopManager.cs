using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{

    public GameObject shop;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public Inventory inv;
    RaycastHit hit;
    public bool shopOpen;
    // public GameManager GameManager;
    int currentPage;

    void Start()
    {
        shopOpen = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            gameObject.GetComponent<Collider>().Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity) && !shopOpen)
        {
            shopOpen = true;
            showMenu();
        }
    }


    void showMenu()
    {
        shop.SetActive(true);
        page1.SetActive(true);
        currentPage = 1;
    }

    public void closeMenu()
    {
        shopOpen = false;
        shop.SetActive(false);
    }
    public void nextPage() // оче плохой код
    {
        if (currentPage == 1)
        {
            page1.SetActive(false);
            page2.SetActive(true);
            currentPage = 2;
        }
        else
        if (currentPage == 2)
        {
            page2.SetActive(false);
            page3.SetActive(true);
            currentPage = 3;
        }
        else
        if (currentPage == 3)
        {
            page3.SetActive(false);
            page1.SetActive(true);
            currentPage = 1;
        }
    }
    public void prevPage()
    {
        if (currentPage == 1)
        {
            page1.SetActive(false);
            page3.SetActive(true);
            currentPage = 3;
        }
        else
        if (currentPage == 2)
        {
            page2.SetActive(false);
            page1.SetActive(true);
            currentPage = 1;
        }
        else
        if (currentPage == 3)
        {
            page3.SetActive(false);
            page2.SetActive(true);
            currentPage = 2;
        }
    }

    public void buyHP()
    {
        if (GameManager.Instance.gold >= 300)
        {
            GameManager.Instance.gold -= 300;
            inv.addItem(1);
        }
    }
    public void buySpeed()
    {
        if (GameManager.Instance.gold >= 700)
        {
            GameManager.Instance.gold -= 700;
            inv.addItem(8);
        }
    }
    public void buyRate()
    {
        if (GameManager.Instance.gold >= 700)
        {
            GameManager.Instance.gold -= 700;
            inv.addItem(9);
        }
    }

    public void buyHelm()
    {
        if (GameManager.Instance.gold >= 1000)
        {
            GameManager.Instance.gold -= 1000;
            inv.addItem(3);
        }
    }

    public void buyArmor()
    {
        if (GameManager.Instance.gold >= 1000)
        {
            GameManager.Instance.gold -= 1000;
            inv.addItem(0);
        }
    }
    public void buyBoots()
    {
        if (GameManager.Instance.gold >= 1000)
        {
            GameManager.Instance.gold -= 1000;
            inv.addItem(5);
        }
    }
    public void buyGloves()
    {
        if (GameManager.Instance.gold >= 1000)
        {
            GameManager.Instance.gold -= 1000;
            inv.addItem(4);
        }
    }
    public void buyRing()
    {
        if (GameManager.Instance.gold >= 1000)
        {
            GameManager.Instance.gold -= 1000;
            inv.addItem(6);
        }
    }
    public void buyRapier()
    {
        if (GameManager.Instance.gold >= 1000)
        {
            GameManager.Instance.gold -= 1000;
            inv.addItem(2);
        }
    }
}