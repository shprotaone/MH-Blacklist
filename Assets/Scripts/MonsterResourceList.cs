using System.Collections.Generic;

public class MonsterResourceList
{
    private string _key;
    private Lang _lang;
    private RankType _rankType;
    private List<string> _resources = new ();

    public string Key => _key;
    public Lang Lang => _lang;
    
    public List<string> Resources => _resources;
    public RankType RankType => _rankType;

    public MonsterResourceList(string key, RankType rankType, Lang lang, string[] resources)
    {
        _key = key;
        _rankType = rankType;
        _lang = lang;
        _resources.AddRange(resources);
    }
}