using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Text.RegularExpressions;

/// <summary>
/// Input Validator for InputFields
/// Takes a ScriptableObject containing a regex pattern and tries to match it with the input
/// </summary>
public class InputField_Validator : MonoBehaviour
{
    public UnityEvent OnInputValid;
    public UnityEvent OnInputInvalid;

    [SerializeField]
    private TMP_InputField _inputFieldToCheck;

    [SerializeField]
    private RegexValidationPattern _validationPattern;

    private bool _isValid = false;
    public bool isValid { get { return _isValid; } }
    
    void Start()
    {
        Debug.Assert(_validationPattern != null, "Assertion Error: No RegexValidationPattern Object specified.", this);

        // Try to find InputField on GameObject if not set in Inspector
        if(_inputFieldToCheck == null)
        {
            _inputFieldToCheck = GetComponent<TMP_InputField>();
        }

        Debug.Assert(_inputFieldToCheck != null, "Assertion Error: No InputField to check.", this);

        if (_inputFieldToCheck)
        {
            _inputFieldToCheck.onValueChanged.AddListener(delegate { ValidateInput(); });

            // Initial Input Validation
            ValidateInput();
        }
    }

    private void ValidateInput()
    {
        Match regexMatcher = _validationPattern.regexExpr.Match(_inputFieldToCheck.text);
        if (regexMatcher.Success)
        {
            OnInputValid.Invoke();
            _isValid = true;
        }
        else
        {
            OnInputInvalid.Invoke();
            _isValid = false;
        }
    }
    
}
