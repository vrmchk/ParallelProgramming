namespace BrownianSimulation;

public abstract class CellsBase
{
    protected readonly int[] Cells;
    protected readonly Particle[] Particles;
    protected readonly int CrystalSize;
    protected readonly int ParticlesNumber;
    protected readonly double Probability;
    protected readonly Thread[] Threads;
    protected bool Running = true;
    
    protected CellsBase(int crystalSize, int particlesNumber, double probability)
    {
        CrystalSize = crystalSize;
        ParticlesNumber = particlesNumber;
        Probability = probability;
        Cells = new int[crystalSize];
        Particles = new Particle[particlesNumber];
        Threads = new Thread[particlesNumber];

        for (var i = 0; i < particlesNumber; i++)
        {
            Particles[i] = new Particle(0);
            Cells[0]++;
        }
    }

    protected abstract string CellName { get; }

    public void Run()
    {
        for (var i = 0; i < ParticlesNumber; i++)
        {
            var index = i;
            Threads[i] = new Thread(() => Simulate(Particles[index]));
            Threads[i].Start();
        }

        for (var t = 0; t < 10; t++)
        {
            Thread.Sleep(1000);
            PrintSnapshot(t + 1);
        }

        Running = false;

        foreach (var thread in Threads)
            thread.Join();

        var total = Cells.Sum();
        Console.WriteLine($"[{CellName}] Total atoms after simulation: {total}");
    }

    protected abstract void Simulate(Particle particle);

    protected abstract void PrintSnapshot(int second);
}