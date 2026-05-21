namespace BrownianSimulation;

public class CellsUnsynced(int crystalSize, int particlesNumber, double probability) 
    : CellsBase(crystalSize, particlesNumber, probability), ICrystalSimulator
{
    protected override string CellName => nameof(CellsUnsynced);

    protected override void Simulate(Particle particle)
    {
        var rand = new Random();

        while (Running)
        {
            var current = particle.Position;
            var m = rand.NextDouble();

            var newPos = m > Probability ? current + 1 : current - 1;
            if (newPos < 0 || newPos >= CrystalSize) newPos = current;

            Cells[current]--;
            Cells[newPos]++;
            particle.Position = newPos;

            Thread.Sleep(10);
        }
    }

    protected override void PrintSnapshot(int second) => Console.WriteLine($"[{CellName}] Second {second}: {string.Join(",", Cells)}");
}