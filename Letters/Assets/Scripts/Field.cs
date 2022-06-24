using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Field : MonoBehaviour
{
    [SerializeField] private GameObject _letterPrefab;
    private RectTransform _rectTransform;
    private GridLayoutGroup _gridLayout;
    private int _x = 0;
    private int _y = 0;

    [SerializeField] private List<string> _initializedLetters = new List<string>();
    [SerializeField] private List<GameObject> _initializedObjects = new List<GameObject>();

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _gridLayout = GetComponent<GridLayoutGroup>();
        ChangeSize(5, 5);
    }

    public void ChangeSize(int x, int y)
    {
        _gridLayout.cellSize = new Vector2((int)(1000 / x), (int)(1000 / y)); // Размер буквы зависит от клеток.

        _x = x;
        _y = y;

        CleanLetters();
        GenerateLetters();
    }

    private void GenerateLetters()
    {
        int size = _x * _y;
        for (int i = 0; i < size; i++)
        {
            int dec = Random.Range(65, 91); // получаем рандомный код символа.
            string letter = ((char)dec).ToString(); // переводим его код в символ, а затем в строку.

            GameObject obj = Instantiate(_letterPrefab, _rectTransform);
            obj.transform.localScale = Vector3.one;

            TMPro.TMP_Text text = obj.GetComponent<TMPro.TMP_Text>(); // присваиваем буковке текст.
            text.text = letter;

            _initializedObjects.Add(obj);
            _initializedLetters.Add(letter);
        }
    }

    public void ReplaceLetters()
    {
        List<string> _replacedLetters = new List<string>();
        int size = _x * _y;

        while (_initializedLetters.Count > 0) // Переставляем буквы с помощью рандома.
        {
            int index = Random.Range(0, _initializedLetters.Count);
            _replacedLetters.Add(_initializedLetters[index]);
            _initializedLetters.RemoveAt(index);
        }

        CleanLetters();

        for (int i = 0; i < size; i++) // Создаем переставленные буквы.
        {
            GameObject obj = Instantiate(_letterPrefab, _rectTransform);
            obj.transform.localScale = Vector3.one;

            TMPro.TMP_Text text = obj.GetComponent<TMPro.TMP_Text>(); // присваиваем буковке текст.
            text.text = _replacedLetters[i];

            _initializedObjects.Add(obj);
            _initializedLetters.Add(_replacedLetters[i]);
        }

        _replacedLetters.Clear(); 
    }

    private void CleanLetters() // Отчистка от ранее созданных букв.
    {
        foreach (GameObject obj in _initializedObjects)
            Destroy(obj);
        _initializedObjects.Clear();
    }

}
