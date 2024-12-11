# CLI File Bundler

## Overview
This project is a Command-Line Interface (CLI) tool designed to bundle multiple code files from a directory into a single file. The tool supports filtering by programming language, sorting by file name or extension, and includes options for adding comments with file paths, removing empty lines, and specifying the author of the bundled file.


This project is a Command-Line Interface (CLI) tool designed to bundle multiple code files from a directory into a single file. 
## Usage

Once the project is built, you can use the following command to bundle code files:



### Command:

`fib bundle --output output.txt --input ./input --language cs,txt --note --sort extension --remove-empty-lines --author "Your Name"`

### Options:
- `--output` or `-o` (Required): The path and name of the output file where the bundled code will be saved.
- `--input` or `-i` (Required): The directory containing the code files to bundle.
- `--language` or `-l`(Required): Comma-separated list of programming languages to include in the bundle (e.g., `cs,txt`), or `all` for all file types.
- `--note` or `-n`: Include source file path and name as a comment in the bundled file.
- `--sort` or `-s`: Sort files by name or extension (default is `name`).
- `--remove-empty-lines` or `-r`: Remove empty lines from the code before bundling.
- `--author` or `-a`: Add an author name in the header of the bundled file.

## Example Folder Structure

Imagine the following folder structure for your project:


project/<br>
│<br>
├── input/<br>
│   ├── file1.cs<br>
│   ├── file2.txt<br>
│   └── file3.cs<br>
│<br>
├── output/<br>
│   └── output.txt  (This is where the bundled content will go)<br>
│<br>
└── <br>

### Content of the `output.txt` File
// Author: Your Name<br>
// --- Start of file1.cs ---<br>
<content of file1.cs><br>
...<br>
// --- End of file1.cs ---<br>
<br>
// --- Start of file2.txt ---<br>
<content of file2.txt><br>
...<br>
// --- End of file2.txt ---<br>



