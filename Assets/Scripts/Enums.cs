public static class Enums
{
    public enum GameColor
    {
        Brown,
        Gray,
        Red,
        Blue,
        Transperant,
        Turquoise,
        Coral,
        SlateGray,
    }

    public enum TerrainType
    {
        Fields,
        Urban,
        Mountains,
        River,
        Empty,
    }

    public enum GoodsType
    {
        Lumber,
        Steel,
        Minerals,
        None,
    }

    public enum BuildingType
    {
        None = 0,
        Track = 1,
        Town = 2,
        City = 3,
    }

    public enum CardType
    {
        Build,
        Transport,
        Special,
    }
}