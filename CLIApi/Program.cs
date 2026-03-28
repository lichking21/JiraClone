using CLI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main()
    {
        using IHost host = Bootstraper.BuildApp();

        var router = host.Services.GetRequiredService<CLIRouter>();

        Console.WriteLine("Program started. Print [help] for information or [exit].");

        router.StartLoop();
    }
}