# Project1_CLI

## Overview
Project1_CLI is a command-line interface (CLI) application designed to perform various tasks efficiently. This project serves as a foundation for learning and implementing robust CLI-based solutions.

## Features
- Processes files in directories, excluding certain folders (e.g., `bin`, `debug`).
- Removes empty lines from files if the option is enabled.
- Provides a flexible and customizable solution for handling file-based operations.

## Prerequisites
- .NET SDK installed on your system.
- Git for version control.

## Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd Project1_CLI
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

## Usage
Run the application using:
```bash
dotnet run
```

### Command-line Options
- `--removeEmptyLines`: Cleans up empty lines from files.
- Additional options can be configured in future versions.

## Project Structure
- `Program.cs`: Entry point of the application.
- `.gitignore`: Excludes unnecessary files from version control.
- `bin/` and `obj/`: Generated directories excluded from the repository.
- `publish/`: For release builds, excluded in `.gitignore`.

## Development
### To contribute:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature-branch-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Your descriptive message"
   ```
4. Push to the branch:
   ```bash
   git push origin feature-branch-name
   ```
5. Open a pull request.

### Running Tests
(Currently no tests available. Add unit tests for better stability.)

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

---
Feel free to modify and expand this README as the project evolves!

