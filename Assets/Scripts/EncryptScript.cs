using UnityEngine;

public class EncryptScript : MonoBehaviour
{
    static int random = -1;

    public static void Initialize()
    {
        if(random == -1)
        {
            random = Random.Range(10000, 99999);
        }
    }

    public static int Obfuscate(int originalValue)
    {
        return random - originalValue;
    }

    public static int Deobfuscate(int obfuscatedValue)
    {
        return random - obfuscatedValue;
    }
}
