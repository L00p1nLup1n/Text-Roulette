namespace Text_Roulette.code.Models
{
    public class Items
    {
        public enum itemName
        {
            beer,
            glass,
            cig,
            saw,
            handcuff,
        }


        public string itemEffect(string itemUsed)
        {
            switch (itemUsed)
            {
                case "beer":
                    return "shotgun";

            }
            return "no";
        }


    }
}