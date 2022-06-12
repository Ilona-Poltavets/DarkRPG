using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;

[XmlRoot("Characteristics")]
public class Characteristics
{
	[XmlElement("maxHealth")]
	public int maxHealth { get; set; }
	//[XmlElement("currentHealth")]
	//public int currentHealth { get; set; }
	[XmlElement("lvl")]
	public int lvl { get; set; }
	[XmlElement("exp")]
	public int exp { get; set; }
	[XmlElement("defense")]
	public int defense { get; set; }
	[XmlElement("damage")]
	public int damage { get; set; }
	[XmlElement("gold")]
	public int gold { get; set; }
	public Characteristics()
	{
		this.maxHealth = 100;
		this.exp = 10;
		this.lvl = 1;
		this.defense = 10;
		this.damage = 10;
		this.gold = 1000;
	}
	public Characteristics(int maxHealth, int lvl, int exp, int defense, int damage, int gold)
    {
		this.maxHealth = maxHealth;
		//this.currentHealth = currentHealth;
		this.lvl = lvl;
		this.exp = exp;
		this.defense = defense;
		this.damage = damage;
		this.gold = gold;
	}
}
