using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Main")]
    [SerializeField] private Field _field;

    [Header("UI References")]
    [SerializeField] private TMP_InputField _inputSizeX; [Tooltip("Responsible for the number of letters on the X axis")]
    [SerializeField] private TMP_InputField _inputSizeY; [Tooltip("Responsible for the number of letters on the Y axis")]
    [SerializeField] private Button _generateButton;
    [SerializeField] private Button _replaceButton;

    #region Singleton

    private void Initialize()
    {
        if(Instance == null)
            Instance = this;
        else if(Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    #endregion

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        _generateButton.onClick.AddListener(() => GenerateField());   
        _replaceButton.onClick.AddListener(() => ReplaceField());
    }

    private void GenerateField()
    {
        if (_inputSizeX.text == string.Empty && _inputSizeY.text == string.Empty) 
            return;

        int x = int.Parse(_inputSizeX.text);
        int y = int.Parse(_inputSizeY.text);

        if (x <= 0 || y <= 0) // ≈сли не проходит по минимальной размерности - возвращаем.
            return;

        _field.ChangeSize(x, y); // »змен€ем размерность пол€.
    }

    private void ReplaceField()
    {
        _field.ReplaceLetters();
    }

}
