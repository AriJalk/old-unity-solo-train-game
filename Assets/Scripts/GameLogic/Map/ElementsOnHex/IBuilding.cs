namespace SoloTrainGame.GameLogic
{
    public interface IBuilding 
    { 
        BuildingTypeSO BuildingType { get; }
        HexData HexTile {  get; }
    }
}