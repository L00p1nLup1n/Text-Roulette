namespace Text_Roulette.code.Models.Items
{

    public enum ItemName
    {
        beer,
        glass,
        cig,
        saw,
        cuff,
        NaN
    }

    public struct ItemsInfo(string info)
    {
        public string Info { get; set; } = info;
    }

}

