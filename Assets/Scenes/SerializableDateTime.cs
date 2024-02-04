using System;
using UnityEngine;

[Serializable]
public struct SerializableDateTime : ISerializationCallbackReceiver
{
    [SerializeField] private int year;
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int hour;
    [SerializeField] private int minute;
    [SerializeField] private int second;

    private DateTime value;

    public SerializableDateTime(int year, int month, int day, int hour, int minute, int second)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
        this.second = second;

        value = new DateTime();
    }

    public void OnBeforeSerialize()
    {
        year = value.Year;
        month = value.Month;
        day = value.Day;
        hour = value.Hour;
        minute = value.Minute;
        second = value.Second;
    }

    public void OnAfterDeserialize()
    {
        year = Mathf.Clamp(year, 1, int.MaxValue);
        month = Mathf.Clamp(month, 1, 12);
        day = Mathf.Clamp(day, 1, 31);

        hour = Mathf.Clamp(hour, 0, 23);
        minute = Mathf.Clamp(minute, 0, 59);
        second = Mathf.Clamp(second, 0, 59);

        // тут, небось, можно бы добавить проверки на количество дней в месяце

        value = new DateTime(year, month, day, hour, minute, second);
    }

    /// <summary>
    /// Чтобы не нужно было писать dateTime.value. И для неявного преобразования типа
    /// </summary>
    public static implicit operator DateTime(SerializableDateTime serializableDateTime)
    {
        return new DateTime(serializableDateTime.year,
            serializableDateTime.month,
            serializableDateTime.day,
            serializableDateTime.hour,
            serializableDateTime.minute,
            serializableDateTime.second);
    }

    public static implicit operator SerializableDateTime(DateTime dateTime)
    {
        return new SerializableDateTime(dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second);
    }
}
