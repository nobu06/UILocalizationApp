[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;    // we'll use this to fill the dictionary (c?)
}


[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}