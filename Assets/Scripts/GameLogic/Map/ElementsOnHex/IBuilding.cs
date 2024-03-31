namespace SoloTrainGame.GameLogic
{
    public interface IBuilding 
    { 
        BuildingTypeSO BuildingType { get; }
        HexGameData HexTile {  get; }
    }
}