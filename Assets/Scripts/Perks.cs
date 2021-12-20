
    public enum PerkType
    {
        ChildAtHeart,
        Cannibal,
        BloodyMess,
        AnimalFriend,
        ComputerWhiz
    }
    

// change this to a record struct when C# 9 support???
    public class Perk
    {
        public Perk(PerkType type, bool unlocked)
        {
            Type = type;
            Unlocked = unlocked;
        }

        public PerkType Type { get; set; }

        public bool Unlocked { get; set; }

        public static Perk ParsePerk(string input)
        {
            string[] split = input.Split('-');
            bool isUnlocked = split[1] == "true";
            return split[0] switch
            {
                "ChildAtHeart" => new Perk(PerkType.ChildAtHeart, isUnlocked),
                "Cannibal" => new Perk(PerkType.Cannibal, isUnlocked),
                "BloodyMess" => new Perk(PerkType.BloodyMess, isUnlocked),
                "AnimalFriend" => new Perk(PerkType.AnimalFriend, isUnlocked),
                "ComputerWhiz" => new Perk(PerkType.ComputerWhiz, isUnlocked),
                _ => null
            };
        }
    }