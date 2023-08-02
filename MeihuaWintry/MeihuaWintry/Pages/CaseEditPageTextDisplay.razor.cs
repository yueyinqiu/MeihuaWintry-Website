using Microsoft.AspNetCore.Components;
using YiJingFramework.Annotating.Zhouyi.Entities;

namespace MeihuaWintry.Pages;

public partial class CaseEditPageTextDisplay
{
    [Parameter]
    public ZhouyiHexagram? Hexagram { get; set; }

    private static string ToLineTitle(int index0Based, ZhouyiHexagram hexagram)
    {
        var y = hexagram.Painting[Math.Min(index0Based, 5)].IsYang ? '九' : '六';
        return index0Based switch
        {
            0 => $"初{y}",
            1 => $"{y}二",
            2 => $"{y}三",
            3 => $"{y}四",
            4 => $"{y}五",
            5 => $"上{y}",
            _ => $"用{y}",
        };
    }
}
