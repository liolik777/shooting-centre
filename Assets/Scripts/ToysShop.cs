using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Toy
{
    public GameObject showCaseToy;
    public GameObject originalToy;
    public int price;
}

public class ToysShop : MonoBehaviour
{
    [SerializeField] private Transform toySpawner;
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private List<Toy> toys;
    private int _selectedToyIndex;

    private int _balance;
    
    private void Start()
    {
        AddBalance(PlayerPrefs.GetInt("balance"));
        SelectToy(0);
    }

    public void AddBalance(int adding)
    {
        _balance += adding;
        PlayerPrefs.SetInt("balance", _balance);
        balanceText.text = $"Ваш баланс: {_balance}";
    }

    public void TakeBalance(int taken)
    {
        AddBalance(-taken);
    }
    
    public void BuyToy()
    {
        Toy selectedToy = toys[_selectedToyIndex];
        if (_balance < selectedToy.price)
            return;
        
        TakeBalance(selectedToy.price);
        Instantiate(selectedToy.originalToy, toySpawner.position, toySpawner.rotation);
    }
    
    public void NextToy()
    {
        SelectToy(_selectedToyIndex + 1);
    }

    public void PreviousToy()
    {
        SelectToy(_selectedToyIndex - 1);
    }

    private void SelectToy(int toyIndex)
    {
		if (toyIndex >= toys.Count)
			toyIndex = 0;
		if (toyIndex < 0)
			toyIndex = toys.Count - 1;
	
        toys[_selectedToyIndex].showCaseToy.SetActive(false);
        toys[toyIndex].showCaseToy.SetActive(true);
        _selectedToyIndex = toyIndex;
        priceText.text = $"Цена: {toys[toyIndex].price} очков";
    }
}