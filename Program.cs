using System;
using System.IO;
using System.Collections.Generic;

var FileCategory = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    {".jpg", "img/jpg"}, {".png", "img/png"}, {".webp", "img/webp"},
    {".svg", "img/svg"}, {".wav", "audio/wav"}, {".mp3", "audio/mp3"},
    {".ogg", "audio/ogg"}, {".midi", "audio/midi"}, {".m4a", "audio/m4a"},
    {".docx", "doc/docx"}, {".pdf", "doc/pdf"}, {".txt", "doc/txt"},
    {".md", "doc/md"}
};

string target = @"C:\Users\mateu\Desktop\test";

if (Directory.Exists(target))
{
    string[] files = Directory.GetFiles(target);
    foreach (string file in files)
    {
        string ext = Path.GetExtension(file).ToLower();
        if (FileCategory.TryGetValue(ext, out string nameFolder))
        {
            string DDir = Path.Combine(target, nameFolder);
            if (!Directory.Exists(DDir))
            {
                Directory.CreateDirectory(DDir);
            }
            string FileName = Path.GetFileName(file);
            string FinalDir = Path.Combine(DDir, FileName);
            string TestDir = Path.Combine(target, FileName);
            FileStream fs;

            try {
             fs = File.Open(TestDir,FileMode.Open, FileAccess.ReadWrite, FileShare.None);
             fs.Close();
            }
            catch (IOException) {
                Console.WriteLine($"O arquivo {FileName} não pode ser movido.");
                Console.WriteLine($"Motivo: o arquivo está sendo usado em outro processo.");
                continue;
            }
            File.Move(file, FinalDir);
            Console.WriteLine($"O arquivo {FileName} foi movido para {nameFolder}"); 
        }
    }
    if (files.Length == 0) {
    Console.WriteLine($"O programa não fez nada pois não tem arquivos soltos no diretório.");    
    }
}
else {
 Console.WriteLine($"O programa não foi executado, pois o diretório alvo não existe.");   
}