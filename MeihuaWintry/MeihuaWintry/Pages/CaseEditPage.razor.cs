using MeihuaWintry.Serialization;
using Microsoft.AspNetCore.Components;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MeihuaWintry.Pages;

public partial class CaseEditPage
{
    private bool hasLoaded = false;
    private DisplayedCase? editingCase;

    [Parameter]
    public Guid CaseId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var c = this.CaseStore.GetCase(this.CaseId);
        this.hasLoaded = true;
        if (c == null)
        {
            this.editingCase = null;
        }
        else
        {
            var z = await this.ZhouyiProvider.GetStoreAsync();
            this.editingCase = new(c, z);
            this.Save(true);
        }
    }

    private void Save(bool noPrompt = false)
    {
        if (this.editingCase is null)
            return;

        this.CaseStore.UpdateCase(this.editingCase.InnerCase);
        if (!noPrompt)
            this.Snackbar.Add("保存成功");
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
            stream, this.editingCase.InnerCase, new StoredCaseContext(new JsonSerializerOptions() {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }).StoredCase);
        await this.Downloader.DownloadFromStream(stream.ToArray(), $"{this.editingCase.Name}.mwb");
    }
}
