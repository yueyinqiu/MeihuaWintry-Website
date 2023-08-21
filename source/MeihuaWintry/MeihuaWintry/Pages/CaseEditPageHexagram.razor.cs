using Microsoft.AspNetCore.Components;
using YiJingFramework.Annotating.Zhouyi.Entities;

namespace MeihuaWintry.Pages;

public partial class CaseEditPageHexagram
{
    [Parameter]
    public ZhouyiHexagram? Hexagram { get; set; }
}
