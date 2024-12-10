
using System.CommandLine;

var bundleOptionPath = new Option<FileInfo>("--output", "file path and name");
bundleOptionPath.AddAlias("-o");
var inputOptionPath = new Option<DirectoryInfo>("--input", "Directory containing code files to bundle");
inputOptionPath.AddAlias("-i");
var languageOption = new Option<String[]>("--language", "List of programming languages to include in the bundle");
languageOption.AddAlias("-l");
var noteOption = new Option<bool>("--note", "Include the source file path and name as a comment in the bundle")
{
    IsRequired = false // האפשרות לא חובה
};
noteOption.AddAlias("-n");
var sortOption = new Option<string>("--sort", "Sort files by name or extension (default: name)")
{
    IsRequired = false
};
sortOption.AddAlias("-s");
// הגדרת ברירת מחדל של ערך
sortOption.SetDefaultValue("name");
var removeEmptyLinesOption = new Option<bool>("--remove-empty-lines", "Remove empty lines from the code before bundling")
{
    IsRequired = false // האפשרות לא חובה
};
removeEmptyLinesOption.AddAlias("-r");
var authorOption = new Option<string>("--author", "Name of the author to include in the bundle file header")
{
    IsRequired = false // האפשרות לא חובה
};
authorOption.AddAlias("-a");

//פקודה שנייה-create-rsp
var createRspCommand = new Command("create-rsp", "Create a response file with prepared command");
var outputOptionRspCommand = new Option<FileInfo>("--response-file", "The response file to create");
outputOptionRspCommand.AddAlias("-o");
createRspCommand.AddOption(outputOptionRspCommand);
createRspCommand.SetHandler((responseFile) =>
{
    // הצגת שאלות למשתמש
    Console.WriteLine("Please provide values for each option.");

    Console.Write("Enter output file path: ");
    var output = Console.ReadLine();

    if (string.IsNullOrEmpty(output) || !Directory.Exists(Path.GetDirectoryName(output)))
    {
        Console.WriteLine("Error: Invalid output file path.");
        return;
    }

    Console.Write("Enter input directory path: ");
    var input = Console.ReadLine();

    if (string.IsNullOrEmpty(input) || !Directory.Exists(input))
    {
        Console.WriteLine("Error: Input directory does not exist.");
        return;
    }

    Console.Write("Enter programming languages (comma separated, or 'all' for all files): ");
    var languages = Console.ReadLine();
    if (string.IsNullOrEmpty(languages))
    {
        Console.WriteLine("Error: Please provide at least one programming language.");
        return;
    }

    Console.Write("Include source file paths as comments? (true/false): ");
    var note = Console.ReadLine();

    Console.Write("Sort files by name or extension? (name/extension): ");
    var sort = Console.ReadLine();

    Console.Write("Remove empty lines? (true/false): ");
    var removeEmptyLines = Console.ReadLine();

    Console.Write("Enter author name: ");
    var author = Console.ReadLine();
    
    // בונה את הפקודה הסופית עם כל פרמטר בשורה נפרדת
    var command = $"bundle" + Environment.NewLine;
    command += $"--output {output}" + Environment.NewLine;
    command += $"--input {input}" + Environment.NewLine;
    command += $"--language {languages}" + Environment.NewLine;
    command += $"--note {note}" + Environment.NewLine;
    command += $"--sort {sort}" + Environment.NewLine;
    command += $"--remove-empty-lines {removeEmptyLines}" + Environment.NewLine;
    command += $"--author {author}" + Environment.NewLine;

    // שומר את הפקודה בקובץ התגובה
    File.WriteAllText(responseFile.FullName, command);

    // אחרי שמירה בקובץ
    Console.WriteLine($"Response file created at {responseFile.FullName}");
    Console.WriteLine($"Content: {File.ReadAllText(responseFile.FullName)}");
    Console.WriteLine($"Response file created at {responseFile.FullName}");
}, outputOptionRspCommand);


var bundleCommand = new Command("bundle", "bundle code files to a single file");
bundleCommand.AddOption(bundleOptionPath);
bundleCommand.AddOption(inputOptionPath);
bundleCommand.AddOption(languageOption);
bundleCommand.AddOption(noteOption);
bundleCommand.AddOption(sortOption);
bundleCommand.AddOption(removeEmptyLinesOption);
bundleCommand.AddOption(authorOption);

bundleCommand.SetHandler((output, input, languages, note, sort, removeEmptyLines, author) =>
{
    try
    {
        if (!input.Exists)
        {
            Console.WriteLine("Error: Input directory does not exist.");
            return;
        }

        using (var outputFile = File.CreateText(output.FullName))//היוזינג קובע שלאחר מכן הקובץ יסגר 
        {
            if (!string.IsNullOrEmpty(author))
            {
                outputFile.WriteLine($"// Author: {author}");
            }
            var files = input.GetFiles("*", SearchOption.AllDirectories)
                 .Where(file => !file.FullName.Contains("bin") && !file.FullName.Contains("debug"))
                 .ToArray();
            if (sort.Equals("extension", StringComparison.OrdinalIgnoreCase))
            {
                files = files.OrderBy(file => file.Extension).ToArray();
            }
            else
            {
                files = files.OrderBy(file => file.Name).ToArray(); // ברירת מחדל: מיון לפי שם הקובץ
            }
            var languageList = languages.SelectMany(lang => lang.Split(',')).Select(lang => lang.Trim()).ToList();

            foreach (var file in files)
            {
                if (languageList.Contains("all") || languageList.Any(lang => file.Extension.Equals($".{lang}", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Reading file: {file.FullName}");
                    var content = File.ReadAllText(file.FullName);

                    // אם האפשרות מחיקת שורות ריקות פעילה, יש לנקות את השורות הריקות
                    if (removeEmptyLines)
                    {
                        content = string.Join(Environment.NewLine, content
                            .Split(Environment.NewLine)
                            .Where(line => !string.IsNullOrWhiteSpace(line)));
                    }

                    if (note)
                    {
                        outputFile.WriteLine($"// Source file: {file.FullName}");
                    }
                    outputFile.WriteLine($"// --- Start of {file.Name} ---");
                    outputFile.WriteLine(content);
                    outputFile.WriteLine($"// --- End of {file.Name} ---");
                }
            }
            Console.WriteLine($"File {output.FullName} created successfully with bundled content.");
        }
    }
    catch (DirectoryNotFoundException ex)
    {
        Console.WriteLine("eror:the path us not valid");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}, bundleOptionPath, inputOptionPath, languageOption, noteOption, sortOption, removeEmptyLinesOption, authorOption);

var rootCommand = new RootCommand("root command for file bundle CLI") {
bundleCommand,
createRspCommand
};
rootCommand.InvokeAsync(args);

