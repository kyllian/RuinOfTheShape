namespace RuinOfTheShape.Simulation;

public interface ISimulationTickSystem
{
    void Initialize();
    void Tick(int ticks = 1);
}

