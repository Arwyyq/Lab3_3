using System;

[Serializable]
public class MyString
{
    public string Value { get; set; }
    public int Length => Value?.Length ?? 0;

    public MyString() { } // Порожній конструктор

    public MyString(string value)
    {
        Value = value;
    }

    public void Print(int index = 0)
    {
        if (index > 0)
        {
            Console.WriteLine($"Рядок [{index}]: {Value}, Довжина: {Length}");
        }
        else
        {
            Console.WriteLine($"Рядок: {Value}, Довжина: {Length}");
        }
    }

    public bool searchSub(string sub) //Метод приймає шуканий текст
    {
        return Value.Contains(sub);
    }

    public void insetSub(int index, string sub)
    {
        Value = Value.Insert(index, sub);
    }

    public void replaceSub(string oldValue, string newValue)
    {
        Value = Value.Replace(oldValue, newValue);
    }
}
