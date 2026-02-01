namespace Domain.Game.Common;

public class Battlefield
{
    public PlayerBattlefieldArea TopPlayer { get; set; } = new();
    public PlayerBattlefieldArea BottomPlayer { get; set; } = new();
    public Queue<BattlefieldAction> Actions { get; set; } = [];
}
