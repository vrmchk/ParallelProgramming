using BrownianSimulation;

Console.Write("Enter the size of the crystal (N): ");
var n = Convert.ToInt32(Console.ReadLine()!);

Console.Write("Enter the number of particles (K): ");
var k = Convert.ToInt32(Console.ReadLine()!);

Console.Write("Enter the probability of moving left (p) [from 0 to 1]: ");
var p = Convert.ToDouble(Console.ReadLine()!);

Console.WriteLine("Starting Cells (unsynced)...");
ICrystalSimulator cell = new CellsUnsynced(n, k, p);
cell.Run();

Console.WriteLine("\nStarting Cells (synced)...");
ICrystalSimulator cellLocked = new CellsSynced(n, k, p);
cellLocked.Run();

Console.ReadLine();