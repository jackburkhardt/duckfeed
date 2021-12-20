public enum SkillType
    {
        Persuasion,
        Guns,
        Lockpick,
        Sneak,
        Medical
    }

// change this to a record struct when C# 9 support???
public class Skill
{
    public Skill(SkillType type, int level)
    {
        Type = type;
        Level = level;
    }

    public SkillType Type { get; set; }

    public int Level { get; set; }

    public static Skill ParseSkill(string input)
    {
        string[] split = input.Split('-');
        int level = int.Parse(split[1]);
        return split[0] switch
        {
            "Persuasion" => new Skill(SkillType.Persuasion, level),
            "Guns" => new Skill(SkillType.Guns, level),
            "Lockpick" => new Skill(SkillType.Lockpick, level),
            "Sneak" => new Skill(SkillType.Sneak, level),
            "Medical" => new Skill(SkillType.Medical, level),
            _ => null
        };    
    }
}