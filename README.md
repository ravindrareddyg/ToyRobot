# ToyRobot Simulator (CLI)

A Toy Robot Simulator built in **C# (.NET 8 – LTS)**, chosen for its stability and wide industry adoption.\
The project demonstrates **clear domain modelling**, **explicit command handling**, **unit testing**, and a **git-style interactive command-line interface**.

---

## Overview

A robot moves on a **5×5 tabletop**.\
The robot accepts the following commands:

- `PLACE X,Y,FACING`
- `MOVE`
- `LEFT`
- `RIGHT`
- `REPORT`

### Rules

- The robot must be **placed** on the table before it can move or report.
- Commands issued before a valid `PLACE` are rejected.
- The robot must **not fall off the table**.
- `REPORT` outputs the robot’s current position and facing direction.
- Output is produced **only** for `REPORT`.

---

## Example

**Input**

```
PLACE 0,0,NORTH
MOVE
REPORT
```

**Output**

```
0,1,NORTH
```

---

## Project Structure

```
ToyRobot/
├── README.md
├── ToyRobot.sln
├── ToyRobot.Cli
│   ├── Commands
│   │   ├── CommandContracts.cs    // Command contracts & outcomes
│   │   ├── CommandParser.cs       // Command parsing
│   │   └── RobotCommandHandler.cs // Command handling logic
│   ├── Enums
│   │   └── Direction.cs           // Direction enum
│   ├── Models
│   │   ├── Position.cs            // Value object
│   │   ├── RobotState.cs          // Robot state & rotation logic
│   │   └── TableTop.cs            // Table boundaries & validation
│   ├── Services                   // Simulator & orchestration
│   ├── Program.cs                 // CLI entry point
│   │   └── ToyRobot.Cli.csproj
├── ToyRobot.Test
│   ├── RobotSimulatorTests.cs
│   ├── TableTopTests.cs
│   └── ToyRobot.Test.csproj

```

---

## Design Principles

- **Domain-focused modelling** — core business rules live in `Models` and command handling logic.
- **Separation by responsibility** — `Commands`, `Models`, and `Services` clearly define boundaries without over-engineering.
- **Explicit control flow** — validation and state transitions are deliberate and easy to debug.
- **Test-driven validation** — business rules are verified using xUnit tests.
- **Disciplined CLI behaviour** — while `REPORT` is the only command that writes to `stdout`.

---

## Downloading the Project

### Option 1: Clone using Git (recommended)

```bash
git clone https://github.com/ravindrareddyg/ToyRobot.git
cd ToyRobot
```

---

### Option 2: Download as ZIP

1. Open the repository on GitHub
2. Click **Code → Download ZIP**
3. Extract the ZIP file

---

## Running the Simulator

### Prerequisites

- .NET SDK 8+
- Windows, macOS, or Linux

### Run from terminal

```bash
dotnet run --project ToyRobot.Cli
```

### Run from Visual Studio Code (Windows, macOS, Linux)

1. Open **Visual Studio Code**
2. Select **File → Open Folder**
3. Open the `ToyRobot` folder
4. Install the **C# Dev Kit / C#** extension when prompted\
   (VS Code will guide you if the .NET SDK is missing)
5. Open the integrated terminal
6. Run:

```bash
dotnet run --project ToyRobot.Cli
```

### Run from Visual Studio

1. Open **Visual Studio 2022** (or later)
2. Select **File → Open → Project/Solution**
3. Navigate to the folder containing `ToyRobot.sln` and open it
4. In Solution Explorer, right-click **ToyRobot** and choose **Set as Startup Project**
5. Press `Ctrl + F5` to run without debugging

### Running with file input (Windows CMD)

```
cmd
type ToyRobot.Cli\input.txt | dotnet run --project ToyRobot.Cli\ToyRobot.Cli.csproj
```
---

## CLI Usage

The simulator runs interactively, piped and file import

---

## Testing

Unit tests cover:

- Tabletop boundary validation
- Invalid tabletop dimensions
- Robot placement rules
- Movement and rotation logic
- Ignore-until-place behaviour
- `REPORT` output correctness

Run all tests:

```bash
dotnet test
```

## Additional Notes
If you encounter errors related to NuGet packages or the .NET SDK during build or test execution, run the following commands inside the project folder where the error occurs.
For example, if the error appears in ToyRobot.Test, navigate to that folder first

```
cd ToyRobot.Test                --change based on folder
dotnet restore
dotnet build
dotnet test
```
---

> This solution intentionally uses a single executable project with clear folder-based separation to keep the design simple and easy to review. The boundaries are explicit and can be split into separate projects if the system grows.

