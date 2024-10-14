using MhwWebsite.Serialization;
using MhwWebsite.Services.ZhouyiReferencing;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.EntityRelations.GuaCharacters.Extensions;

namespace MhwWebsite.Pages;

public partial class CaseEditPage
{
    private DisplayedCase? editingCase;
    private PreloadedZhouyiStore? zhouyi;
    private ZhouyiHexagram? displayingText;

    [Parameter]
    public Guid CaseId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var c = this.CaseStore.GetCase(this.CaseId);
        this.zhouyi = await this.ZhouyiProvider.GetStoreAsync();
        if (c == null)
        {
            this.editingCase = null;
            this.displayingText = null;
        }
        else
        {
            this.editingCase = new(c, this.zhouyi);
            this.Save(true);
            this.displayingText = this.editingCase.Original;
        }
    }

    private string ToStringForSelection(ZhouyiHexagram hexagram)
    {
        var builder = new StringBuilder();
        _ = builder.Append(hexagram.Name);
        _ = builder.Append('（');

        bool isSpecial = false;
        if (hexagram.Painting == this.editingCase?.Original?.Painting)
        {
            _ = builder.Append("本卦、");
            isSpecial = true;
        }
        if (hexagram.Painting == this.editingCase?.Changed?.Painting)
        {
            _ = builder.Append("变卦、");
            isSpecial = true;
        }
        if (hexagram.Painting == this.editingCase?.Overlapping?.Painting)
        {
            _ = builder.Append("互卦、");
            isSpecial = true;
        }
        _ = builder.Remove(builder.Length - 1, 1);
        if (isSpecial)
            _ = builder.Append('）');
        return builder.ToString();
    }

    private void Save(bool noPrompt = false)
    {
        if (this.editingCase is null)
            return;

        this.CaseStore.UpdateCase(this.editingCase.InnerCase);
        if (!noPrompt)
            _ = this.Snackbar.Add("保存成功");
    }

    private async Task Delete()
    {
        if (this.editingCase is null)
            return;

        bool? result = await this.DialogService.ShowMessageBox(
            "删除确认",
            "您确定要删除此占例吗？",
            yesText: "删除", cancelText: "取消");
        if (result is not true)
            return;
        this.CaseStore.RemoveCase(this.editingCase.InnerCase);
        this.NavigationManager.NavigateTo("/");
    }
    private async Task Export()
    {
        if (this.editingCase is null)
            return;

        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(
            stream, this.editingCase.InnerCase, new StoredCaseContext(new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }).StoredCase);
        await this.Downloader.DownloadFromStream(stream.ToArray(), $"{this.editingCase.Name}.json");
    }
    private async Task Copy()
    {
        if (this.editingCase is null)
            return;

        var text = new StringBuilder();
        text.AppendLine($"{this.editingCase.Name}");
        text.AppendLine($"{this.editingCase.WesternTimeDisplay}");
        text.AppendLine($"卦象：" +
            $"{this.editingCase.Original.Painting.ToUnicodeChar()} " +
            $"{this.editingCase.Overlapping.Painting.ToUnicodeChar()} " +
            $"{this.editingCase.Changed.Painting.ToUnicodeChar()}");
        text.AppendLine($"备注：");
        text.AppendLine($"{this.editingCase.Comment}");
        await Clipboard.CopyTextToClipboardAsync(text.ToString());

        _ = this.Snackbar.Add("复制成功");
    }
}
