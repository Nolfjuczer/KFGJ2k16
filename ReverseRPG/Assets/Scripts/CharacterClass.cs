using UnityEngine;
using System.Collections.Generic;

public class CharacterClass : MonoBehaviour
{
    public EClasses MyClass;

    public int HP;
    public int MP;

    public int Stamina;
    public int Strenght;
    public int Dextirity;
    public int Inteligence;

    public List<Spell> Spells;
    public List<Weapon> Weapons;
    public List<Armor> Armors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public struct Weapon
{
    public string Name;
    public int Attack;
}

public struct Armor
{
    public string Name;
    public int Defense;
}

public struct Spell
{
    public string Name;
    public int DMG;
    public int Cost;
}

public enum EClasses
{
    Mage,
    Knight
}