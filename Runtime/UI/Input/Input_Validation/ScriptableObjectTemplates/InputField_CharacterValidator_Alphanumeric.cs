using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
    /// <summary>
    /// Custom Character Input Validator to only allow valid characters
    /// Allows only alphanumeric characters A-Z and 0-9
    /// Replaces Umlauts and other special characters based on charMapping
    /// </summary>
    [CreateAssetMenu(menuName = "TMP_InputValidator/InputField_CharacterValidator_Alphanumeric")]
    [Serializable]
    public class InputField_CharacterValidator_Alphanumeric : TMP_InputValidator
    {               
        private Dictionary<char, char> _charMapping = new Dictionary<char, char>()
        {
            { 'ä', 'a' },
            { 'ö', 'o' },
            { 'ü', 'u' },
            { 'Ä', 'A' },
            { 'Ö', 'O' },
            { 'Ü', 'U' },
            { 'ß', 's' }
        };
        
        public override char Validate(ref string text, ref int pos, char ch)
        {           
            char processedChar = processChar(ch);

            if (isAsciiLetter(processedChar))
            {
                text = text.Insert(pos, Char.ToUpper(processedChar).ToString());
                pos += 1;
                return ch;
            }
            if (Char.IsDigit(ch))
            {
                text = text.Insert(pos, ch.ToString());
                pos += 1;
                return ch;
            }
            return '\0';
        }

        // helper functions
        private bool isAsciiLetter(char aChar)
        {
            return (aChar >= 'A' && aChar <= 'Z') || (aChar >= 'a' && aChar <= 'z');
        }

        private char processChar(char aChar)
        {
            if(_charMapping.TryGetValue(aChar, out char rch))
            {
                return rch;
            }
            return aChar;            
        }
    }
}