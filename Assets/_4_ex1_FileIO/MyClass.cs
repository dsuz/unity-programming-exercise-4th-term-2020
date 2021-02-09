using System;

[Serializable]  // ← 必要
public class MyClass
{
    public int level;   // public または SerializeField 属性のフィールドしかシリアライズされないことに注意
    public string name;
    public int hp;

    public MyClass(int level, string name, int hp)
    {
        this.level = level;
        this.name = name;
        this.hp = hp;
    }

    public new string ToString()
    {
        string result = $"Level: {level}, Name: {name}, HP: {hp}";
        return result;
    }
}
