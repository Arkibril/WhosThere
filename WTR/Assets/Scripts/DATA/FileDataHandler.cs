using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "Eh ouais ! Tu ne me decryptera pas, c'est Zelrows bebe";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName + ".game";
        this.useEncryption = useEncryption;
    }

    public T Load<T>()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        T loadedData = default(T);

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<T>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Une erreur a été rencontrée lors du chargement des données " + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save<T>(T data)
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

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.Write(dataToStore);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Une erreur a été rencontrée lors de la sauvegarde des données : " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        StringBuilder modifiedData = new StringBuilder(data.Length);
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData.Append((char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]));

        }
        return modifiedData.ToString();
    }
}
