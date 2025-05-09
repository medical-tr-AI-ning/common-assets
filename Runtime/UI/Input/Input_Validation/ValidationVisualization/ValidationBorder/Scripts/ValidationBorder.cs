using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ValidationBorder : MonoBehaviour
{
    #region variables
    private Image _validationBorder;
    private InputField_Validator _validator;

    [SerializeField] private Color _validColor = Color.green;
    [SerializeField] private Color _invalidColor = Color.red;
    #endregion

    #region unity_functions
    void Awake()
    {
        _validationBorder = GetComponent<Image>();
        _validationBorder.color = _invalidColor;

        if(!transform.parent)
        {
            Debug.LogError("Required parent transform not found.", this);
            return;
        }

        bool parentHasInputField = transform.parent.GetComponent<TMPro.TMP_InputField>();
        if (!parentHasInputField)
        {
            Debug.LogError("No InputField component found on parent.", this);
            return;
        }

        _validator = transform.parent.GetComponent<InputField_Validator>();
        if (!_validator)
        {
            Debug.LogError("No InputField_Validator component found on parent.", this);
            return;
        }

        _validator.OnInputValid.AddListener(SetBorderValid);
        _validator.OnInputInvalid.AddListener(SetBorderInvalid);
    }
    #endregion

    #region public_functions
    public void SetBorderValid()
    {
        _validationBorder.color = _validColor;
    }

    public void SetBorderInvalid()
    {
        _validationBorder.color = _invalidColor;
    }
    #endregion
}
