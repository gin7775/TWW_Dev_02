using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    private bool useEncryption = false;

    //private string wordToGenerateTheSeed = "generationSeed";

    //private int seed;

    private string encryptionWord = "PruebaDEEncrYpTacIoN";

    public FileDataHandler(string dataDirPath, string dataFilePath, bool useEncryption)
    {
        //System.Random random = new System.Random();
        //this.seed = random.Next();
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFilePath;
        this.useEncryption = useEncryption;
        //this.encryptionWord = GenerateEncryptionWord(10, seed);
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try 
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }

                if(useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } 
            catch(Exception e) 
            {
                Debug.LogError("No fue posible cargar el archivo de data desde: " + fullPath + '\n' + e);
            }
        }
        return loadedData;

    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Ocurrió un error al salvar los datos " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionWord[i % encryptionWord.Length]);
        }

        return modifiedData;
    }

    public static string GenerateEncryptionWord(int length, int seed)
    {
        System.Random random = new System.Random(seed);
        StringBuilder encryptionWord = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            // Generar un carácter aleatorio (letras y números)
            char randomChar = (char)random.Next(33, 126); // Caracteres imprimibles
            encryptionWord.Append(randomChar);
        }
        Debug.Log(encryptionWord.ToString());
        return encryptionWord.ToString();
    }

}
