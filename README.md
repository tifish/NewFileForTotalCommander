# NewFileForTotalCommander

## Features

- New file dialog for Total Commander to create an empty file.
- Can choose encoding and remember for each type of file.

## Installation

- Copy `bin\NewFile.exe` to `%COMMANDER_PATH%\Tools\NewFile`.
- Add to `UserCmd.ini`:

```ini
[em_CreateNew]
cmd=NewFile
menu=New File
param=""%P"
path=%COMMANDER_PATH%\Tools\NewFile
```

- Add to `WinCmd.ini`:

```ini
[Shortcuts]
C+N=em_CreateNew
```

- Now press Ctrl+N in Total Commander to create new file.
