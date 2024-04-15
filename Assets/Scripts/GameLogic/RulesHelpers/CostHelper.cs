namespace SoloTrainGame.GameLogic
{
    public static class CostHelper
    {
        public static int CalculateBuildCost(BuildingTypeSO buildType, HexGameData hexData, int modifier = 0)
        {
            int cost = hexData.TileType.TerrainCost + buildType.Cost;
            return cost;
        }
    }
}