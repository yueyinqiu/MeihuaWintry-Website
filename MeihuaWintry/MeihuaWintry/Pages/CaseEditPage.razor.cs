using MeihuaWintry.Services.CaseStorage;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace MeihuaWintry.Pages;

partial class CaseEditPage
{
    private MarkupString MessageCaseNotFound
    {
        get
        {
            var lines = new[] {
                $"您正试图访问标识码为 {this.CaseId} 的占例，" +
                $"但我们没有在您的的浏览器储存中找到它。" +
                $"这可能是出于以下原因：",
                "一、您打开了他人所分享的链接，或者正在跨浏览器使用。" +
                "冬日梅花的储存服务由本地浏览器提供，因此无法通过链接来分享或接收占例，" +
                "也无法跨浏览器访问。",
                "二、您试图访问的占例已被删除。" +
                "需要注意，冬日梅花的储存服务由本地浏览器提供，" +
                "清除浏览器的某些数据可能导致占例丢失。"
            };
            return new MarkupString(string.Join("<br>", lines));
        }
    }
        

    private CaseEditPageEditingCase? editingCase;

    [Parameter]
    public Guid CaseId { get; set; }

    protected override void OnParametersSet()
    {
        var c = this.CaseStore.GetCase(this.CaseId);
        this.editingCase = c is null ? null : new(c);
    }
}
