namespace BrownianSimulation;

public class CellsSynced(int crystalSize, int particlesNumber, double probability) : CellsBase(crystalSize, particlesNumber, probability), ICrystalSimulator
{
    private readonly Lock _lockObj = new();

    protected override string CellName => nameof(CellsSynced);

    protected override void Simulate(Particle particle)
    {
        var rand = new Random();

        while (Running)
        {
            lock (_lockObj)
            {
                var current = particle.Position;
                var moveProbability = rand.NextDouble();

                var newPos = moveProbability > Probability ? current + 1 : current - 1;
                if (newPos < 0 || newPos >= CrystalSize) newPos = current;

                Cells[current]--;
                Cells[newPos]++;
                particle.Position = newPos;
            }

            Thread.Sleep(10);
        }
    }

    protected override void PrintSnapshot(int second)
    {
        lock (_lockObj)
        {
            Console.WriteLine($"[{CellName}] Second {second}: {string.Join(",", Cells)}");
        }
    }
}
