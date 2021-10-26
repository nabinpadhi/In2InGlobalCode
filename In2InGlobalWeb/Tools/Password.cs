using System;
using System.Collections;

/// <summary>
/// Represents an instance of the Password class. This class contains
/// all the functions and properties necessary to generate specific
/// random passwords based on various user-specified criteria.
/// 
/// Written by David Farrell (david@davidfarrell.info)
/// Copyright 2005
/// </summary>
public class Password
{
    /// <summary>
    /// Internal private variable representing the number of characters in the password
    /// </summary>		
    private int miPasswordLength = 8;

    /// <summary>
    /// Internal private variable representing whether or not to show the phonetic of each character
    /// </summary>
    private bool mbShowPhonetics = false;

    /// <summary>
    /// Internal private variable representing whether or not to include letters in the password
    /// </summary>
    private bool mbIncludeLetters = true;

    /// <summary>
    /// Internal private variable representing whether or not to include mixed case letters in the password
    /// </summary>
    private bool mbIncludeMixedCase = false;

    /// <summary>
    /// Internal private variable representing whether or not to include numbers in the password
    /// </summary>
    private bool mbIncludeNumbers = true;

    /// <summary>
    /// Internal private variable representing whether or not to include puncutation in the password
    /// </summary>
    private bool mbIncludePunc = false;

    /// <summary>
    /// Internal private variable representing whether or not to prevent the user of similar-looking characters in the password
    /// </summary>
    private bool mbNoSimilarCharacters = false;

    /// <summary>
    /// Internal private variable representing the quantity of passwords to be generated
    /// </summary>
    private int miQuantity = 1;

    /// <summary>
    /// Internal private variable to store the pool of possible characters to use when randomly generating a password
    /// </summary>
    private ArrayList marrCharPool;


    /// <summary>
    /// Gets or sets the length of the password. Default value is 8.
    /// </summary>
    public int PasswordLength
    {
        get
        {
            return miPasswordLength;
        }

        set
        {
            miPasswordLength = value;
        }
    }

    /// <summary>
    /// Gets or sets whether or not to show phonetics for each character. Default value is false.
    /// </summary>
    public bool ShowPhonetics
    {
        get
        {
            return mbShowPhonetics;
        }
        set
        {
            mbShowPhonetics = value;
        }
    }

    /// <summary>
    /// Gets or sets whether or not to include letters in the password. Default value is true.
    /// </summary>
    public bool IncludeLetters
    {
        get
        {
            return mbIncludeLetters;
        }
        set
        {
            mbIncludeLetters = value;
        }
    }

    /// <summary>
    /// Gets or sets whether or not to include mixed case letters in the password. Default value is false.
    /// </summary>
    public bool IncludeMixedCase
    {
        get
        {
            return mbIncludeMixedCase;
        }
        set
        {
            mbIncludeMixedCase = value;
        }
    }

    /// <summary>
    /// Gets or sets whether or not to include numbers in the password. Default value is false.
    /// </summary>
    public bool IncludeNumbers
    {
        get
        {
            return mbIncludeNumbers;
        }
        set
        {
            mbIncludeNumbers = value;
        }
    }

    /// <summary>
    /// Gets or sets whether or not to include punctuation in the password. Default value is false.
    /// </summary>
    public bool IncludePunc
    {
        get
        {
            return mbIncludePunc;
        }
        set
        {
            mbIncludePunc = value;
        }
    }

    /// <summary>
    /// Gets or sets whether or not to include similar-looking characters in the password. Default value is false.
    /// </summary>
    public bool NoSimilarCharacters
    {
        get
        {
            return mbNoSimilarCharacters;
        }
        set
        {
            mbNoSimilarCharacters = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of passwords to be generated. Default value is 1.
    /// </summary>
    public int Quantity
    {
        get
        {
            return miQuantity;
        }
        set
        {
            miQuantity = value;
        }
    }

    /// <summary>
    /// Constructor that initializes a new instance of the Password class
    /// </summary>
    public Password()
    {
        //
        // TODO: Add constructor logic here
        //
        //Initialise the character pool
        marrCharPool = new ArrayList();
    }

    /// <summary>
    /// Public method to generate the random passwords
    /// </summary>
    /// <returns>A string containing the passwords generated. Multiple passwords are delimited by a character return (\r\n)</returns>
    public string Generate()
    {
        try
        {
            string sResult = "";

            //Start the random number generator
            Random rdm = new Random();

            //Determine the character pool i.e. what characters were are allowed to draw from
            BuildCharacterPool();

            //Iterate through the number of passwords required


            for (int x = 0; x < miQuantity; x++)
            {
                //Iterate through the number of characters required in the password
                for (int i = 0; i < miPasswordLength; i++)
                {
                    sResult += marrCharPool[rdm.Next(marrCharPool.Count)].ToString();
                }

                //sResult = sResult;
            }

            return sResult;
        }
        catch
        {
            string sError = "Please select a character set to generate passwords from.";

            return sError;
        }
    }

    /// <summary>
    /// Private method for generating the pool of characters to be used when generating
    /// a password. The pool of characters used is determined by the properties set by the user.
    /// </summary>
    private void BuildCharacterPool()
    {

        marrCharPool.Clear();

        //Add lowercase alphabet if required

        if (mbIncludeLetters == true)
        {
            for (int i = 97; i < 123; i++)
            {
                marrCharPool.Add(Convert.ToChar(i).ToString());
            }
        }

        //Add uppercase alphabet if required
        if (mbIncludeMixedCase == true)
        {
            for (int i = 65; i < 91; i++)
            {
                marrCharPool.Add(Convert.ToChar(i).ToString());
            }
        }

        //add numbers if required
        if (mbIncludeNumbers == true)
        {
            for (int i = 48; i < 58; i++)
            {
                marrCharPool.Add(Convert.ToChar(i).ToString());
            }
        }

        //add punctuation if required
        if (mbIncludePunc == true)
        {
            for (int i = 33; i < 48; i++)
            {
                marrCharPool.Add(Convert.ToChar(i).ToString());
            }

            for (int i = 58; i < 65; i++)
            {
                marrCharPool.Add(Convert.ToChar(i).ToString());
            }
        }

    }
}

