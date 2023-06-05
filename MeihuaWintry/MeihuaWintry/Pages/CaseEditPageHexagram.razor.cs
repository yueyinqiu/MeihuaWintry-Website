using Microsoft.AspNetCore.Components;
using System.Drawing;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.PrimitiveTypes;

namespace MeihuaWintry.Pages;

public partial class CaseEditPageHexagram
{
    [Parameter]
    public ZhouyiHexagram? Hexagram { get; set; }
}
