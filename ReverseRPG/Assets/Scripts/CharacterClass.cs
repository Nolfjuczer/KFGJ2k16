using System;
using UnityEngine;
using System.Collections.Generic;
[Serializable]
public class CharacterClass : CharacterStats
{
    public EClasses MyClass;

    public ELevels Level;
    public ELevels StageStartLevel;
    [SerializeField]
    public List<Spell> Spells = new List<Spell>();
    [SerializeField]
    public List<Weapon> Weapons = new List<Weapon>();
    [SerializeField]
    public List<Armor> Armors = new List<Armor>();
}
[Serializable]
public class Weapon
{
    public string Name;
    public int Attack;
}
[Serializable]
public class Armor
{
    public string Name;
    public int Defense;
}
[Serializable]
public class Spell
{
    public string Name;
    public int Attack;
    public int Cost;
    [SerializeField]
    public string ParentSpellName;
    [SerializeField]
    public List<string> ChildSpellNames;
}

public enum EClasses
{
    Mage,
    Knight
}

public enum ELevels
{
    Level1 = 1000,
    Level2 = 3000,
    Level3 = 7000,
    Level4 = 15000,
    Level5 = 25000,
    Level6 = 40000,
}