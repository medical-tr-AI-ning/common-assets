using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "RegexValidation/Pattern")]
public class RegexValidationPattern : ScriptableObject
{
    [SerializeField]
    private string _regexValidationExpression = "(.*?)";
    public Regex regexExpr { get { return new Regex(_regexValidationExpression); } }    
}
