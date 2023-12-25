using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;

public static class MiscExtensions
{
	static object lockObject = new object();

	private const string _format = "{0:0.##}";
	public static double Percent(this double baseValue, float percentile)
	{
		return baseValue * (1f + (percentile / 100f)) - baseValue;
	}
	public static double Percent(this int baseValue, float percentile)
	{
		return baseValue * (1f + (percentile / 100f)) - baseValue;
	}

	public static string GetSha1(this object obj)
	{
		BinaryFormatter bf = new BinaryFormatter();
		using var sha1 = SHA1.Create();
		using var ms = new MemoryStream();
		bf.Serialize(ms, obj);
		return Convert.ToBase64String(sha1.ComputeHash(ms.ToArray()));
	}
	public static void ClearChildren(this Transform transform)
	{
		lock (lockObject)
		{
			var children = new List<GameObject>();
			foreach (Transform child in transform)
			{
				children.Add(child?.gameObject);
			}
			children.ForEach(child => GameObject.Destroy(child));
		}
	}
	public static int NumberAfterUnderscore(this string text)
	{
		return int.Parse(text[(text.LastIndexOf('_') + 1)..]);
	}
	public static string IncrementNumberAfterUndescore(this string text)
	{
		var newNumber = text.NumberAfterUnderscore() + 1;
		text = text.Remove(text.LastIndexOf('_') + 1);
		return text + newNumber;
	}
	public static string SetNumberAfterUnderscore(this string text, int number)
	{
		text = text.Remove(text.LastIndexOf('_') + 1);
		return text + number;
	}
	public static bool Chance(this int chance)
	{
		return UnityEngine.Random.Range(0f, chance) <= 1f;
	}
	public static float RangeAverage(this Vector2 range)
	{
		return (range.x + range.y) / 2;
	}

	public static T PickRandomWeighted<T>(this IEnumerable<T> list, Func<T, float> Weight, int? seed = null)
	{
		var sumOfWeights = list.Sum(Weight);
		System.Random random;
		if (seed == null) random = new System.Random();
		else random = new System.Random((int)seed);
		var roll = random.NextDouble() * sumOfWeights;

		var target = 0f;
		foreach (var item in list)
		{
			target += Weight(item);
			if (target >= roll) return item;
		}
		return list.First();
	}
	public static T PickRandom<T>(this IEnumerable<T> list)
	{
		return list.ElementAt(UnityEngine.Random.Range(0, list.Count()));
	}

	public static double ScaleMultiplic(this double baseValue, int level, float multiplier)
	{
		var ret = baseValue;
		for (int i = 0; i < level - 1; i++)
		{
			ret *= multiplier;
		}
		return ret;
	}
	public static double ScaleAdditive(this double baseValue, int level, float addition)
	{
		if (level <= 0) return baseValue;
		return baseValue + addition * (level - 1);
	}
	public static string ToStringAppealing(this double d, bool isCompact = true)
	{
		if (d >= 1000 && isCompact)
			return ToShortNumberString(d);
		if (d == 0) return "0";
		if (d < 1)
		{
			return d.ToString("#.######");
		}
		return Math.Ceiling(d).ToString();
	}

	private static string ToScientificString(this double d)
	{
		return Math.Ceiling(d).ToString("#.##E0");
	}
	public static string ToShortNumberString(this double bignumber)
	{
		if (bignumber >= 1e27)
		{
			return "LOT";
		}
		if (bignumber >= 1e24)
		{
			return string.Format(_format, bignumber / 1e24) + "Y";
		}
		if (bignumber >= 1e21)
		{
			return string.Format(_format, bignumber / 1e21) + "Z";
		}
		if (bignumber >= 1e18)
		{
			return string.Format(_format, bignumber / 1e18) + "E";
		}
		if (bignumber >= 1e15)
		{
			return string.Format(_format, bignumber / 1e15) + "P";
		}
		if (bignumber >= 1e12)
		{
			return string.Format(_format, bignumber / 1e12) + "T";
		}
		if (bignumber >= 1e9)
		{
			return string.Format(_format, bignumber / 1e9) + "G";
		}
		if (bignumber >= 1e6)
		{
			return string.Format(_format, bignumber / 1e6) + "M";
		}
		if (bignumber >= 1e3)
		{
			return string.Format(_format, bignumber / 1e3) + "k";
		}
		return string.Format("{0:0}", bignumber);
	}

	public static string ToCompactTime(this TimeSpan timeSpan)
	{
		if (timeSpan.TotalSeconds < 60)
		{
			return $"{(int)timeSpan.TotalSeconds}s";
		}
		if (timeSpan.TotalMinutes < 60)
		{
			return $"{(int)timeSpan.TotalMinutes + 1}m";
		}
		if (timeSpan.TotalHours < 24)
		{
			return $"{(int)timeSpan.TotalHours + 1}h";
		}
		return $"{(int)timeSpan.TotalDays + 1}d";
	}
	public static string ToRoman(this int number)
	{
		if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
		if (number < 1) return string.Empty;
		if (number >= 1000) return "M" + ToRoman(number - 1000);
		if (number >= 900) return "CM" + ToRoman(number - 900);
		if (number >= 500) return "D" + ToRoman(number - 500);
		if (number >= 400) return "CD" + ToRoman(number - 400);
		if (number >= 100) return "C" + ToRoman(number - 100);
		if (number >= 90) return "XC" + ToRoman(number - 90);
		if (number >= 50) return "L" + ToRoman(number - 50);
		if (number >= 40) return "XL" + ToRoman(number - 40);
		if (number >= 10) return "X" + ToRoman(number - 10);
		if (number >= 9) return "IX" + ToRoman(number - 9);
		if (number >= 5) return "V" + ToRoman(number - 5);
		if (number >= 4) return "IV" + ToRoman(number - 4);
		if (number >= 1) return "I" + ToRoman(number - 1);
		throw new ArgumentOutOfRangeException("something bad happened");
	}
	public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultIfNotFound = default)
	{
		if (dictionary == null || !dictionary.ContainsKey(key)) return defaultIfNotFound;
		return dictionary[key];
	}
	public static Canvas GetCanvas(this GameObject g)
	{
		if (g.GetComponent<Canvas>() != null)
		{
			return g.GetComponent<Canvas>();
		}
		else
		{
			if (g.transform.parent != null)
			{
				return GetCanvas(g.transform.parent.gameObject);
			}
			else
			{
				return null;
			}
		}
	}

}
