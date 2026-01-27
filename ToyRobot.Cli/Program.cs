using Microsoft.Extensions.DependencyInjection;
using ToyRobot.Commands;
using ToyRobot.Models;
using ToyRobot.Services;

var services = new ServiceCollection();

// config
services.AddSingleton(new Tabletop(5, 5));

// app services
services.AddSingleton<ICommandParser, CommandParser>();
services.AddSingleton<IRobotCommandHandler, RobotCommandHandler>();
services.AddSingleton<IRobotSimulator, RobotSimulator>();
services.AddSingleton<ICommandProcessor, CommandProcessor>();

var provider = services.BuildServiceProvider();
provider.GetRequiredService<ICommandProcessor>().Run();