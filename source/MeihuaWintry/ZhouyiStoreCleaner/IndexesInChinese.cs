namespace ZhouyiStoreCleaner;
internal static class IndexesInChinese
{
    public static Dictionary<string, int> Dictionary { get; } = new();
    static IndexesInChinese()
    {
        const string zeroToTen = "零一二三四五六七八九十";
        for (int i = 0; i <= 10; i++)
        {
            Dictionary.Add(zeroToTen[i].ToString(), i);
        }
        Dictionary.Add("〇", 0);

        for (int i = 11; i < 20; i++)
        {
            Dictionary.Add($"十{zeroToTen[i % 10]}", i);
            Dictionary.Add($"一十{zeroToTen[i % 10]}", i);
        }
        Dictionary.Add($"二十", 20);

        for (int i = 21; i < 30; i++)
        {
            Dictionary.Add($"二十{zeroToTen[i % 10]}", i);
            Dictionary.Add($"廿{zeroToTen[i % 10]}", i);
        }
        Dictionary.Add($"三十", 30);

        for (int i = 31; i < 40; i++)
        {
            Dictionary.Add($"三十{zeroToTen[i % 10]}", i);
            Dictionary.Add($"卅{zeroToTen[i % 10]}", i);
        }
        Dictionary.Add($"四十", 40);

        for (int i = 41; i < 50; i++)
        {
            Dictionary.Add($"四十{zeroToTen[i % 10]}", i);
            Dictionary.Add($"卌{zeroToTen[i % 10]}", i);
        }
        Dictionary.Add($"五十", 50);

        for (int i = 51; i < 60; i++)
        {
            Dictionary.Add($"五十{zeroToTen[i % 10]}", i);
            Dictionary.Add($"圩{zeroToTen[i % 10]}", i);
        }
        Dictionary.Add($"六十", 60);

        for (int i = 61; i < 70; i++)
        {
            Dictionary.Add($"六十{zeroToTen[i % 10]}", i);
            Dictionary.Add($"圆{zeroToTen[i % 10]}", i);
        }
    }
}
