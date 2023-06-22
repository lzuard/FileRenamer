namespace RenamerApp;

public static class Renamer
{
    /// <summary>
    /// Get directory path from user
    /// </summary>
    private static string? GetDirectory()
    {
        Console.WriteLine("Enter the full path to the directory with files to rename:");
        var filesPath = Console.ReadLine();

        if (Directory.Exists(filesPath)) return filesPath;
        Console.WriteLine("Specified path is not a directory or it doesn't exists");
        return null;
    }

    /// <summary>
    /// Get path to the txt file with names from user
    /// </summary>
    private static string? GetTextFile()
    {
        Console.WriteLine("\nEnter the full path to the text file with new names:");
        var txtPath = Console.ReadLine();

        if (File.Exists(txtPath)) return txtPath;
        Console.WriteLine("Specified file doesn't exists");
        return null;

    }

    /// <summary>
    /// Perform renaming
    /// </summary>
    private static void Rename(string[] files, string[] names)
    {
        try
        {
            for (int i = 0; i < Math.Min(files.Length, names.Length); i++)
            {
                File.Move(files[i], Path.GetDirectoryName(files[i])+"/"+names[i]);
            }
            Console.WriteLine("Renaming has been done successful");
        }
        catch (Exception e)
        {
            Console.WriteLine("The error has occured: "+e.Message);
        }
    }
    
    /// <summary>
    /// Start renaming
    /// </summary>
    public static void Start()
    {
        //Get directory with files
        var filesPath = GetDirectory();
        if (filesPath is null) return;

        //Get file with names
        var txtPath = GetTextFile();
        if(txtPath is null) return;
        
        //Get files from directory and read names
        var files = Directory.GetFiles(filesPath);
        var names = File.ReadAllLines(txtPath);
        
        //Ask user if everything correct
        Console.WriteLine($"\nFound {files.Length} files and {names.Length} names, so this is going to happen:");
        for (int i = 0; i < Math.Min(files.Length, names.Length); i++)
        {
            names[i] = $"{i + 1}. {names[i]}";
            Console.WriteLine(Path.GetFileName(files[i]).PadRight(20)+ " ---> " + names[i]);
        }
        
        while (true)
        {
            Console.WriteLine("Are you sure? (y/n)");
            var key = Console.ReadLine();
            if (key is not null)
            {
                key = key.ToLower();
                switch (key[0])
                {
                    case 'y':
                        Rename(files, names);
                        return;
                    case 'n':
                        Console.WriteLine("Process terminated");
                        return;
                }
            }
        }
    }
}