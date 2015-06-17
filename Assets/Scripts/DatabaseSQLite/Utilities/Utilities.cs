using System;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Xml;
using System.Text.RegularExpressions;


public static class Utilities
{
    public static XmlNode GetNode(this XmlNode node, string tag)
    {
        XmlNode selected = node.SelectSingleNode("descendant::" + tag);
				
        return selected;
    }

    public static XmlNodeList GetNodeList(this XmlNode node, string tag)
    {
        XmlNodeList nodes = node.SelectNodes("descendant::" + tag);
		
        return nodes;
    }

    public static string GetNodeText(this XmlNode node, string tag)
    {
        XmlNode selected = node.GetNode(tag);

        if (selected != null)
            return selected.InnerText;

        return null;
    }
		
    public static float GetFloat(string value)
    {
        value = value.Trim();
        value = value.Replace("%", "");
        return float.Parse(value);
    }
	
    public static bool IsLuckyEnough(float percent)
    {
        float[] a = new float[]{1.0f - percent, percent};
		
        int index = GetIndexFromProbabilities(a);
		
        return (index == 1);
    }
	
    public static int GetIndexFromProbabilities(float[]a)
    {
        int index = 0;
		
        float v = UnityEngine.Random.value;
		
        float[] b = new float[a.Length + 1];
		
        float amount = 0;
		
        for (int i = 0; i < b.Length - 1; i++)
        {
            b [i] = amount;
			
            amount += a [i];
        }
		
        b [b.Length - 1] = 1.0f;
		
        for (int i = 0; i < b.Length - 1; i++)
        {
            if (v >= b [i] && v < b [i + 1])
            {
                index = i;
                break;
            }
        }
		
        return index;
    }

    /*
        Less than zero: t1 is earlier than t2. <
        Zero: t1 is the same as t2. =
        Greater than zero: t1 is later than t2. >
     */
    public static int CompateDateTime(string t1, string t2)
    {
        System.DateTime d1 = Utilities.ParseDateTime(t1);
        System.DateTime d2 = Utilities.ParseDateTime(t2);
        
        return System.DateTime.Compare(d1, d2);
    }
	
    public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    public static DateTime ParseDateTime(string date_time, string format="yyyy-MM-dd HH:mm:ss")
    {
        CultureInfo provider = CultureInfo.InvariantCulture;
			
        DateTime result = new DateTime();
			
        try
        {
            if (date_time.Length > format.Length)
            {
                date_time = date_time.Substring(0, format.Length);
            }
            result = DateTime.ParseExact(date_time, format, provider);
        } catch (FormatException)
        {
            Debug.Log(date_time + " is not in the correct format.");

        }
        return result;	
    }
	
    public static TimeSpan SubDateTime(string date_time_current, string date_time_before, string format="yyyy-MM-dd HH:mm:ss")
    {
        DateTime time_before = ParseDateTime(date_time_before, format);
        DateTime time_current = ParseDateTime(date_time_current, format);
        TimeSpan result = time_current.Subtract(time_before);
        return result;	
    }

    public static bool CheckEqualDateTime(string date_time_current, string date_time_before, string format="yyyy-MM-dd")
    {
        DateTime time_before = ParseDateTime(date_time_before, format);
        DateTime time_current = ParseDateTime(date_time_current, format);
        if (!DateTime.Equals(time_before, time_current))
        {
            return false;
        }
        return true;	
    }

    const string emoticons = ",./<>?;':\"[]{}-=_+`~!@#$%^&*()\\";

    public static bool CheckTextContaintEmoticon(this string text)
    {
        for (int i = 0; i < emoticons.Length; i++)
        {
            char c = emoticons [i];

            int index = text.IndexOf(c);

            if (index >= 0 && index < text.Length)
                return true;
        }

        return false;
    }

    public static bool CheckTextSimilar(this string text)
    {
        string lower = text.ToLower();

        char first = lower [0];

        for (int i = 1; i < lower.Length; i++)
        {
            if (first != lower [i])
                return false;
        }

        return true;
    }

    public static bool CheckTextIsAlphaNumeric(this string text)
    {
        string lower = text.ToLower();

        for (int i = 0; i < text.Length; i++)
        {
            if ((lower [i] >= 'a' && lower [i] <= 'z') || (lower [i] >= '0' && lower [i] <= '9'))
                continue;

            return false;
        }

        return true;
    }

    public static string floatToPercent(float value)
    {
        return	string.Format("{0}%", value.ToString("F1"));
    }

    public static string adjustPercent(string currentPercent_str, float value)
    {

        currentPercent_str = currentPercent_str.Substring(0, currentPercent_str.IndexOf('%'));		
        float currentPercent = float.Parse(currentPercent_str);
        currentPercent += value;

        return string.Format("{0} %", currentPercent.ToString("F1"));
    }

    public static string cutExtension(string str)
    {
//		if(str.Contains(".")){
//			str = str.Substring(0,str.LastIndexOf('.'));
//		}

        return str;
    }

    public static string getPersitentPath()
    {

        string result = string.Format("file://{0}", Application.persistentDataPath);

        return result;
    }
}


