# IO Utilities

The **IO Utilities** module provides helper tools aimed at simplifying file and directory operations within Unity projects. These utilities are designed for streamlining workflows, such as directory creation, path management, and sanitizing file names.

---

## Classes

### 1. `FileUtilities`

#### Overview
The **`FileUtilities`** class is a static utility class providing essential methods for working with directories and files. While functional, the code may need refactoring and additional testing to handle edge cases and improve robustness.

#### Key Features
- Automatically ensure directories exist.
- Construct full directory paths with or without a trailing slash.
- Sanitize file names by replacing invalid characters.

#### Disclaimer
This class has not been extensively tested and is still in early development. While it performs basic file-system operations, care should be taken with edge cases such as invalid paths, access permissions, or platform-specific restrictions.

---

### `FileUtilities`

#### Methods

##### 1. `EnsureDirectoryExists/EnsureDirectory`
```c#
public static void EnsureDirectoryExists(string path, bool escapeSpecialCharacters = false)
public static void EnsureDirectory(this string path, bool escapeSpecialCharacters = false)
```

- **Description**:
  Ensures that a specified directory exists by creating it if it does not. Particularly useful for file-saving workflows where intermediate directories may not yet exist.

- **Parameters**:
	- `string path`: The full path of the directory to ensure.
    - `escapeSpecialCharacters` : Specifies whether to escape special characters in the path before ensuring the directory's existence.

- **Details**:
	- If the directory already exists, no action is taken.
	- If the directory is created, Unity’s `AssetDatabase` is refreshed immediately to make sure the Unity Editor recognizes the changes (only relevant in the Editor).

- **Usage**:
  ```c#
  FileUtilities.EnsureDirectoryExists("Assets/Textures/NewFolder");
  // Logs: "Directory created at path: Assets/Textures/NewFolder"
  var anotherPath = "Assets/Textures/NewFolder2";
  anotherPath.EnsureDirectory();
  ```

- **Best Practices**:
	- Use this method when dynamically generating files or folders during runtime or editor scripting.
	- Be cautious when using on absolute paths outside Unity's project directory. `AssetDatabase.Refresh` only affects Unity-specific paths.

---

##### 2. `GetDirectoryPath`
```c#
public static string GetDirectoryPath(string rootPath, string directoryName)
```

- **Description**:
  Combines a root directory path and a directory name into a full directory path **without** a trailing slash.

- **Parameters**:
	- `string rootPath`: The base directory path.
	- `string directoryName`: The name of the directory to append to the root.

- **Returns**:
	- `string`: The combined directory path.

- **Details**:
	- Uses C#'s `Path.Combine` to form the full path, ensuring proper handling of slashes for different operating systems.

- **Usage**:
  ```c#
  string path = FileUtilities.GetDirectoryPath("Assets/Textures", "MyImages");
  Debug.Log(path); // Output: "Assets/Textures/MyImages"
  ```

---

##### 3. `GetDirectoryRoot`
```c#
public static string GetDirectoryRoot(string rootPath, string directoryName)
```

- **Description**:
  Combines a root directory path and a directory name into a full directory path **with** a trailing slash.

- **Parameters**:
	- `string rootPath`: The base directory path.
	- `string directoryName`: The name of the directory to append to the root.

- **Returns**:
	- `string`: The combined directory path, including the trailing slash.

- **Details**:
	- This method is similar to `GetDirectoryPath`, but explicitly includes a trailing slash on the resulting path to ensure consistency.

- **Usage**:
  ```c#
  string path = FileUtilities.GetDirectoryRoot("Assets/Textures", "MyImages");
  Debug.Log(path); // Output: "Assets/Textures/MyImages/"
  ```

---

##### 4. `SanitizeFileName`
```c#
public static string SanitizeFileName(string fileName)
```

- **Description**:
  Cleanses invalid or problematic characters from a file name, replacing them with underscores (`_`). Ensures that resulting file names comply with common file-system naming conventions.

- **Parameters**:
	- `string fileName`: The original file name to sanitize.

- **Returns**:
	- `string`: A sanitized file name with invalid characters replaced by underscores.

- **Details**:
	- Uses a regular expression to replace all non-alphanumeric characters, underscores, or hyphens with an underscore.

- **Usage**:
  ```c#
  string safeFileName = FileUtilities.SanitizeFileName("Invalid/File\\Name:?.png");
  Debug.Log(safeFileName); // Output: "Invalid_File_Name__.png"
  ```

- **Best Practices**:
	- Use this method when programmatically generating file names to prevent errors or conflicts caused by invalid characters.
	- Extend or adjust the regex pattern to handle project-specific character requirements where necessary.

---

### Best Practices for `FileUtilities`

1. **Robust Path Handling**:
	- Consider edge cases like empty strings, invalid characters in paths, or invalid drive letters when using methods like `EnsureDirectoryExists`.

2. **Consistency in Paths**:
	- The distinction between `GetDirectoryPath` (without trailing slash) and `GetDirectoryRoot` (with trailing slash) is potentially confusing. Evaluate the need for simplification or enhanced clarity in method names.

3. **Cross-Platform Compatibility**:
	- Ensure that these methods work seamlessly for both Windows and Unix-based systems (including macOS), especially for file-system paths with differing slash styles.

4. **Editor-Only Behavior**:
	- Methods like `EnsureDirectoryExists` rely on `AssetDatabase.Refresh`, which is only relevant in UnityEditor. This behavior might require adjustment or conditional compilation for runtime environments.

---

### Expansion Roadmap

1. **Path Normalization Helpers**:
	- Introduce methods to normalize paths, ensuring consistent use of slashes (`/` vs `\`) across different operating systems.

2. **Error Handling**:
	- Enhance error handling in `EnsureDirectoryExists` to gracefully deal with permission issues, invalid paths, or other exceptions (e.g., log warnings or use a try-catch block).

3. **Validation Utilities**:
	- Add methods to validate paths (e.g., checking for invalid characters, ensuring valid root paths).

4. **Namespace Alignment**:
	- Consider aligning method names and structures with Unity's built-in APIs for better developer familiarity (e.g., methods like `PathUtility.Combine` could mimic Unity-style naming).

5. **Advanced Use Cases**:
	- Add functionality for file/folder enumeration, filtering by extensions, recursive operations, etc.

---

## Namespace
All related utilities are available under:
```c#
PsigenVision.Utilities.IO
```
---