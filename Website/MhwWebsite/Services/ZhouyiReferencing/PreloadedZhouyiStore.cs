using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount.Extensions;

namespace MhwWebsite.Services.ZhouyiReferencing;

public sealed class PreloadedZhouyiStore
{
    private readonly ZhouyiHexagram[] hexagramsByIndex;
    private readonly Dictionary<GuaHexagram, ZhouyiHexagram> hexagramsByGua;
    private readonly Dictionary<GuaTrigram, ZhouyiTrigram> trigramsByGua;
    public PreloadedZhouyiStore(ZhouyiStore store)
    {
        var hexagrams = new List<ZhouyiHexagram>(64);
        this.hexagramsByGua = new(64);
        for (byte b = 0b1_000_000; b <= 0b1_111_111; b++)
        {
            var gua = Gua.FromBytes(new[] { b }).AsFixed<GuaHexagram>();
            var hexagram = store.GetHexagram(gua);
            hexagrams.Add(hexagram);
            this.hexagramsByGua[gua] = hexagram;
        }
        hexagrams.Sort((left, right) => string.CompareOrdinal(left.Index, right.Index));
        this.hexagramsByIndex = hexagrams.ToArray();

        this.trigramsByGua = new(8);
        for (byte b = 0b1_000; b <= 0b1_111; b++)
        {
            var gua = Gua.FromBytes(new[] { b }).AsFixed<GuaTrigram>();
            var trigram = store.GetTrigram(gua);
            this.trigramsByGua[gua] = trigram;
        }
    }

    public ZhouyiHexagram this[GuaHexagram gua]
    {
        get
        {
            return this.hexagramsByGua[gua];
        }
    }

    public ZhouyiTrigram this[GuaTrigram gua]
    {
        get
        {
            return this.trigramsByGua[gua];
        }
    }

    public IEnumerable<ZhouyiHexagram> EnumrateHexagramsByIndex()
    {
        return this.hexagramsByIndex;
    }
}
