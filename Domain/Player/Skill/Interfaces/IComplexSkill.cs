namespace Models.Player;

public interface IComplexSkill : ISkill
{
    Point Direction { get; set; }
}