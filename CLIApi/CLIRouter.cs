using Microsoft.Extensions.DependencyInjection;
using Domain;
using Application;

namespace CLI;

public class CLIRouter
{
    private readonly IServiceProvider _serviceProvider;

    public CLIRouter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider; 
    }

    public void StartLoop()
    {
        while (true)
        {
            Console.Write("jira> ");
            var input = Console.ReadLine()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            if (input == null || input.Length == 0) continue;

            var command = input[0].ToLower();
            var args = input.Skip(1).ToArray();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var provider = scope.ServiceProvider;

                switch (command)
                {
                    case "add-user":
                        // Example: jira> add-user Durak
                        var userName = string.Join(" ", args);
                        var userRepo = provider.GetRequiredService<IUserRepository>();
                        
                        var newUser = new User(userName); 
                        
                        userRepo.Add(newUser);
                        Console.WriteLine($"(OK) User '{userName}' added.");
                        break;

                    case "add-task":
                        // Example: jira> add-task "Vysmorkat'sya"
                        var title = string.Join(" ", args);
                        var taskRepo = provider.GetRequiredService<ITaskRepository>();
                        var newTask = new UserTask(title);
                        taskRepo.Add(newTask);
                        Console.WriteLine($"(OK) Task [{title}] created.");
                        break;

                    case "assign-task":
                        // Example: jira> assign-task 1 42 (taskId=1, userId=42)
                        var taskId = int.Parse(args[0]);
                        var userId = int.Parse(args[1]);
                        var assignUseCase = provider.GetRequiredService<AssignTaskToUserUseCase>();
                        assignUseCase.Execute(userId, taskId);
                        Console.WriteLine($"(OK) Task [{taskId}] assigned to user [{userId}].");
                        break;

                    case "exit":
                        Console.WriteLine("Shutting down...");
                        return;

                    case "help":
                        Console.WriteLine("Commands: add-task <title>, assign-task <taskId> <userId>, exit");
                        break;

                    default:
                        Console.WriteLine($"(ERR) Undefined command '{command}'.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"(ERR) >> {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}